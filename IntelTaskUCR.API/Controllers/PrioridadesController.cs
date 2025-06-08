using IntelTaskUCR.API.DTOs;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PrioridadesController : ControllerBase
    {

        private readonly IPrioridadesRepository _repository;

        public PrioridadesController(IPrioridadesRepository repository)
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
            var prioridades = await _repository.GetAllAsync();
            var dtoList = prioridades.Select(MapToDto);
            return Ok(dtoList);
        }
        
        [HttpGet("WithTareas")]
        public async Task<IActionResult> GetAllWithTareas()
        {
            var prioridades = await _repository.GetAllWithTareasAsync();
            var dtoList = prioridades.Select(MapToDto);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(byte id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }
        
        [HttpGet("withTarea/{id}")]
        public async Task<IActionResult> GetWithTarea(byte id)
        {
            var prioridades = await _repository.GetByIdWithTareasAsync(id);
            if (prioridades == null) return NotFound();
            var dto = MapToDto(prioridades);
            return Ok(dto);
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] EPrioridades entity)
        // {
        //     await _repository.AddAsync(entity);
        //     return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_prioridad }, entity);
        // }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrioridadesDTO dto)
        {
            var entity = MapToEntity(dto);

            await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_prioridad }, dto);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(byte id, [FromBody] EPrioridades entity)
        // {
        //
        //     EPrioridades findPrioridades = await _repository.GetByIdAsync(id);
        //
        //     if (findPrioridades != null)
        //     { 
        //         findPrioridades.CN_Id_prioridad = entity.CN_Id_prioridad;
        //         findPrioridades.CT_Nombre_prioridad = entity.CT_Nombre_prioridad;
        //         findPrioridades.CT_Descripcion_prioridad = entity.CT_Descripcion_prioridad;
        //
        //         await _repository.UpdateAsync(findPrioridades);
        //         return NoContent();
        //
        //     }
        //     else
        //     {
        //         return NotFound("El id de la prioridad no existe");
        //     }
        // }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id, [FromBody] PrioridadesDTO dto)
        {
            if (id != dto.CN_Id_prioridad) return BadRequest("Id no existe");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound($"No se encontró la prioridad con ID {id}");

            // Actualizamos solo lo permitido
            MapUpdateFields(entity, dto);
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(byte id)
        // {
        //     bool tieneTareas = await _repository.TieneTareasAsync(id);
        //
        //     if (tieneTareas)
        //         return BadRequest("No se puede eliminar la prioridad porque está asociada a una o más tareas.");
        //
        //     await _repository.DeleteAsync(id);
        //     return NoContent();
        // }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private PrioridadesDTO MapToDto(EPrioridades p) => new PrioridadesDTO
        {
            CN_Id_prioridad = p.CN_Id_prioridad,
            CT_Nombre_prioridad = p.CT_Nombre_prioridad,
            CT_Descripcion_prioridad = p.CT_Descripcion_prioridad,
            Tareas = p.Tareas?.Select(tareas => new TareasDto
            {
                CN_Id_tarea = tareas.CN_Id_tarea,
                CN_Tarea_origen = tareas.CN_Tarea_origen,
                CT_Titulo_tarea = tareas.CT_Titulo_tarea,
                CT_Descripcion_tarea = tareas.CT_Descripcion_tarea,
                CT_Descripcion_espera = tareas.CT_Descripcion_espera,
                CN_Id_complejidad = tareas.CN_Id_complejidad,
                CN_Id_estado = tareas.CN_Id_estado,
                CN_Id_prioridad = tareas.CN_Id_prioridad,
                CN_Numero_GIS = tareas.CN_Numero_GIS,
                CF_Fecha_asignacion = tareas.CF_Fecha_asignacion,
                CN_Usuario_asignado = tareas.CN_Usuario_asignado,
                CN_Usuario_creador = tareas.CN_Usuario_creador,
                CF_Fecha_finalizacion = tareas.CF_Fecha_finalizacion,
                CF_Fecha_limite = tareas.CF_Fecha_limite,
                
            }).ToList() ?? new List<TareasDto>()
        };

        private EPrioridades MapToEntity(PrioridadesDTO dto) => new EPrioridades
        {
            CN_Id_prioridad = dto.CN_Id_prioridad,
            CT_Nombre_prioridad = dto.CT_Nombre_prioridad,
            CT_Descripcion_prioridad = dto.CT_Descripcion_prioridad
        };

        private static void MapUpdateFields(EPrioridades entity, PrioridadesDTO dto)
        {
            entity.CT_Nombre_prioridad = dto.CT_Nombre_prioridad;
            entity.CT_Descripcion_prioridad = dto.CT_Descripcion_prioridad;
        }

    }
}
