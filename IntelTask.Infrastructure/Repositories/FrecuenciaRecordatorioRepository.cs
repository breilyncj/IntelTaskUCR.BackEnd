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
    public class FrecuenciaRecordatorioRepository : IFrecuenciaRecordatorioRepository
    {

        private readonly IntelTaskDbContext _context;

        public FrecuenciaRecordatorioRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EFrecuenciaRecordatorio frecuenciaRecordatorio)
        {
            await _context.T_Frecuecia_Recordatorio.AddAsync(frecuenciaRecordatorio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Frecuecia_Recordatorio.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Frecuecia_Recordatorio.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<EFrecuenciaRecordatorio>> GetAllAsync()
        {
            return await _context.T_Frecuecia_Recordatorio.ToListAsync();
        }

        public async Task<EFrecuenciaRecordatorio?> GetByIdAsync(int id)
        {
            return await _context.T_Frecuecia_Recordatorio.FindAsync(id);
        }

        public async Task UpdateAsync(EFrecuenciaRecordatorio frecuenciaRecordatorio)
        {
            _context.T_Frecuecia_Recordatorio.Update(frecuenciaRecordatorio);
            await _context.SaveChangesAsync();
        }
    }
}
