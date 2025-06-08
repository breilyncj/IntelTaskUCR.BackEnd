using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using IntelTaskUCR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class TareasJustificacionRechazoRepository : ITareasJustificacionRechazoRepository
    {
        private readonly IntelTaskDbContext _context;

        public TareasJustificacionRechazoRepository(IntelTaskDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ETareasJustificacionRechazo>> GetAllAsync()
        {
            return await _context.T_Tareas_Justificacion_Rechazo.ToListAsync();
        }
        public async Task<IEnumerable<ETareasJustificacionRechazo>> GetAllWithTareasAsync()
        {
            return await _context.T_Tareas_Justificacion_Rechazo
                .Include(i => i.Tareas)
                .ToListAsync();
        }
        public async Task<ETareasJustificacionRechazo?> GetByIdWithTareasAsync(int id)
        {
            return await _context.T_Tareas_Justificacion_Rechazo
                .Include(i => i.Tareas)
                .FirstOrDefaultAsync(i => i.CN_Id_tarea_rechazo == id);
        }
        public async Task<ETareasJustificacionRechazo?> GetByIdAsync(int id)
        {
            return await _context.T_Tareas_Justificacion_Rechazo.FindAsync(id);
        }
        public async Task AddAsync(ETareasJustificacionRechazo tareasJustificacionRechazo)
        {
            await _context.T_Tareas_Justificacion_Rechazo.AddAsync(tareasJustificacionRechazo);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ETareasJustificacionRechazo tareasJustificacionRechazo)
        {
            _context.T_Tareas_Justificacion_Rechazo.Update(tareasJustificacionRechazo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int idTareasIncumplimientos)
        {
            var entidad = await _context.T_Tareas_Justificacion_Rechazo.FindAsync(idTareasIncumplimientos);

            if (entidad != null)
            {
                _context.T_Tareas_Justificacion_Rechazo.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

    }
}
