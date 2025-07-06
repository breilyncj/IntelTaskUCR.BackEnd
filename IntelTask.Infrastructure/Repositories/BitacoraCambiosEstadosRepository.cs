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
    public class BitacoraCambiosEstadosRepository : IBitacoraCambiosEstadosRepository
    {
        private readonly IntelTaskDbContext _context;

        //Constructor de la clase
        public BitacoraCambiosEstadosRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EBitacoraCambiosEstados>> GetAllAsync()
        {
            return await _context.T_Bitacora_Cambios_Estados.ToListAsync();
        }

        public async Task<EBitacoraCambiosEstados?> GetByIdAsync(int id)
        {
            return await _context.T_Bitacora_Cambios_Estados.FindAsync(id);
        }

        //Guarda
        public async Task AddAsync(EBitacoraCambiosEstados bitacora)
        {
            await _context.T_Bitacora_Cambios_Estados.AddAsync(bitacora);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(EBitacoraCambiosEstados bitacora)
        {
            _context.T_Bitacora_Cambios_Estados.Update(bitacora);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Bitacora_Cambios_Estados.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Bitacora_Cambios_Estados.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
