using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TareasSeguimientoController : ControllerBase
    {
        private readonly ITareasSeguimientoRepository _repository;

        public TareasSeguimientoController(ITareasSeguimientoRepository repository)
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
        public async Task<IActionResult> Create([FromBody] ETareasSeguimiento entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_seguimiento }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ETareasSeguimiento entity)
        {
            var seguimientoExistente = await _repository.GetByIdAsync(id);

            if (seguimientoExistente != null)
            {
                seguimientoExistente.CN_Id_tarea = entity.CN_Id_tarea;
                seguimientoExistente.CT_Comentario = entity.CT_Comentario;
                seguimientoExistente.CF_Fecha_seguimiento = entity.CF_Fecha_seguimiento;

                await _repository.UpdateAsync(seguimientoExistente);
                return NoContent(); // 204
            }

            return NotFound("El id del seguimiento no existe");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }

}
