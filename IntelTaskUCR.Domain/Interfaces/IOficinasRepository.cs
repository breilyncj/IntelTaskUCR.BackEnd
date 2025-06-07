using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IOficinasRepository
    {
        Task<IEnumerable<EOficinas>> GetAllAsync();

        Task<EOficinas?> GetByIdAsync(int id);

        Task AddAsync(EOficinas oficinas);

        Task UpdateAsync(EOficinas oficinas);

        Task DeleteAsync(int id);

    }
}
