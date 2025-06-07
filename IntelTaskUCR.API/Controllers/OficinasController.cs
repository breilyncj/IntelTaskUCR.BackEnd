using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OficinasController : ControllerBase
    {
        private readonly IOficinasRepository _repository;
        public OficinasController(IOficinasRepository repository) { _repository = repository; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EOficinas entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Codigo_oficina }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EOficinas entity)
        {

            EOficinas findOficina = await _repository.GetByIdAsync(id);

            if (findOficina != null)
            {

                findOficina.CN_Codigo_oficina = entity.CN_Codigo_oficina; // Si hubiera incremento en la bd esta linea se ignora
                findOficina.CT_Nombre_oficina = entity.CT_Nombre_oficina;
                findOficina.CN_Oficina_encargada = entity.CN_Oficina_encargada;

                await _repository.UpdateAsync(findOficina);
                return NoContent();

            }
            else
            {
                return NotFound("El id de la oficina no existe");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
