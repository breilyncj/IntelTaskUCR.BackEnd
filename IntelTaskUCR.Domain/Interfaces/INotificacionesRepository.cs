using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface INotificacionesRepository
    {
        Task<IEnumerable<ENotificaciones>> GetAllAsync();

        Task<ENotificaciones?> GetByIdAsync(int id);

        Task AddAsync(ENotificaciones notificaciones);

        Task UpdateAsync(ENotificaciones notificaciones);

        Task DeleteAsync(int id);

        Task<int> CrearNotificacionParaUsuarios(ENotificaciones notificacion, List<ENotificacionesXUsuarios> usuariosRelacionados);

        Task<IEnumerable<ENotificaciones>> GetByUsuarioYTipoAsync(int usuarioId, int tipoNotificacion);

    }
}
