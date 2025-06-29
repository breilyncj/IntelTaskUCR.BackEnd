using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<EUsuarios>> GetAllAsync();
        
        Task<IEnumerable<EUsuarios>> GetAllWithRelacionesAsync();
        
        Task<EUsuarios?> GetByIdWithTareasAsignadasAsync(int id);
        
        Task<EUsuarios?> GetByIdWithTareasCreadorAsync(int id);

        Task<EUsuarios?> GetByIdAsync(int id);
        
        Task<EUsuarios?> GetByIdWithFrecuenciaRecordatorioAsync(int id);
        
        Task<EUsuarios?> GetByCorreo(string correo);

        Task AddAsync(EUsuarios usuarios);

        Task UpdateAsync(EUsuarios usuarios);

        Task DeleteAsync(int id);

        Task<IEnumerable<ENotificaciones>> GetNotificacionesDeUsuario(int idUsuario);
        Task<IEnumerable<ENotificaciones>> GetNotificacionesEnviadasPor(string correoOrigen);
        Task<IEnumerable<ENotificaciones>> GetNotificacionesRecibidasPor(int idUsuario);

    }
}
