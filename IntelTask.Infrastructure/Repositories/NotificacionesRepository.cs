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
            int ultimoId = 0;
            if (await _context.T_Notificaciones.AnyAsync())
            {
                ultimoId = await _context.T_Notificaciones.MaxAsync(n => n.CN_Id_notificacion);
            }
            notificaciones.CN_Id_notificacion = ultimoId + 1;

            // PASO 2: Agregar y guardar
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

        public async Task<int> CrearNotificacionParaUsuarios(ENotificaciones notificacion, List<ENotificacionesXUsuarios> usuariosRelacionados)
        {
            // 1. Asignar manualmente el ID de la notificación (ejemplo: max ID + 1)
            int maxId = await _context.T_Notificaciones.MaxAsync(n => (int?)n.CN_Id_notificacion) ?? 0;
            notificacion.CN_Id_notificacion = maxId + 1;

            // 2. Agregar la notificación
            _context.T_Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();

            // 3. Asignar el ID de notificación a cada usuario relacionado y agregarlos
            foreach (var usuarioNotificacion in usuariosRelacionados)
            {
                usuarioNotificacion.CN_Id_notificacion = notificacion.CN_Id_notificacion;
                _context.T_Notificaciones_X_Usuarios.Add(usuarioNotificacion);
            }

            await _context.SaveChangesAsync();

            return notificacion.CN_Id_notificacion;
        }


        public async Task<IEnumerable<ENotificaciones>> GetByUsuarioYTipoAsync(int usuarioId, int tipoNotificacion)
        {
            return await _context.T_Notificaciones
                .Where(n => n.CN_Tipo_notificacion == tipoNotificacion &&
                            n.NotificacionesXUsuarios.Any(nxu => nxu.CN_Id_usuario == usuarioId))
                .ToListAsync();
        }

    }
}
