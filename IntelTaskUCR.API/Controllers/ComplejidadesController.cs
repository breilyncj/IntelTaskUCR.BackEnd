using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplejidadesController : ControllerBase
    {
        private readonly IComplejidadesRepository _repository;
        public ComplejidadesController(IComplejidadesRepository repository) { _repository = repository; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(byte id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EComplejidades entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_complejidad }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id, [FromBody] EComplejidades entity)
        {

            EComplejidades findComplejidad = await _repository.GetByIdAsync(id);

            if (findComplejidad != null)
            {
                //findUser.CN_Id_complejidad = entity.CN_Id_complejidad; 
                findComplejidad.CT_Nombre = entity.CT_Nombre;

                await _repository.UpdateAsync(findComplejidad);
                return NoContent();

            }
            else
            {
                return NotFound("El id de la complejidad no existe");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            bool tieneTareas = await _repository.TieneTareasAsync(id);

            if (tieneTareas)
                return BadRequest("No se puede eliminar la complejidad porque está asociada a una o más tareas.");

            await _repository.DeleteAsync(id);
            return NoContent();
        }







    }
}
