using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface IAdjuntosRepository
    {
        Task<IEnumerable<EAdjuntos>> GetAllAsync();

        Task<EAdjuntos> GetByIdAsync(int id);

        Task AddAsync(EAdjuntos adjunto);

        Task UpdateAsync(EAdjuntos adjunto);

        Task DeleteAsync(int id);

        Task AgregarAsync(EAdjuntos adjunto);
    }
}
