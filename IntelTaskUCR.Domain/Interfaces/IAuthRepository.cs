using IntelTaskUCR.Domain.Entities;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResult> Login(string correo, string password);
    }
}
