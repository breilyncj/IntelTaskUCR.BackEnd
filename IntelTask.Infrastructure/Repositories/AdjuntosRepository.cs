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
    public class AdjuntosRepository : IAdjuntosRepository
    {
        private readonly IntelTaskDbContext _context;

        public AdjuntosRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EAdjuntos>> GetAllAsync()
        {
            return await _context.T_Adjuntos.ToListAsync();
        }

        public async Task<EAdjuntos?> GetByIdAsync(int id)
        {
            return await _context.T_Adjuntos.FindAsync(id);
        }

        //Guarda
        public async Task AddAsync(EAdjuntos adjunto)
        {
            await _context.T_Adjuntos.AddAsync(adjunto);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(EAdjuntos adjunto)
        {
            _context.T_Adjuntos.Update(adjunto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Adjuntos.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Adjuntos.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
