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

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tareas = await _repository.GetAllAsync();
            var dtoList = tareas.Select(MapToDto);
            return Ok(dtoList);
        }
        
        [HttpGet("withRelaciones")]
        public async Task<IActionResult> GetAllWithRelaciones()
        {
            var tareas = await _repository.GetAllWithRelacionesAsync();
            var dtoList = tareas.Select(MapToDto);
            return Ok(dtoList);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tareas = await _repository.GetByIdAsync(id);
            return tareas != null ? Ok(MapToDto(tareas)) : NotFound();
        }
        
        
        [HttpGet("WithRelaciones/{id}")]
        public async Task<IActionResult> GetWithRelaciones(int id)
        {
            var tarea = await _repository.GetByIdWithRelacionesAsync(id);
            return tarea != null ? Ok(MapToDto(tarea)) : NotFound();
        }
        
        [HttpGet("ByUsuarioCreador/{id}")]
        public async Task<IActionResult> GetByIdWithUsuarioCreadorAsync(int id)
        {
            var tareas = await _repository.GetAllByIdUsuarioCreadorAsync(id);
            var dtoList = tareas.Select(MapToDto);
            return Ok(dtoList);
        }
        
        [HttpGet("ByUsuarioAsignado/{id}")]
        public async Task<IActionResult> GetByIdWithUsuarioAsignadoAsync(int id)
        {
            var tareas = await _repository.GetAllByIdUsuarioAsignadoAsync(id);
            var dtoList = tareas.Select(MapToDto);
            return Ok(dtoList);
        }


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

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] byte nuevoEstadoId)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound($"No se encontró la tarea con ID {id}");
            }

            entity.CN_Id_estado = nuevoEstadoId;

            await _repository.UpdateAsync(entity);

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
                CN_Id_tarea = t.TareaOrigen.CN_Id_tarea,
                CN_Tarea_origen = t.TareaOrigen.CN_Tarea_origen,
                CT_Titulo_tarea = t.TareaOrigen.CT_Titulo_tarea,
                CT_Descripcion_tarea = t.TareaOrigen.CT_Descripcion_tarea,
                CT_Descripcion_espera = t.TareaOrigen.CT_Descripcion_espera,
                CN_Id_complejidad = t.TareaOrigen.CN_Id_complejidad,
                CN_Id_estado = t.TareaOrigen.CN_Id_estado,
                CN_Id_prioridad = t.TareaOrigen.CN_Id_prioridad,
                CN_Numero_GIS =t.TareaOrigen.CN_Numero_GIS,
                CF_Fecha_asignacion = t.TareaOrigen.CF_Fecha_asignacion,
                CF_Fecha_limite = t.TareaOrigen.CF_Fecha_limite,
                CF_Fecha_finalizacion = t.TareaOrigen.CF_Fecha_finalizacion,
                CN_Usuario_creador = t.TareaOrigen.CN_Usuario_creador,
                CN_Usuario_asignado = t.TareaOrigen.CN_Usuario_asignado,
                
            } : null,
            
            Prioridades = t.Prioridades != null ? new PrioridadesDTO()
            {
                CN_Id_prioridad = t.Prioridades.CN_Id_prioridad,
                CT_Nombre_prioridad = t.Prioridades.CT_Nombre_prioridad,
                CT_Descripcion_prioridad = t.Prioridades.CT_Descripcion_prioridad
            } : null,

            UsuarioAsignado = t.UsuarioAsignado != null ? new UsuariosDto()
            {
                CN_Id_usuario = t.UsuarioAsignado.CN_Id_usuario,
                CT_Nombre_usuario = t.UsuarioAsignado.CT_Nombre_usuario,
                CT_Correo_usuario = t.UsuarioAsignado.CT_Correo_usuario,
                CF_Fecha_nacimiento = t.UsuarioAsignado.CF_Fecha_nacimiento,
                CT_Contrasenna = t.UsuarioAsignado.CT_Contrasenna,
                CB_Estado_usuario = t.UsuarioAsignado.CB_Estado_usuario,
                CF_Fecha_creacion_usuario = t.UsuarioAsignado.CF_Fecha_creacion_usuario,
                CF_Fecha_modificacion_usuario = t.UsuarioAsignado.CF_Fecha_modificacion_usuario,
                CN_Id_rol = t.UsuarioAsignado.CN_Id_rol
            } : null,

            UsuarioCreador = t.UsuarioCreador != null ? new UsuariosDto()
            {
                CN_Id_usuario = t.UsuarioCreador.CN_Id_usuario,
                CT_Nombre_usuario = t.UsuarioCreador.CT_Nombre_usuario,
                CT_Correo_usuario = t.UsuarioCreador.CT_Correo_usuario,
                CF_Fecha_nacimiento = t.UsuarioCreador.CF_Fecha_nacimiento,
                CT_Contrasenna = t.UsuarioCreador.CT_Contrasenna,
                CB_Estado_usuario = t.UsuarioCreador.CB_Estado_usuario,
                CF_Fecha_creacion_usuario = t.UsuarioCreador.CF_Fecha_creacion_usuario,
                CF_Fecha_modificacion_usuario = t.UsuarioCreador.CF_Fecha_modificacion_usuario,
                CN_Id_rol = t.UsuarioCreador.CN_Id_rol
            } : null,


            Complejidades = t.Complejidades != null ? new ComplejidadesDto()
            {
                CN_Id_complejidad = t.Complejidades.CN_Id_complejidad,
                CT_Nombre = t.Complejidades.CT_Nombre,
            } : null,

            TareasHijas = t.TareasHijas?.Select(hijas => new TareasDto
            {
                CN_Id_tarea = hijas.CN_Id_tarea,
                CN_Tarea_origen = hijas.CN_Tarea_origen,
                CT_Titulo_tarea = hijas.CT_Titulo_tarea,
                CT_Descripcion_tarea = hijas.CT_Descripcion_tarea,
                CT_Descripcion_espera = hijas.CT_Descripcion_espera,
                CN_Id_complejidad = hijas.CN_Id_complejidad,
                CN_Id_estado = hijas.CN_Id_estado,
                CN_Id_prioridad = hijas.CN_Id_prioridad,
                CN_Numero_GIS = hijas.CN_Numero_GIS,
                CF_Fecha_asignacion = hijas.CF_Fecha_asignacion,
                CF_Fecha_limite = hijas.CF_Fecha_limite,
                CF_Fecha_finalizacion = hijas.CF_Fecha_finalizacion,
                CN_Usuario_creador = hijas.CN_Usuario_creador,
                CN_Usuario_asignado = hijas.CN_Usuario_asignado,
            }).ToList() ?? new List<TareasDto>(),

            TareasIncumplimientos = t.TareasIncumplimientos?.Select(incumplimientos => new TareaIncumplimientoDto()
            {
                CN_Id_tarea_incumplimiento = incumplimientos.CN_Id_tarea_incumplimiento,
                CN_Id_tarea = incumplimientos.CN_Id_tarea,
                CT_Justificacion_incumplimiento = incumplimientos.CT_Justificacion_incumplimiento,
                CF_Fecha_incumplimiento = incumplimientos.CF_Fecha_incumplimiento

            }).ToList() ?? new List<TareaIncumplimientoDto>(),

            TareasSeguimiento = t.TareasSeguimiento?.Select(seguimientos => new TareasSeguimientoDto()
            {
                CN_Id_seguimiento = seguimientos.CN_Id_seguimiento,
                CN_Id_tarea = seguimientos.CN_Id_tarea,
                CF_Fecha_seguimiento = seguimientos.CF_Fecha_seguimiento,
                CT_Comentario = seguimientos.CT_Comentario
            }).ToList() ?? new List<TareasSeguimientoDto>(),


             Estados = t.Estados != null ? new EstadosDto()
             {
                 CN_Id_estado = t.Estados.CN_Id_estado,
                 CT_Estado = t.Estados.CT_Estado,
                 CT_Descripcion = t.Estados.CT_Descripcion,
             } : null,


            TareasJustificacionRechazo = t.TareasJustificacionRechazo?.Select(rechazo => new TareasJustificacionRechazoDto()
            {
                CN_Id_tarea_rechazo = rechazo.CN_Id_tarea_rechazo,
                CN_Id_tarea = rechazo.CN_Id_tarea,
                CT_Descripcion_rechazo = rechazo.CT_Descripcion_rechazo,
                CF_Fecha_hora_rechazo = rechazo.CF_Fecha_hora_rechazo
            }).ToList() ?? new List<TareasJustificacionRechazoDto>(),

            Adjuntos = t.AdjuntosXTareas?.Select(ax => new AdjuntosDto
            {
                CN_Id_adjuntos = ax.CN_Id_adjuntos,
                CT_Archivo_ruta = ax.Adjunto?.CT_Archivo_ruta,
                CF_Fecha_registro = ax.Adjunto?.CF_Fecha_registro ?? DateTime.MinValue
            }).ToList() ?? new List<AdjuntosDto>()
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
            CN_Usuario_asignado = dto.CN_Usuario_asignado,
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
