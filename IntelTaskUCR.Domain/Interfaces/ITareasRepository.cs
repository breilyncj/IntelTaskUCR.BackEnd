using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface ITareasRepository
    {
        Task<IEnumerable<ETareas>> GetAllAsync();

        Task<ETareas?> GetByIdAsync(int id);
        
        Task<ETareas?> GetByIdWithIncumplimientoAsync(int id);

        Task AddAsync(ETareas tarea);

        Task UpdateAsync(ETareas tarea);

        Task DeleteAsync(int id);
    }
}
