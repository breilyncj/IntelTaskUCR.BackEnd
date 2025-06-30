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
    public class TareasRepository : ITareasRepository
    {
        private readonly IntelTaskDbContext _context;

        //Constructor de la clase
        public TareasRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ETareas>> GetAllAsync()
        {
            return await _context.T_Tareas.ToListAsync();
        }
        public async Task<IEnumerable<ETareas>> GetAllWithRelacionesAsync()
        {
            return await _context.T_Tareas
                .Include(t => t.TareaOrigen)
                .Include(t => t.TareasHijas)
                .Include(t => t.Prioridades)
                .Include(t => t.Complejidades)
                .Include(t => t.UsuarioAsignado)//s
                .Include(t => t.UsuarioCreador)//s
                .Include(t => t.TareasIncumplimientos)
                .Include(t => t.TareasSeguimiento)
                .Include(t => t.TareasJustificacionRechazo)
                .Include(t => t.Estados)
                .Include(t => t.AdjuntosXTareas)
                    .ThenInclude(ax => ax.Adjunto)
                    .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<ETareas?> GetByIdAsync(int id)
        {
            return await _context.T_Tareas.FindAsync(id);
        }
        public async Task<IEnumerable<ETareas>> GetAllByIdUsuarioCreadorAsync(int id)
        {
            return await _context.T_Tareas
                .Include(t => t.TareaOrigen)
                .Include(t => t.TareasHijas)
                .Include(t => t.Prioridades)
                .Include(t => t.Complejidades)
                .Include(t => t.UsuarioAsignado)//s
                .Include(t => t.UsuarioCreador)//s
                .Include(t => t.TareasIncumplimientos)
                .Include(t => t.TareasSeguimiento)
                .Include(t => t.TareasJustificacionRechazo)
                .Include( t => t.Estados)
                .Include(t => t.AdjuntosXTareas)
                    .ThenInclude(ax => ax.Adjunto)
                .Where(t => t.UsuarioCreador != null && t.UsuarioCreador.CN_Id_usuario == id)
                .AsSplitQuery()
                .ToListAsync();
        }
        
        public async Task<IEnumerable<ETareas>> GetAllByIdUsuarioAsignadoAsync(int id)
        {
            return await _context.T_Tareas
                .Include(t => t.TareaOrigen)
                .Include(t => t.TareasHijas)
                .Include(t => t.Prioridades)
                .Include(t => t.Complejidades)
                .Include(t => t.UsuarioAsignado)//s
                .Include(t => t.UsuarioCreador)//s
                .Include(t => t.TareasIncumplimientos)
                .Include(t => t.TareasSeguimiento)
                .Include(t => t.TareasJustificacionRechazo)
                .Include( t => t.Estados)
                .Include(t => t.AdjuntosXTareas)
                    .ThenInclude(ax => ax.Adjunto)
                .Where(t => t.UsuarioAsignado != null && t.UsuarioAsignado.CN_Id_usuario == id)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<ETareas?> GetByIdWithRelacionesAsync(int id)
        {
            return await _context.T_Tareas
                .Include(t => t.TareaOrigen)
                .Include(t => t.TareasHijas)
                .Include(t => t.Prioridades)
                .Include(t => t.Complejidades)
                .Include(t => t.UsuarioAsignado)
                .Include(t => t.UsuarioCreador)
                .Include(t => t.TareasIncumplimientos)
                .Include(t => t.TareasSeguimiento)
                .Include(t => t.TareasJustificacionRechazo)
                .Include(t => t.Estados)
                .Include(t => t.AdjuntosXTareas)
                    .ThenInclude(ax => ax.Adjunto)
                    .AsSplitQuery()
                .FirstOrDefaultAsync(t => t.CN_Id_tarea == id); 
        }


        //Guarda
        public async Task AddAsync(ETareas tareas)
        {
            await _context.T_Tareas.AddAsync(tareas);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(ETareas tareas)
        {
            _context.T_Tareas.Update(tareas);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Tareas.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Tareas.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

    }
}
