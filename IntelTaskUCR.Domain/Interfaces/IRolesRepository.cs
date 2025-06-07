using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IRolesRepository
    {

        Task<IEnumerable<ERoles>> GetAllAsync();

        Task<ERoles?> GetByIdAsync(int id);

        Task AddAsync(ERoles roles);

        Task UpdateAsync(ERoles roles);

        Task DeleteAsync(int id);

    }
}
