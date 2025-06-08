using IntelTaskUCR.API.DTOs;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosController : ControllerBase
    {
        private readonly IEstadosRepository _repository;
        public EstadosController(IEstadosRepository repository) { _repository = repository; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("with_tareas")]
        public async Task<IActionResult> GetAllWithTareas()
        {
            var estados = await _repository.GetAllWithTareasAsync();
            var dtoList = estados.Select(MapToDto);
            return Ok(dtoList);
        }

        [HttpGet("with_tarea/{id}")]
        public async Task<IActionResult> GetWithTarea(byte id)
        {
            var item = await _repository.GetByIdWithTareasAsync(id);
            if (item == null) return NotFound();
            var dto = MapToDto(item);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(byte id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EstadosDto dto)
        {
            var entity = MapToEntity(dto);

            await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_estado }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id, [FromBody] EstadosDto dto)
        {
            if (id != dto.CN_Id_estado) return BadRequest("Id no existe");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound($"No se encontró el incumplimiento con ID {id}");

            // Actualizamos solo lo permitido
            MapUpdateFields(entity, dto);
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }


        private EstadosDto MapToDto(EEstados entity)
        {
            return new EstadosDto
            {
                CN_Id_estado = entity.CN_Id_estado,
                CT_Estado = entity.CT_Estado,
                CT_Descripcion = entity.CT_Descripcion,
                Tareas = entity.Tareas?.Select(tareas => new TareasDto
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
                    CF_Fecha_limite = tareas.CF_Fecha_limite,
                    CF_Fecha_finalizacion = tareas.CF_Fecha_finalizacion,
                    CN_Usuario_creador = tareas.CN_Usuario_creador,
                    CN_Usuario_asignado = tareas.CN_Usuario_asignado
                }).ToList() ?? new List<TareasDto>()
            };
        }

        private EEstados MapToEntity(EstadosDto dto) => new EEstados
        {
            CN_Id_estado = dto.CN_Id_estado,
            CT_Estado = dto.CT_Estado,
            CT_Descripcion = dto.CT_Descripcion
        };

        private static void MapUpdateFields(EEstados entity, EstadosDto dto)
        {
      
            entity.CT_Estado = dto.CT_Estado;
            entity.CT_Descripcion = dto.CT_Descripcion;
        }

    }
}
