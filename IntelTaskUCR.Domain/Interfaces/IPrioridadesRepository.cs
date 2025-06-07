using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IPrioridadesRepository
    {
        Task<IEnumerable<EPrioridades>> GetAllAsync();

        Task<EPrioridades?> GetByIdAsync(byte id);

        Task AddAsync(EPrioridades prioridades);

        Task UpdateAsync(EPrioridades prioridades);

        Task DeleteAsync(byte id);

        Task<bool> TieneTareasAsync(byte idPrioridad);
    }
}
