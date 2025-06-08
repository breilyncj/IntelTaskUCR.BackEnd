using IntelTaskUCR.API.DTOs;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasJustificacionRechazoController : ControllerBase
    {
        private readonly ITareasJustificacionRechazoRepository _repository;

        public TareasJustificacionRechazoController(ITareasJustificacionRechazoRepository repository)
        {
            _repository = repository;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     var items = await _repository.GetAllAsync();
        //     return Ok(items);
        // }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tareasJustificacionRechazos = await _repository.GetAllAsync();
            var dtoList = tareasJustificacionRechazos.Select(MapToDto);
            return Ok(dtoList);
        }
        
        [HttpGet("with_tareas")]
        public async Task<IActionResult> GetAllWithTareas()
        {
            var tareasJustificacionRechazos = await _repository.GetAllWithTareasAsync();
            var dtoList = tareasJustificacionRechazos.Select(MapToDto);
            return Ok(dtoList);
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tareasJustificacionRechazos = await _repository.GetByIdAsync(id);
            return tareasJustificacionRechazos != null ? Ok(tareasJustificacionRechazos) : NotFound();
        }
        
        [HttpGet("with_tarea/{id}")]
        public async Task<IActionResult> GetWithTarea(int id)
        {
            var tareasJustificacionRechazos = await _repository.GetByIdWithTareasAsync(id);
            if (tareasJustificacionRechazos == null) return NotFound();
            var dto = MapToDto(tareasJustificacionRechazos);
            return Ok(dto);
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] ETareasJustificacionRechazo entity)
        // {
        //     await _repository.AddAsync(entity);
        //     return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_tarea_rechazo }, entity);
        // }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TareasJustificacionRechazoDto dto)
        {
            var entity = MapToEntity(dto);

            await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_tarea_rechazo }, dto);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, [FromBody] ETareasJustificacionRechazo entity)
        // {
        //     ETareasJustificacionRechazo findRechazo = await _repository.GetByIdAsync(id);
        //
        //     if (findRechazo != null)
        //     {
        //         findRechazo.CN_Id_tarea = entity.CN_Id_tarea;
        //         findRechazo.CF_Fecha_hora_rechazo = entity.CF_Fecha_hora_rechazo;
        //         findRechazo.CT_Descripcion_rechazo = entity.CT_Descripcion_rechazo;
        //
        //         await _repository.UpdateAsync(findRechazo);
        //         return NoContent();
        //     }
        //
        //     return NotFound("El id de la justificación de rechazo no existe");
        // }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TareasJustificacionRechazoDto dto)
        {
            if (id != dto.CN_Id_tarea_rechazo) return BadRequest("Id no existe");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound($"No se encontró la justificacion del rechazo con ID {id}");

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
        
        
        private TareasJustificacionRechazoDto MapToDto(ETareasJustificacionRechazo entity)
        {
            return new TareasJustificacionRechazoDto
            {
                CN_Id_tarea_rechazo = entity.CN_Id_tarea_rechazo,
                CN_Id_tarea = entity.CN_Id_tarea,
                CF_Fecha_hora_rechazo = entity.CF_Fecha_hora_rechazo,
                CT_Descripcion_rechazo = entity.CT_Descripcion_rechazo,
                Tareas = entity.Tareas != null
                    ? new TareasDto
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
                    }
                    : null
            };
        }
        
        private ETareasJustificacionRechazo MapToEntity(TareasJustificacionRechazoDto dto) => new ETareasJustificacionRechazo()
        {
            CN_Id_tarea_rechazo = dto.CN_Id_tarea_rechazo,
            CN_Id_tarea = dto.CN_Id_tarea,
            CF_Fecha_hora_rechazo = dto.CF_Fecha_hora_rechazo,
            CT_Descripcion_rechazo = dto.CT_Descripcion_rechazo
        };
        
        private static void MapUpdateFields(ETareasJustificacionRechazo entity, TareasJustificacionRechazoDto dto)
        {
            // entity.CN_Id_tarea = dto.CN_Id_tarea;
            entity.CF_Fecha_hora_rechazo = dto.CF_Fecha_hora_rechazo;
            entity.CT_Descripcion_rechazo = dto.CT_Descripcion_rechazo;
            // entity.CN_Id_tarea_rechazo = dto.CN_Id_tarea_rechazo;
        }

    }

}
