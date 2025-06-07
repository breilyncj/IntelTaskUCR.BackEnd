using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosController : ControllerBase
    {
        private readonly IEstadosRepository _repository;
        public EstadosController(IEstadosRepository repository) { _repository = repository; }

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
        public async Task<IActionResult> Create([FromBody] EEstados entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_estado }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id, [FromBody] EEstados entity)
        {

            EEstados findUser = await _repository.GetByIdAsync(id);

            if (findUser != null)
            {
                //findUser.CN_Id_estado = entity.CN_Id_estado; 
                findUser.CT_Estado = entity.CT_Estado;
                findUser.CT_Descripcion = entity.CT_Descripcion;

                await _repository.UpdateAsync(findUser);
                return NoContent();

            }
            else
            {
                return NotFound("El id del usuario no existe");
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
