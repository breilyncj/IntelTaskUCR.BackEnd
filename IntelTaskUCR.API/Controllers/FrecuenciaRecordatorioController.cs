using IntelTaskUCR.API.DTOs;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FrecuenciaRecordatorioController : ControllerBase
    {
        private readonly IFrecuenciaRecordatorioRepository _repository;
        // private readonly IUsuariosRepository _usuariosRepository;
        public FrecuenciaRecordatorioController(IFrecuenciaRecordatorioRepository repository) { 
            _repository = repository;
            // _usuariosRepository = usuariosRepository;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     var items = await _frecuenciaRecordatorioRepository.GetAllAsync();
        //     return Ok(items);
        // }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var frecuenciaRecordatorios = await _repository.GetAllAsync();
            var dtoList = frecuenciaRecordatorios.Select(MapToDto);
            return Ok(dtoList);
        }
        
        [HttpGet("with_usuarios")]
        public async Task<IActionResult> GetAllWithUsuarios()
        {
            var frecuenciaRecordatorios = await _repository.GetAllWithUsuariosAsync();
            var dtoList = frecuenciaRecordatorios.Select(MapToDto);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var frecuenciaRecordatorios = await _repository.GetByIdAsync(id);
            return frecuenciaRecordatorios != null ? Ok(MapToDto(frecuenciaRecordatorios)) : NotFound();
        }
        
        [HttpGet("with_usuario/{id}")]
        public async Task<IActionResult> GetWithUsuario(int id)
        {
            var frecuenciaRecordatorio = await _repository.GetByIdWithUsuariosAsync(id);
            if (frecuenciaRecordatorio == null) return NotFound();
            var dto = MapToDto(frecuenciaRecordatorio);
            return Ok(dto);
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] EFrecuenciaRecordatorio entity)
        // {
        //
        //     EUsuarios usuarioCreador = await _usuariosRepository.GetByIdAsync(entity.CN_Id_usuario_creador);
        //
        //     if (usuarioCreador != null)
        //     {
        //         await _frecuenciaRecordatorioRepository.AddAsync(entity);
        //         return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_recordatorio }, entity);
        //     }
        //     else
        //     {
        //         return NotFound("No se encontró el usuario creador");
        //     }
        // }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FrecuenciaRecordatorioDto dto)
        {
            var entity = MapToEntity(dto);
            
            await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_recordatorio }, dto);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, [FromBody] EFrecuenciaRecordatorio entity)
        // {
        //
        //     EFrecuenciaRecordatorio findFrecuenciaRecordatorio = await _frecuenciaRecordatorioRepository.GetByIdAsync(id);
        //
        //     EUsuarios usuarioCreador = await _usuariosRepository.GetByIdAsync(entity.CN_Id_usuario_creador);
        //
        //     if (usuarioCreador != null && findFrecuenciaRecordatorio != null)
        //     {
        //         findFrecuenciaRecordatorio.CN_Id_recordatorio = entity.CN_Id_recordatorio; // Si hubiera incremento en la bd esta linea se ignora
        //         findFrecuenciaRecordatorio.CT_Texto_recordatorio = entity.CT_Texto_recordatorio;
        //         findFrecuenciaRecordatorio.CF_Fecha_hora_recordatorio = entity.CF_Fecha_hora_recordatorio;
        //         findFrecuenciaRecordatorio.CF_Fecha_hora_registro = entity.CF_Fecha_hora_registro;
        //         findFrecuenciaRecordatorio.CF_Fecha_final_evento = entity.CF_Fecha_final_evento;
        //         findFrecuenciaRecordatorio.CF_Fecha_hora_evento_pospuesto = entity.CF_Fecha_hora_evento_pospuesto;
        //         findFrecuenciaRecordatorio.CN_Frecuencia_recordatorio = entity.CN_Frecuencia_recordatorio;
        //         findFrecuenciaRecordatorio.CN_Id_usuario_creador = entity.CN_Id_usuario_creador;
        //         findFrecuenciaRecordatorio.CB_Estado = entity.CB_Estado;
        //
        //         await _frecuenciaRecordatorioRepository.UpdateAsync(findFrecuenciaRecordatorio);
        //         return NoContent();
        //
        //     }
        //     else if (findFrecuenciaRecordatorio == null)
        //     {
        //         return NotFound("No se encontró la frecuencia del recordatorio");
        //     }
        //     else
        //     {
        //         return NotFound("No se encontró el usuario creador");
        //     }
        //
        // }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FrecuenciaRecordatorioDto dto)
        {
            if (id != dto.CN_Id_recordatorio) return BadRequest("Id no existe");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound($"No se encontró la frecuencia del recordatorio con ID {id}");

            // Actualizamos solo lo permitido
            MapUpdateFields(entity, dto);
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     await _frecuenciaRecordatorioRepository.DeleteAsync(id);
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

        private FrecuenciaRecordatorioDto MapToDto(EFrecuenciaRecordatorio entity)
        {
            return new FrecuenciaRecordatorioDto
            {
                CN_Id_recordatorio = entity.CN_Id_recordatorio,
                CT_Texto_recordatorio = entity.CT_Texto_recordatorio,
                CF_Fecha_hora_recordatorio = entity.CF_Fecha_hora_recordatorio,
                CF_Fecha_hora_registro = entity.CF_Fecha_hora_registro,
                CF_Fecha_final_evento = entity.CF_Fecha_final_evento,
                CF_Fecha_hora_evento_pospuesto = entity.CF_Fecha_hora_evento_pospuesto,
                CN_Frecuencia_recordatorio = entity.CN_Frecuencia_recordatorio,
                CN_Id_usuario_creador = entity.CN_Id_usuario_creador,
                CB_Estado = entity.CB_Estado,
                Usuarios = entity.Usuarios != null
                    ? new UsuariosDto
                    {
                        CN_Id_usuario = entity.Usuarios.CN_Id_usuario,
                        CN_Id_rol = entity.Usuarios.CN_Id_rol,
                        CT_Contrasenna = entity.Usuarios.CT_Contrasenna,
                        CT_Correo_usuario = entity.Usuarios.CT_Correo_usuario,
                        CT_Nombre_usuario = entity.Usuarios.CT_Nombre_usuario,
                        CB_Estado_usuario = entity.Usuarios.CB_Estado_usuario,
                        CF_Fecha_creacion_usuario = entity.Usuarios.CF_Fecha_creacion_usuario,
                        CF_Fecha_modificacion_usuario = entity.Usuarios.CF_Fecha_modificacion_usuario,
                        CF_Fecha_nacimiento = entity.Usuarios.CF_Fecha_nacimiento
                    }
                    : null
            };
        }

        private EFrecuenciaRecordatorio MapToEntity(FrecuenciaRecordatorioDto dto) => new EFrecuenciaRecordatorio()
        {
            CN_Id_recordatorio = dto.CN_Id_recordatorio,
            CT_Texto_recordatorio = dto.CT_Texto_recordatorio,
            CF_Fecha_hora_recordatorio = dto.CF_Fecha_hora_recordatorio,
            CF_Fecha_hora_registro = dto.CF_Fecha_hora_registro,
            CF_Fecha_final_evento = dto.CF_Fecha_final_evento,
            CF_Fecha_hora_evento_pospuesto = dto.CF_Fecha_hora_evento_pospuesto,
            CN_Frecuencia_recordatorio = dto.CN_Frecuencia_recordatorio,
            CB_Estado = dto.CB_Estado,
            CN_Id_usuario_creador = dto.CN_Id_usuario_creador
        };

        private static void MapUpdateFields(EFrecuenciaRecordatorio entity, FrecuenciaRecordatorioDto dto)
        {
            entity.CT_Texto_recordatorio = dto.CT_Texto_recordatorio;
            entity.CF_Fecha_hora_recordatorio = dto.CF_Fecha_hora_recordatorio;
            entity.CF_Fecha_hora_registro = dto.CF_Fecha_hora_registro;
            entity.CF_Fecha_final_evento = dto.CF_Fecha_final_evento;
            entity.CF_Fecha_hora_evento_pospuesto = dto.CF_Fecha_hora_evento_pospuesto;
            entity.CN_Frecuencia_recordatorio = dto.CN_Frecuencia_recordatorio;
            entity.CB_Estado = dto.CB_Estado;
        }
    }
}
