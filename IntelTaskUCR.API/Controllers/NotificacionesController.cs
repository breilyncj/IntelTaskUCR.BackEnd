using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionesRepository _repository;
        public NotificacionesController(INotificacionesRepository repository) { _repository = repository; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ENotificaciones entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_notificacion }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ENotificaciones entity)
        {
            ENotificaciones notificacionExistente = await _repository.GetByIdAsync(id);

            if (notificacionExistente != null)
            {
                notificacionExistente.CN_Tipo_notificacion = entity.CN_Tipo_notificacion;
                notificacionExistente.CT_Titulo_notificacion = entity.CT_Titulo_notificacion;
                notificacionExistente.CT_Texto_notificacion = entity.CT_Texto_notificacion;
                notificacionExistente.CT_Correo_origen = entity.CT_Correo_origen;
                notificacionExistente.CF_Fecha_registro = entity.CF_Fecha_registro;
                notificacionExistente.CF_Fecha_notificacion = entity.CF_Fecha_notificacion;
                notificacionExistente.CN_Id_recordatorio = entity.CN_Id_recordatorio;

                await _repository.UpdateAsync(notificacionExistente);
                return NoContent();
            }
            else
            {
                return NotFound("El id de la notificación no existe");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
