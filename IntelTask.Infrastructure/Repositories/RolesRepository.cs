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
    public class RolesRepository : IRolesRepository
    {

        private readonly IntelTaskDbContext _context;

        public RolesRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ERoles roles)
        {
            await _context.T_Roles.AddAsync(roles);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Roles.FindAsync(id);

            if (entidad != null) {

                _context.T_Roles.Remove(entidad);
                await _context.SaveChangesAsync();
            
            }
        }

        public async Task<IEnumerable<ERoles>> GetAllAsync()
        {
            return await _context.T_Roles.ToListAsync();
        }

        public async Task<ERoles?> GetByIdAsync(int id)
        {
            return await _context.T_Roles.FindAsync(id);
        }

        public async Task UpdateAsync(ERoles roles)
        {
            _context.T_Roles.Update(roles);
            await _context.SaveChangesAsync();
        }
    }
}
