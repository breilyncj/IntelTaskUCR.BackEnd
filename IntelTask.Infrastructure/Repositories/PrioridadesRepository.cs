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
    public class PrioridadesRepository : IPrioridadesRepository
    {

        private readonly IntelTaskDbContext _context;

        //Constructor de la clase
        public PrioridadesRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EPrioridades>> GetAllAsync()
        {
            return await _context.T_Prioridades.ToListAsync();
        }

        public async Task<EPrioridades?> GetByIdAsync(byte id)
        {
            return await _context.T_Prioridades.FindAsync(id);
        }

        //Guarda
        public async Task AddAsync(EPrioridades prioridades)
        {
            await _context.T_Prioridades.AddAsync(prioridades);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(EPrioridades prioridades)
        {
            _context.T_Prioridades.Update(prioridades);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(byte id)
        {
            var entidad = await _context.T_Prioridades.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Prioridades.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TieneTareasAsync(byte idPrioridad)
        {
            return await _context.T_Tareas.AnyAsync(t => t.CN_Id_prioridad == idPrioridad);

        }

    }
}
