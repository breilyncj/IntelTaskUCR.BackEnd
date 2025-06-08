using IntelTaskUCR.API.DTOs;
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
            var tareasSeguimiento = await _repository.GetAllAsync();
            var dtoList = tareasSeguimiento.Select(MapToDto);
            return Ok(dtoList);
        }

        [HttpGet("with_tareas")]
        public async Task<IActionResult> GetAllWithTareas()
        {
            var tareasSeguimiento= await _repository.GetAllWithTareasAsync();
            var dtoList = tareasSeguimiento.Select(MapToDto);
            return Ok(dtoList);
        }

        [HttpGet("with_tarea/{id}")]
        public async Task<IActionResult> GetWithTarea(int id)
        {
            var item = await _repository.GetByIdWithTareasAsync(id);
            if (item == null) return NotFound();
            var dto = MapToDto(item);
            return Ok(dto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TareasSeguimientoDto dto)
        {
            var entity = MapToEntity(dto);

            await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_seguimiento }, dto);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TareasSeguimientoDto dto)
        {
            if (id != dto.CN_Id_seguimiento) return BadRequest("Id no existe");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound($"No se encontró el incumplimiento con ID {id}");

            // Actualizamos solo lo permitido
            MapUpdateFields(entity, dto);
            await _repository.UpdateAsync(entity);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private TareasSeguimientoDto MapToDto(ETareasSeguimiento entity)
        {
            return new TareasSeguimientoDto
            {
                CN_Id_seguimiento = entity.CN_Id_seguimiento,
                CN_Id_tarea = entity.CN_Id_tarea,
                CT_Comentario = entity.CT_Comentario,
                CF_Fecha_seguimiento = entity.CF_Fecha_seguimiento,
                Tareas = entity.Tareas != null ? new TareasDto
                {
                    CN_Id_tarea = entity.Tareas.CN_Id_tarea,
                    CN_Tarea_origen = entity.Tareas.CN_Tarea_origen,
                    CT_Titulo_tarea = entity.Tareas.CT_Titulo_tarea,
                    CT_Descripcion_tarea = entity.Tareas.CT_Descripcion_tarea,
                    CT_Descripcion_espera = entity.Tareas.CT_Descripcion_espera,
                    CN_Id_complejidad = entity.Tareas.CN_Id_complejidad,
                    CN_Id_estado = entity.Tareas.CN_Id_estado,
                    CN_Id_prioridad = entity.Tareas.CN_Id_prioridad,
                    CN_Numero_GIS = entity.Tareas.CN_Numero_GIS,
                    CF_Fecha_asignacion = entity.Tareas.CF_Fecha_asignacion,
                    CF_Fecha_limite = entity.Tareas.CF_Fecha_limite,
                    CF_Fecha_finalizacion = entity.Tareas.CF_Fecha_finalizacion,
                    CN_Usuario_creador = entity.Tareas.CN_Usuario_creador,
                    CN_Usuario_asignado = entity.Tareas.CN_Usuario_asignado
                } : null
            };
        }

        private ETareasSeguimiento MapToEntity(TareasSeguimientoDto dto) => new ETareasSeguimiento
        {

            CN_Id_seguimiento = dto.CN_Id_seguimiento,
            CN_Id_tarea = dto.CN_Id_tarea,
            CT_Comentario = dto.CT_Comentario,
            CF_Fecha_seguimiento = dto.CF_Fecha_seguimiento
        };

        private static void MapUpdateFields(ETareasSeguimiento entity, TareasSeguimientoDto dto)
        {
            entity.CT_Comentario = dto.CT_Comentario;
            entity.CF_Fecha_seguimiento = dto.CF_Fecha_seguimiento;
        }

    }

}
