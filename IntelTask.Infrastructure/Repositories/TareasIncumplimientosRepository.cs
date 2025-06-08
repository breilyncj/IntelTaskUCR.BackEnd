using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using IntelTaskUCR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class TareasIncumplimientosRepository : ITareasIncumplimientosRepository
    {
        
        private readonly IntelTaskDbContext _context;

        public TareasIncumplimientosRepository(IntelTaskDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ETareasIncumplimientos>> GetAllAsync()
        {
            return await _context.T_Tareas_Incumplimientos.ToListAsync();
        }
        public async Task<IEnumerable<ETareasIncumplimientos>> GetAllWithTareasAsync()
        {
            return await _context.T_Tareas_Incumplimientos
                .Include(i => i.Tareas)
                .ToListAsync();
        }

        public async Task<ETareasIncumplimientos?> GetByIdAsync(int id)
        {
            return await _context.T_Tareas_Incumplimientos.FindAsync(id);
        }
        public async Task<ETareasIncumplimientos?> GetByIdWithTareasAsync(int id)
        {
            return await _context.T_Tareas_Incumplimientos
                .Include(i => i.Tareas)
                .FirstOrDefaultAsync(i => i.CN_Id_tarea_incumplimiento == id);
        }
        
        public async Task AddAsync(ETareasIncumplimientos tareaIncumplimientos)
        {
            await _context.T_Tareas_Incumplimientos.AddAsync(tareaIncumplimientos);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ETareasIncumplimientos tareaIncumplimientos)
        {
            _context.T_Tareas_Incumplimientos.Update(tareaIncumplimientos);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int idTareasIncumplimientos)
        {
            var entidad = await _context.T_Tareas_Incumplimientos.FindAsync(idTareasIncumplimientos);
            
            if (entidad != null)
            {
                _context.T_Tareas_Incumplimientos.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
