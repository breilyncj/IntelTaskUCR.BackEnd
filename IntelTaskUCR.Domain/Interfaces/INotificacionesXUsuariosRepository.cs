using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface INotificacionesXUsuariosRepository
    {
        Task<IEnumerable<ENotificacionesXUsuarios>> GetAllAsync();

        Task<ENotificacionesXUsuarios?> GetByIdAsync(int id);

        Task AddAsync(ENotificacionesXUsuarios notificacionesXUsuarios);

        Task UpdateAsync(ENotificacionesXUsuarios notificacionesXUsuarios);

        Task DeleteAsync(int id);
    }
}
