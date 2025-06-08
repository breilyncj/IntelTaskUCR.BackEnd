using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface ITareasJustificacionRechazoRepository
    {
        Task<IEnumerable<ETareasJustificacionRechazo>> GetAllAsync();

        Task<ETareasJustificacionRechazo?> GetByIdAsync(int id);

        Task AddAsync(ETareasJustificacionRechazo tareasJustificacionRechazo);

        Task UpdateAsync(ETareasJustificacionRechazo tareasJustificacionRechazo);

        Task DeleteAsync(int idTareasJustificacionRechazo);
    }
}
