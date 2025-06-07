using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PrioridadesController : ControllerBase
    {

        private readonly IPrioridadesRepository _repository;

        public PrioridadesController(IPrioridadesRepository repository)
        {
            _repository = repository;
        }

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
        public async Task<IActionResult> Create([FromBody] EPrioridades entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_prioridad }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id, [FromBody] EPrioridades entity)
        {

            EPrioridades findPrioridades = await _repository.GetByIdAsync(id);

            if (findPrioridades != null)
            { 
                findPrioridades.CN_Id_prioridad = entity.CN_Id_prioridad;
                findPrioridades.CT_Nombre_prioridad = entity.CT_Nombre_prioridad;
                findPrioridades.CT_Descripcion_prioridad = entity.CT_Descripcion_prioridad;

                await _repository.UpdateAsync(findPrioridades);
                return NoContent();

            }
            else
            {
                return NotFound("El id de la prioridad no existe");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            bool tieneTareas = await _repository.TieneTareasAsync(id);

            if (tieneTareas)
                return BadRequest("No se puede eliminar la prioridad porque está asociada a una o más tareas.");

            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }
}
