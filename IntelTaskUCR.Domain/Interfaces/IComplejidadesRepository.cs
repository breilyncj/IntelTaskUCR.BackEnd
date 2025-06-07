using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IComplejidadesRepository
    {
        Task<IEnumerable<EComplejidades>> GetAllAsync();

        Task<EComplejidades?> GetByIdAsync(byte id);

        Task AddAsync(EComplejidades complejidad);

        Task UpdateAsync(EComplejidades complejidad);

        Task DeleteAsync(byte id);

        Task<bool> TieneTareasAsync(byte idComplejidad);

    }
}
