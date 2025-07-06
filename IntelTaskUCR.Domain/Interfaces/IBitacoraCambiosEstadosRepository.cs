using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IBitacoraCambiosEstadosRepository
    {
        Task<IEnumerable<EBitacoraCambiosEstados>> GetAllAsync();

        Task<EBitacoraCambiosEstados?> GetByIdAsync(int id);

        Task AddAsync(EBitacoraCambiosEstados bitacora);

        Task UpdateAsync(EBitacoraCambiosEstados bitacora);

        Task DeleteAsync(int id);
    }
}
