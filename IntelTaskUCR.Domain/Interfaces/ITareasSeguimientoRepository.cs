using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface ITareasSeguimientoRepository
    {
        Task<IEnumerable<ETareasSeguimiento>> GetAllAsync();

        Task<IEnumerable<ETareasSeguimiento>> GetAllWithTareasAsync();

        Task<ETareasSeguimiento?> GetByIdAsync(int id);

        Task<ETareasSeguimiento?> GetByIdWithTareasAsync(int id);

        Task AddAsync(ETareasSeguimiento tarea);

        Task UpdateAsync(ETareasSeguimiento tarea);

        Task DeleteAsync(int id);
    }
}
