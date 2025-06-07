using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly ITareasRepository _repository;
        public TareasController(ITareasRepository repository) { _repository = repository; }

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
        public async Task<IActionResult> Create([FromBody] ETareas entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_tarea }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ETareas entity)
        {

            ETareas findTarea = await _repository.GetByIdAsync(id);

            if (findTarea != null)
            {
                findTarea.CN_Id_tarea = entity.CN_Id_tarea; 
                findTarea.CN_Tarea_origen = entity.CN_Tarea_origen;
                findTarea.CT_Titulo_tarea = entity.CT_Titulo_tarea;
                findTarea.CT_Descripcion_tarea = entity.CT_Descripcion_tarea;
                findTarea.CT_Descripcion_espera = entity.CT_Descripcion_espera;
                findTarea.CN_Id_complejidad = entity.CN_Id_complejidad;
                findTarea.CN_Id_estado = entity.CN_Id_estado;
                findTarea.CN_Id_prioridad = entity.CN_Id_prioridad;
                findTarea.CN_Numero_GIS = entity.CN_Numero_GIS;
                findTarea.CF_Fecha_asignacion = entity.CF_Fecha_asignacion;
                findTarea.CF_Fecha_limite = entity.CF_Fecha_limite;
                findTarea.CF_Fecha_finalizacion = entity.CF_Fecha_finalizacion;
                findTarea.CN_Usuario_creador = entity.CN_Usuario_creador;
                findTarea.CN_Usuario_asignado = entity.CN_Usuario_asignado;

                await _repository.UpdateAsync(findTarea);
                return NoContent();
            }
            else
            {
                return NotFound("El id de la tarea no existe");
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
