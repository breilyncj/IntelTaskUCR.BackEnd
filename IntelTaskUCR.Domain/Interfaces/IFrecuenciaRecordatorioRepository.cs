using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IFrecuenciaRecordatorioRepository
    {

        Task<IEnumerable<EFrecuenciaRecordatorio>> GetAllAsync();

        Task<EFrecuenciaRecordatorio?> GetByIdAsync(int id);

        Task AddAsync(EFrecuenciaRecordatorio frecuenciaRecordatorio);

        Task UpdateAsync(EFrecuenciaRecordatorio frecuenciaRecordatorio);

        Task DeleteAsync(int id);

    }
}
