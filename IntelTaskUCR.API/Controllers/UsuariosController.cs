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
        public async Task<IActionResult> Create([FromBody] EUsuarios entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_usuario }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EUsuarios entity)
        {

            EUsuarios findUser = await _repository.GetByIdAsync(id);

            if (findUser != null) {

                findUser.CN_Id_usuario = entity.CN_Id_usuario; // Si hubiera incremento en la bd esta linea se ignora
                findUser.CT_Nombre_usuario = entity.CT_Nombre_usuario;
                findUser.CT_Correo_usuario = entity.CT_Correo_usuario;
                findUser.CF_Fecha_nacimiento = entity.CF_Fecha_nacimiento;
                findUser.CT_Contrasenna = entity.CT_Contrasenna;
                findUser.CB_Estado_usuario = entity.CB_Estado_usuario;
                findUser.CF_Fecha_creacion_usuario = entity.CF_Fecha_creacion_usuario;
                findUser.CF_Fecha_modificacion_usuario = entity.CF_Fecha_modificacion_usuario;
                findUser.CN_Id_rol = entity.CN_Id_rol;

                await _repository.UpdateAsync(findUser);
                return NoContent();
            
            } else
            {
                return NotFound("El id del usuario no existe");
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
