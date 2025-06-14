using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using IntelTaskUCR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IntelTaskDbContext _context;
        
        private readonly IUsuariosRepository _usuariosRepository;
        
        public AuthRepository(IntelTaskDbContext context, IUsuariosRepository usuariosRepository)
        {
            _context = context;
            _usuariosRepository = usuariosRepository;
        }
        
        public async Task<LoginResult> Login(string correo, string password)
        {
            var usuario = await _usuariosRepository.GetByCorreo(correo);

            if (usuario == null)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Correo no registrado."
                };
            }

            if (!usuario.CT_Contrasenna.Equals(password))
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Contraseña incorrecta."
                };
            }

            return new LoginResult
            {
                Success = true,
                Message = "Inicio de sesión exitoso.",
                Usuario = usuario
            };
        }

    }
}
