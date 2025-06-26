using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IAdjuntosXTareasRepository
    {
        Task<IEnumerable<EAdjuntosXTareas>> GetAllAsync();

        Task<EAdjuntosXTareas?> GetByIdsAsync(int idAdjunto, int idTarea);

        Task AddAsync(EAdjuntosXTareas adjunto);

        Task UpdateAsync(EAdjuntosXTareas adjunto);

        Task DeleteAsync(int idAdjunto, int idTarea);
    }
}
