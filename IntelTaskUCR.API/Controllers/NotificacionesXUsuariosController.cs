using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesXUsuariosController : ControllerBase
    {
        private readonly INotificacionesXUsuariosRepository _repository;

        public NotificacionesXUsuariosController(INotificacionesXUsuariosRepository repository)
        {
            _repository = repository;
        }


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
        public async Task<IActionResult> Create([FromBody] ENotificacionesXUsuarios entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_notificacion }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ENotificacionesXUsuarios entity)
        {
            ENotificacionesXUsuarios notificacionExistente = await _repository.GetByIdAsync(id);

            if (notificacionExistente != null)
            {
                notificacionExistente.CN_Id_usuario = entity.CN_Id_usuario;
                notificacionExistente.CT_Correo_destino = entity.CT_Correo_destino;

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
