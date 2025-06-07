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
    public class EstadoRepository : IEstadosRepository
    {
        private readonly IntelTaskDbContext _context;

        //Constructor de la clase
        public EstadoRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EEstados>> GetAllAsync()
        {
            return await _context.T_Estados.ToListAsync();
        }

        public async Task<EEstados?> GetByIdAsync(byte id)
        {
            return await _context.T_Estados.FindAsync(id);
        }

        //Guarda
        public async Task AddAsync(EEstados estado)
        {
            await _context.T_Estados.AddAsync(estado);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(EEstados estado)
        {
            _context.T_Estados.Update(estado);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(byte id)
        {
            var entidad = await _context.T_Estados.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Estados.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TieneTareasAsync(byte idEstado)
        {
            return await _context.T_Tareas.AnyAsync(t => t.CN_Id_estado == idEstado);

        }

    }
}
