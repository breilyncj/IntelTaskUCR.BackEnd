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
    public class ComplejidadesRepository : IComplejidadesRepository
    {
        private readonly IntelTaskDbContext _context;

        //Constructor de la clase
        public ComplejidadesRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EComplejidades>> GetAllAsync()
        {
            return await _context.T_Complejidades.ToListAsync();
        }

        public async Task<EComplejidades?> GetByIdAsync(byte id)
        {
            return await _context.T_Complejidades.FindAsync(id);
        }

        //Guarda
        public async Task AddAsync(EComplejidades complejidad)
        {
            await _context.T_Complejidades.AddAsync(complejidad);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(EComplejidades complejidad)
        {
            _context.T_Complejidades.Update(complejidad);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(byte id)
        {
            var entidad = await _context.T_Complejidades.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Complejidades.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TieneTareasAsync(byte idComplejidad)
        {
            return await _context.T_Tareas.AnyAsync(t => t.CN_Id_complejidad == idComplejidad);
        
        }
    }
}
