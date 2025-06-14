using IntelTaskUCR.API.DTOs;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        
        public AuthController(IAuthRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _repository.Login(dto.Correo, dto.Contrasenna);

            if (!result.Success)
            {
                if (result.Message == "Correo no registrado.")
                {
                    return NotFound(new { result.Message });
                }

                // Por seguridad podrías devolver 401 sin decir qué falló
                return Unauthorized(new { result.Message });
            }

            return Ok(new
            {
                result.Message,
                Usuario = result.Usuario
            });
        }


        // private EUsuarios MapToEntity(LoginDto dto) => new EUsuarios
        // {
        //     CN_Id_usuario = dto.CN_Id_usuario,
        //     CN_Id_rol = dto.CN_Id_rol,
        //     CT_Contrasenna = dto.CT_Contrasenna,
        //     CT_Correo_usuario = dto.CT_Correo_usuario,
        //     CT_Nombre_usuario = dto.CT_Nombre_usuario,
        //     CF_Fecha_creacion_usuario = dto.CF_Fecha_creacion_usuario,
        //     CF_Fecha_nacimiento = dto.CF_Fecha_nacimiento,
        //     CF_Fecha_modificacion_usuario = dto.CF_Fecha_modificacion_usuario,
        //     CB_Estado_usuario = dto.CB_Estado_usuario
        // };
    }
}
