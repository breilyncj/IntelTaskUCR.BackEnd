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
    public class OficinasRepository : IOficinasRepository
    {

        private readonly IntelTaskDbContext _context;

        public OficinasRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EOficinas oficinas)
        {
            await _context.T_Oficinas.AddAsync(oficinas);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Oficinas.FindAsync(id);

            if (entidad != null)
            {

                _context.T_Oficinas.Remove(entidad);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<EOficinas>> GetAllAsync()
        {
            return await _context.T_Oficinas.ToListAsync();
        }

        public async Task<EOficinas?> GetByIdAsync(int id)
        {
            return await _context.T_Oficinas.FindAsync(id);
        }

        public async Task UpdateAsync(EOficinas roles)
        {
            _context.T_Oficinas.Update(roles);
            await _context.SaveChangesAsync();
        }
    }
}
