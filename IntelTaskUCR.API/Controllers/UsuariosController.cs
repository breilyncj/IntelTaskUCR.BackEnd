using IntelTaskUCR.API.DTOs;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _repository;
        public UsuariosController(IUsuariosRepository repository) { _repository = repository; }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _repository.GetAllAsync();
            var dtoList = usuarios.Select(MapToDto);
            return Ok(dtoList);
        }
        
        [HttpGet("withRelaciones")]
        public async Task<IActionResult> GetAllWithRelaciones()
        {
            var usuarios = await _repository.GetAllWithRelacionesAsync();
            var dtoList = usuarios.Select(MapToDto);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var usuarios = await _repository.GetByIdAsync(id);
            return usuarios != null ? Ok(MapToDto(usuarios)) : NotFound();
        }
        
        [HttpGet("WithFrecuenciaRecordatorios/{id}")]
        public async Task<IActionResult> GetWithFrecuenciaRecordatorios(int id)
        {
            var usuario = await _repository.GetByIdWithFrecuenciaRecordatorioAsync(id);
            return usuario != null ? Ok(MapToDto(usuario)) : NotFound();
        }
        
        [HttpGet("WithTareasAsignadas/{id}")]
        public async Task<IActionResult> GetWithTareasAsignadas(int id)
        {
            var usuario = await _repository.GetByIdWithTareasAsignadasAsync(id);
            return usuario != null ? Ok(MapToDto(usuario)) : NotFound();
        }
        
        [HttpGet("WithTareasCreador/{id}")]
        public async Task<IActionResult> GetWithTareasCreador(int id)
        {
            var usuario = await _repository.GetByIdWithTareasCreadorAsync(id);
            return usuario != null ? Ok(MapToDto(usuario)) : NotFound();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuariosDto dto)
        {
            var entity = MapToEntity(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_usuario }, MapToDto(entity));
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuariosDto dto)
        {

            if (id != dto.CN_Id_usuario)
            {
                return BadRequest("El id no existe. ");
            }
            
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound($"No se encontró el usuario con ID {id}");
            }

            MapUpdateFields(entity, dto);
            await _repository.UpdateAsync(entity);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("GetNotificacionesDeUsuario/{idUsuario}")]
        public async Task<IActionResult> GetNotificacionesUsuario(int idUsuario)
        {
            var notificaciones = await _repository.GetNotificacionesDeUsuario(idUsuario);
            return Ok(notificaciones);
        }


        private UsuariosDto MapToDto(EUsuarios u) => new UsuariosDto
        {
            CN_Id_usuario = u.CN_Id_usuario,
            CN_Id_rol = u.CN_Id_rol,
            CT_Contrasenna = u.CT_Contrasenna,
            CT_Correo_usuario = u.CT_Correo_usuario,
            CT_Nombre_usuario = u.CT_Nombre_usuario,
            CF_Fecha_creacion_usuario = u.CF_Fecha_creacion_usuario,
            CF_Fecha_nacimiento = u.CF_Fecha_nacimiento,
            CF_Fecha_modificacion_usuario = u.CF_Fecha_modificacion_usuario,
            CB_Estado_usuario = u.CB_Estado_usuario,
            
            TareasUsuarioAsignado = u.TareasUsuarioAsignado?.Select(tareasAsignadas => new TareasDto()
            {
                CN_Id_tarea = tareasAsignadas.CN_Id_tarea,
                CN_Tarea_origen = tareasAsignadas.CN_Tarea_origen,
                CT_Titulo_tarea = tareasAsignadas.CT_Titulo_tarea,
                CT_Descripcion_tarea = tareasAsignadas.CT_Descripcion_tarea,
                CT_Descripcion_espera = tareasAsignadas.CT_Descripcion_espera,
                CN_Id_complejidad = tareasAsignadas.CN_Id_complejidad,
                CN_Id_estado = tareasAsignadas.CN_Id_estado,
                CN_Id_prioridad = tareasAsignadas.CN_Id_prioridad,
                CN_Numero_GIS = tareasAsignadas.CN_Numero_GIS,
                CF_Fecha_asignacion = tareasAsignadas.CF_Fecha_asignacion,
                CF_Fecha_limite = tareasAsignadas.CF_Fecha_limite,
                CF_Fecha_finalizacion = tareasAsignadas.CF_Fecha_finalizacion,
                CN_Usuario_creador = tareasAsignadas.CN_Usuario_creador,
                CN_Usuario_asignado = tareasAsignadas.CN_Usuario_asignado,
                
            }).ToList() ?? new List<TareasDto>(),
            
            TareasUsuarioCreador  = u.TareasUsuarioCreador?.Select(tareasCreadas => new TareasDto()
            {
                CN_Id_tarea = tareasCreadas.CN_Id_tarea,
                CN_Tarea_origen = tareasCreadas.CN_Tarea_origen,
                CT_Titulo_tarea = tareasCreadas.CT_Titulo_tarea,
                CT_Descripcion_tarea = tareasCreadas.CT_Descripcion_tarea,
                CT_Descripcion_espera = tareasCreadas.CT_Descripcion_espera,
                CN_Id_complejidad = tareasCreadas.CN_Id_complejidad,
                CN_Id_estado = tareasCreadas.CN_Id_estado,
                CN_Id_prioridad = tareasCreadas.CN_Id_prioridad,
                CN_Numero_GIS = tareasCreadas.CN_Numero_GIS,
                CF_Fecha_asignacion = tareasCreadas.CF_Fecha_asignacion,
                CF_Fecha_limite = tareasCreadas.CF_Fecha_limite,
                CF_Fecha_finalizacion = tareasCreadas.CF_Fecha_finalizacion,
                CN_Usuario_creador = tareasCreadas.CN_Usuario_creador,
                CN_Usuario_asignado = tareasCreadas.CN_Usuario_asignado,
                
            }).ToList() ?? new List<TareasDto>(),
            
            
            
            
            FrecuenciaRecordatorios = u.FrecuenciaRecordatorios?.Select(frecuencias => new FrecuenciaRecordatorioDto()
            {
                CN_Id_recordatorio = frecuencias.CN_Id_recordatorio,
                CN_Id_usuario_creador = frecuencias.CN_Id_usuario_creador,
                CN_Frecuencia_recordatorio = frecuencias.CN_Frecuencia_recordatorio,
                CF_Fecha_final_evento = frecuencias.CF_Fecha_final_evento,
                CF_Fecha_hora_evento_pospuesto = frecuencias.CF_Fecha_hora_evento_pospuesto,
                CF_Fecha_hora_recordatorio = frecuencias.CF_Fecha_hora_recordatorio,
                CF_Fecha_hora_registro = frecuencias.CF_Fecha_hora_registro,
                CB_Estado = frecuencias.CB_Estado,
                CT_Texto_recordatorio = frecuencias.CT_Texto_recordatorio
                
            }).ToList() ?? new List<FrecuenciaRecordatorioDto>()
            
            
            
        };

        private EUsuarios MapToEntity(UsuariosDto dto) => new EUsuarios
        {
            CN_Id_usuario = dto.CN_Id_usuario,
            CN_Id_rol = dto.CN_Id_rol,
            CT_Contrasenna = dto.CT_Contrasenna,
            CT_Correo_usuario = dto.CT_Correo_usuario,
            CT_Nombre_usuario = dto.CT_Nombre_usuario,
            CF_Fecha_creacion_usuario = dto.CF_Fecha_creacion_usuario,
            CF_Fecha_nacimiento = dto.CF_Fecha_nacimiento,
            CF_Fecha_modificacion_usuario = dto.CF_Fecha_modificacion_usuario,
            CB_Estado_usuario = dto.CB_Estado_usuario
        };
        
        private static void MapUpdateFields(EUsuarios entity, UsuariosDto dto)
        {
            entity.CN_Id_rol = dto.CN_Id_rol;
            entity.CT_Contrasenna = dto.CT_Contrasenna;
            entity.CT_Correo_usuario = dto.CT_Correo_usuario;
            entity.CT_Nombre_usuario = dto.CT_Nombre_usuario;
            // entity.CF_Fecha_creacion_usuario = dto.CF_Fecha_creacion_usuario;
            entity.CF_Fecha_modificacion_usuario = dto.CF_Fecha_modificacion_usuario;
            entity.CF_Fecha_nacimiento = dto.CF_Fecha_nacimiento;
            entity.CB_Estado_usuario = dto.CB_Estado_usuario;
        }


    }
}
