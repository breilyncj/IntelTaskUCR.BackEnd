using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AdjuntosXTareasController : Controller
    {
        private readonly IAdjuntosXTareasRepository _repository;

        public AdjuntosXTareasController(IAdjuntosXTareasRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{idAdjunto:int}/{idTarea:int}")]
        public async Task<IActionResult> Get(int idAdjunto, int idTarea)
        {
            var item = await _repository.GetByIdsAsync(idAdjunto, idTarea);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EAdjuntosXTareas entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { idAdjunto = entity.CN_Id_adjuntos, idTarea = entity.CN_Id_tarea }, entity);
        }

        [HttpPut("{idAdjunto:int}/{idTarea:int}")]
        public async Task<IActionResult> Update(int idAdjunto, int idTarea, [FromBody] EAdjuntosXTareas entity)
        {
            if (idAdjunto != entity.CN_Id_adjuntos || idTarea != entity.CN_Id_tarea)
                return BadRequest("IDs no coinciden");

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{idAdjunto:int}/{idTarea:int}")]
        public async Task<IActionResult> Delete(int idAdjunto, int idTarea)
        {
            await _repository.DeleteAsync(idAdjunto, idTarea);
            return NoContent();
        }
    }
}
