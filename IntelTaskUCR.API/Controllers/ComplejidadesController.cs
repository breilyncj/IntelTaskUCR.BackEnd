using IntelTaskUCR.API.DTOs;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplejidadesController : ControllerBase
    {
        private readonly IComplejidadesRepository _repository;
        public ComplejidadesController(IComplejidadesRepository repository) { _repository = repository; }

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     var items = await _repository.GetAllAsync();
        //     return Ok(items);
        // }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var complejidades = await _repository.GetAllAsync();
            return Ok(complejidades);
        }
        
        [HttpGet("withTareas")]
        public async Task<IActionResult> GetAllWithTareas()
        {
            var complejidades = await _repository.GetAllWithTareasAsync();
            var dtoList = complejidades.Select(MapToDto);
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
            var complejidades = await _repository.GetByIdWithTareasAsync(id);

            if (complejidades == null) return NotFound();
            
            var dto = MapToDto(complejidades);
            return Ok(dto);
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] EComplejidades entity)
        // {
        //     await _repository.AddAsync(entity);
        //     return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_complejidad }, entity);
        // }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComplejidadesDto dto)
        {
            var entity = MapToEntity(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_complejidad }, dto);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(byte id, [FromBody] EComplejidades entity)
        // {
        //
        //     EComplejidades findComplejidad = await _repository.GetByIdAsync(id);
        //
        //     if (findComplejidad != null)
        //     {
        //         //findUser.CN_Id_complejidad = entity.CN_Id_complejidad; 
        //         findComplejidad.CT_Nombre = entity.CT_Nombre;
        //
        //         await _repository.UpdateAsync(findComplejidad);
        //         return NoContent();
        //
        //     }
        //     else
        //     {
        //         return NotFound("El id de la complejidad no existe");
        //     }
        // }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id, [FromBody] ComplejidadesDto dto)
        {
            if (id != dto.CN_Id_complejidad) return BadRequest("Id no existe");

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return NotFound($"No se encontro la complejidad con la ID {id}");
            
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
        //         return BadRequest("No se puede eliminar la complejidad porque está asociada a una o más tareas.");
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

        private ComplejidadesDto MapToDto(EComplejidades c) => new ComplejidadesDto
        {
            CN_Id_complejidad = c.CN_Id_complejidad,
            CT_Nombre = c.CT_Nombre,
            Tareas = c.Tareas?.Select(tareas => new TareasDto
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

        private EComplejidades MapToEntity(ComplejidadesDto dto) => new EComplejidades
        {
            CN_Id_complejidad = dto.CN_Id_complejidad,
            CT_Nombre = dto.CT_Nombre
        };

        private static void MapUpdateFields(EComplejidades entity, ComplejidadesDto dto)
        {
            entity.CT_Nombre = dto.CT_Nombre;
        }
        
    }
}
