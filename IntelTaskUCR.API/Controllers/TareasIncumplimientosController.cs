using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TareasIncumplimientosController : ControllerBase
    {
        private readonly ITareasIncumplimientosRepository _repository;

        public TareasIncumplimientosController(ITareasIncumplimientosRepository repository)
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
        public async Task<IActionResult> Create([FromBody] ETareasIncumplimientos entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_tarea_incumplimiento }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ETareasIncumplimientos entity)
        {

            ETareasIncumplimientos findTareaIncumplimiento = await _repository.GetByIdAsync(id);

            if (findTareaIncumplimiento != null)
            {
                findTareaIncumplimiento.CN_Id_tarea_incumplimiento = entity.CN_Id_tarea_incumplimiento;
                findTareaIncumplimiento.CN_Id_tarea = entity.CN_Id_tarea;
                findTareaIncumplimiento.CT_Justificacion_incumplimiento = entity.CT_Justificacion_incumplimiento;
                findTareaIncumplimiento.CF_Fecha_incumplimiento = entity.CF_Fecha_incumplimiento;

                await _repository.UpdateAsync(findTareaIncumplimiento);
                return NoContent();
            }
            return NotFound("El id de la tarea incumplimiento no existe");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
        
    }
}
