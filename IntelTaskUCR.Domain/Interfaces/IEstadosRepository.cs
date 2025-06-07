using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IEstadosRepository
    {
        Task<IEnumerable<EEstados>> GetAllAsync();

        Task<EEstados?> GetByIdAsync(byte id);

        Task AddAsync(EEstados estado);

        Task UpdateAsync(EEstados estado);

        Task DeleteAsync(byte id);

        Task<bool> TieneTareasAsync(byte id);
    }
}
