using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasJustificacionRechazoController : ControllerBase
    {
        private readonly ITareasJustificacionRechazoRepository _repository;

        public TareasJustificacionRechazoController(ITareasJustificacionRechazoRepository repository)
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
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ETareasJustificacionRechazo entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_tarea_rechazo }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ETareasJustificacionRechazo entity)
        {
            ETareasJustificacionRechazo findRechazo = await _repository.GetByIdAsync(id);

            if (findRechazo != null)
            {
                findRechazo.CN_Id_tarea = entity.CN_Id_tarea;
                findRechazo.CF_Fecha_hora_rechazo = entity.CF_Fecha_hora_rechazo;
                findRechazo.CT_Descripcion_rechazo = entity.CT_Descripcion_rechazo;

                await _repository.UpdateAsync(findRechazo);
                return NoContent();
            }

            return NotFound("El id de la justificación de rechazo no existe");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }

}
