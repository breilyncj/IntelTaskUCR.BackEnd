using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<EUsuarios>> GetAllAsync();
        
        Task<IEnumerable<EUsuarios>> GetAllWithRelacionesAsync();

        Task<EUsuarios?> GetByIdAsync(int id);
        
        Task<EUsuarios?> GetByIdWithFrecuenciaRecordatorioAsync(int id);

        Task AddAsync(EUsuarios usuarios);

        Task UpdateAsync(EUsuarios usuarios);

        Task DeleteAsync(int id);

    }
}
