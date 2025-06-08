using IntelTaskUCR.API.DTOs;
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
            var tareasIncumplimientos = await _repository.GetAllAsync();
            var dtoList = tareasIncumplimientos.Select(MapToDto);
            return Ok(dtoList);
        }
        
        [HttpGet("with_tareas")]
        public async Task<IActionResult> GetAllWithTareas()
        {
            var tareasIncumplimientos = await _repository.GetAllWithTareasAsync();
            var dtoList = tareasIncumplimientos.Select(MapToDto);
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
            var tareaIncumplimiento = await _repository.GetByIdAsync(id);
            return tareaIncumplimiento != null ? Ok(MapToDto(tareaIncumplimiento)) : NotFound();
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] ETareasIncumplimientos entity)
        // {
        //     await _repository.AddAsync(entity);
        //     return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_tarea_incumplimiento }, entity);
        // }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TareaIncumplimientoDto dto)
        {
            var entity = MapToEntity(dto);

            await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_tarea_incumplimiento }, dto);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, [FromBody] ETareasIncumplimientos entity)
        // {
        //
        //     ETareasIncumplimientos findTareaIncumplimiento = await _repository.GetByIdAsync(id);
        //
        //     if (findTareaIncumplimiento != null)
        //     {
        //         findTareaIncumplimiento.CN_Id_tarea_incumplimiento = entity.CN_Id_tarea_incumplimiento;
        //         findTareaIncumplimiento.CN_Id_tarea = entity.CN_Id_tarea;
        //         findTareaIncumplimiento.CT_Justificacion_incumplimiento = entity.CT_Justificacion_incumplimiento;
        //         findTareaIncumplimiento.CF_Fecha_incumplimiento = entity.CF_Fecha_incumplimiento;
        //
        //         await _repository.UpdateAsync(findTareaIncumplimiento);
        //         return NoContent();
        //     }
        //     return NotFound("El id de la tarea incumplimiento no existe");
        //
        // }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TareaIncumplimientoDto dto)
        {
            if (id != dto.CN_Id_tarea_incumplimiento) return BadRequest("Id no existe");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound($"No se encontró el incumplimiento con ID {id}");

            // Actualizamos solo lo permitido
            MapUpdateFields(entity, dto);
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     await _repository.DeleteAsync(id);
        //     return NoContent();
        // }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private TareaIncumplimientoDto MapToDto(ETareasIncumplimientos entity)
        {
            return new TareaIncumplimientoDto
            {
                CN_Id_tarea_incumplimiento = entity.CN_Id_tarea_incumplimiento,
                CN_Id_tarea = entity.CN_Id_tarea,
                CT_Justificacion_incumplimiento = entity.CT_Justificacion_incumplimiento,
                CF_Fecha_incumplimiento = entity.CF_Fecha_incumplimiento,
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


        private ETareasIncumplimientos MapToEntity(TareaIncumplimientoDto dto) => new ETareasIncumplimientos
        {
            CN_Id_tarea_incumplimiento = dto.CN_Id_tarea_incumplimiento,
            CN_Id_tarea = dto.CN_Id_tarea,
            CT_Justificacion_incumplimiento = dto.CT_Justificacion_incumplimiento,
            CF_Fecha_incumplimiento = dto.CF_Fecha_incumplimiento
        };
        
        private static void MapUpdateFields(ETareasIncumplimientos entity, TareaIncumplimientoDto dto)
        {
            entity.CT_Justificacion_incumplimiento = dto.CT_Justificacion_incumplimiento;
            entity.CF_Fecha_incumplimiento = dto.CF_Fecha_incumplimiento;
        }
        
    }
}
