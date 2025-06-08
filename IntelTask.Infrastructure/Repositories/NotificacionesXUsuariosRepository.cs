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
    public class NotificacionesXUsuariosRepository : INotificacionesXUsuariosRepository
    {
        private readonly IntelTaskDbContext _context;

        //Constructor de la clase
        public NotificacionesXUsuariosRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ENotificacionesXUsuarios>> GetAllAsync()
        {
            return await _context.T_Notificaciones_X_Usuarios.ToListAsync();
        }

        public async Task<ENotificacionesXUsuarios?> GetByIdAsync(int id)
        {
            return await _context.T_Notificaciones_X_Usuarios.FindAsync(id);
        }

        //Guarda
        public async Task AddAsync(ENotificacionesXUsuarios notificaciones)
        {
            await _context.T_Notificaciones_X_Usuarios.AddAsync(notificaciones);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(ENotificacionesXUsuarios notificaciones)
        {
            _context.T_Notificaciones_X_Usuarios.Update(notificaciones);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Notificaciones_X_Usuarios.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Notificaciones_X_Usuarios.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

    }
}
