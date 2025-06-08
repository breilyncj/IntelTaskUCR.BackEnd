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
    public class NotificacionesRepository : INotificacionesRepository
    {
        private readonly IntelTaskDbContext _context;

        //Constructor de la clase
        public NotificacionesRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ENotificaciones>> GetAllAsync()
        {
            return await _context.T_Notificaciones.ToListAsync();
        }

        public async Task<ENotificaciones?> GetByIdAsync(int id)
        {
            return await _context.T_Notificaciones.FindAsync(id);
        }

        //Guarda
        public async Task AddAsync(ENotificaciones notificaciones)
        {
            await _context.T_Notificaciones.AddAsync(notificaciones);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(ENotificaciones notificaciones)
        {
            _context.T_Notificaciones.Update(notificaciones);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Notificaciones.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Notificaciones.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
