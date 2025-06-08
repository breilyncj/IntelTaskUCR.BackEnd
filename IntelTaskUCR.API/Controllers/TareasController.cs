using IntelTaskUCR.API.DTOs;
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

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     var items = await _repository.GetAllAsync();
        //     return Ok(items);
        // }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tareas = await _repository.GetAllAsync();
            var dtoList = tareas.Select(MapToDto);
            return Ok(dtoList);
        }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> Get(int id)
        // {
        //     var item = await _repository.GetByIdAsync(id);
        //     return item != null ? Ok(item) : NotFound();
        // }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tareas = await _repository.GetByIdAsync(id);
            return tareas != null ? Ok(MapToDto(tareas)) : NotFound();
        }
        
        // [HttpGet("WithIncumplimientos/{id}")]
        // public async Task<IActionResult> GetWithIncumplimientos(int id)
        // {
        //     var tarea = await _repository.GetByIdWithIncumplimientoAsync(id);
        //     return tarea != null ? Ok(MapToDto(tarea)) : NotFound();
        // }
        //
        // [HttpGet("WithTareaOrigen/{id}")]
        // public async Task<IActionResult> GetWithTareaOrigen(int id)
        // {
        //     var tarea = await _repository.GetByIdWithTareasOrigenAsync(id);
        //     return tarea != null ? Ok(MapToDto(tarea)) : NotFound();
        // }
        //
        // [HttpGet("WithTareasHijas/{id}")]
        // public async Task<IActionResult> GetWithTareasHijas(int id)
        // {
        //     var tarea = await _repository.GetByIdWithTareasHijasAsync(id);
        //     return tarea != null ? Ok(MapToDto(tarea)) : NotFound();
        // }
        
        [HttpGet("WithRelaciones/{id}")]
        public async Task<IActionResult> GetWithRelaciones(int id)
        {
            var tarea = await _repository.GetByIdWithRelacionesAsync(id);
            return tarea != null ? Ok(MapToDto(tarea)) : NotFound();
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] ETareas entity)
        // {
        //     await _repository.AddAsync(entity);
        //     return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_tarea }, entity);
        // }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TareasDto dto)
        {

            var entity = MapToEntity(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_tarea }, MapToDto(entity));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TareasDto dto)
        {

            if (id != dto.CN_Id_tarea)
            {
                return BadRequest("El id no existe. ");
            }
            
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound($"No se encontró la tarea con ID {id}");
            }

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
        
        private TareasDto MapToDto(ETareas t) => new TareasDto
        {
            CN_Id_tarea = t.CN_Id_tarea,
            CN_Tarea_origen = t.CN_Tarea_origen,
            CT_Titulo_tarea = t.CT_Titulo_tarea,
            CT_Descripcion_tarea = t.CT_Descripcion_tarea,
            CT_Descripcion_espera = t.CT_Descripcion_espera,
            CN_Id_complejidad = t.CN_Id_complejidad,
            CN_Id_estado = t.CN_Id_estado,
            CN_Id_prioridad = t.CN_Id_prioridad,
            CN_Numero_GIS = t.CN_Numero_GIS,
            CF_Fecha_asignacion = t.CF_Fecha_asignacion,
            CF_Fecha_limite = t.CF_Fecha_limite,
            CF_Fecha_finalizacion = t.CF_Fecha_finalizacion,
            CN_Usuario_creador = t.CN_Usuario_creador,
            CN_Usuario_asignado = t.CN_Usuario_asignado,
            
            TareaOrigen = t.TareaOrigen != null ? new TareasDto()
            {
                CN_Id_tarea = t.TareaOrigen.CN_Id_tarea
            } : null,
            
            TareasHijas = t.TareasHijas?.Select(hijas => new TareasDto
            {
                CN_Id_tarea = hijas.CN_Id_tarea,
                CT_Titulo_tarea = hijas.CT_Titulo_tarea,
                CT_Descripcion_tarea = hijas.CT_Descripcion_tarea
            }).ToList(),
            
            TareasIncumplimientos = t.TareasIncumplimientos?.Select(incumplimientos => new TareaIncumplimientoDto()
            {
                CN_Id_tarea_incumplimiento = incumplimientos.CN_Id_tarea_incumplimiento,
                CN_Id_tarea = incumplimientos.CN_Id_tarea,
                CT_Justificacion_incumplimiento = incumplimientos.CT_Justificacion_incumplimiento,
                CF_Fecha_incumplimiento = incumplimientos.CF_Fecha_incumplimiento
                
            }).ToList() ?? new List<TareaIncumplimientoDto>()
        };

        private ETareas MapToEntity(TareasDto dto) => new ETareas
        {
            CN_Id_tarea = dto.CN_Id_tarea,
            CN_Tarea_origen = dto.CN_Tarea_origen,
            CT_Titulo_tarea = dto.CT_Titulo_tarea,
            CT_Descripcion_tarea = dto.CT_Descripcion_tarea,
            CT_Descripcion_espera = dto.CT_Descripcion_espera,
            CN_Id_complejidad = dto.CN_Id_complejidad,
            CN_Id_estado = dto.CN_Id_estado,
            CN_Id_prioridad = dto.CN_Id_prioridad,
            CN_Numero_GIS = dto.CN_Numero_GIS,
            CF_Fecha_asignacion = dto.CF_Fecha_asignacion,
            CF_Fecha_limite = dto.CF_Fecha_limite,
            CF_Fecha_finalizacion = dto.CF_Fecha_finalizacion,
            CN_Usuario_creador = dto.CN_Usuario_creador,
            CN_Usuario_asignado = dto.CN_Usuario_asignado
        };
        
        private static void MapUpdateFields(ETareas entity, TareasDto dto)
        {
            
            entity.CT_Titulo_tarea = dto.CT_Titulo_tarea;
            entity.CT_Descripcion_tarea = dto.CT_Descripcion_tarea;
            entity.CT_Descripcion_espera = dto.CT_Descripcion_espera;
            entity.CN_Id_complejidad = dto.CN_Id_complejidad;
            entity.CN_Id_estado = dto.CN_Id_estado;
            entity.CN_Id_prioridad = dto.CN_Id_prioridad;
            entity.CN_Numero_GIS = dto.CN_Numero_GIS;
            entity.CF_Fecha_asignacion = dto.CF_Fecha_asignacion;
            entity.CF_Fecha_limite = dto.CF_Fecha_limite;
            entity.CF_Fecha_finalizacion = dto.CF_Fecha_finalizacion;
            entity.CN_Usuario_creador = dto.CN_Usuario_creador;
            entity.CN_Usuario_asignado = dto.CN_Usuario_asignado;
        }
    }
}
