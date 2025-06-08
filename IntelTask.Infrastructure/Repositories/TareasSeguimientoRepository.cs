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
    public class TareasSeguimientoRepository : ITareasSeguimientoRepository
    {

        private readonly IntelTaskDbContext _context;

        public TareasSeguimientoRepository(IntelTaskDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ETareasSeguimiento>> GetAllAsync()
        {
            return await _context.T_Tareas_Seguimiento.ToArrayAsync();
        }
        public async Task<ETareasSeguimiento?> GetByIdAsync(int id)
        {
            return await _context.T_Tareas_Seguimiento.FindAsync(id);
        }
        public async Task AddAsync(ETareasSeguimiento tareaSeguimiento)
        {
            await _context.T_Tareas_Seguimiento.AddAsync(tareaSeguimiento);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ETareasSeguimiento tareaSeguimiento)
        {
            _context.T_Tareas_Seguimiento.Update(tareaSeguimiento);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Tareas_Seguimiento.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Tareas_Seguimiento.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

    }
}
