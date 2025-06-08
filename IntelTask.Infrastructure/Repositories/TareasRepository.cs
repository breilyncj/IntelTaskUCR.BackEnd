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
                .Include(t => t.TareasIncumplimientos)
                .Include(t => t.TareasSeguimiento)
                .Include(t => t.TareasJustificacionRechazo)
                .Include( t => t.Estados)
                .ToListAsync();
        }

        public async Task<ETareas?> GetByIdAsync(int id)
        {
            return await _context.T_Tareas.FindAsync(id);
        }

        public async Task<ETareas?> GetByIdWithRelacionesAsync(int id)
        {
            return await _context.T_Tareas
                .Include(t => t.TareaOrigen)
                .Include(t => t.TareasHijas)
                .Include(t => t.TareasIncumplimientos)
                .Include(t => t.TareasSeguimiento)
                .Include(t => t.TareasJustificacionRechazo)
                .Include(t => t.Estados)
                .FirstOrDefaultAsync(t => t.CN_Id_tarea == id); 
        }

        // public async Task<ETareas?> GetByIdWithTareasOrigenAsync(int id)
        // {
        //     return await _context.T_Tareas
        //         .Include(d => d.TareaOrigen)
        //         .FirstOrDefaultAsync(d => d.CN_Id_tarea == id); // Filtra por el id
        // }
        // public async Task<ETareas?> GetByIdWithTareasHijasAsync(int id)
        // {
        //     return await _context.T_Tareas
        //         .Include(d => d.TareasHijas)
        //         .FirstOrDefaultAsync(d => d.CN_Id_tarea == id); // Filtra por el id
        // }
        //
        // public async Task<ETareas?> GetByIdWithIncumplimientoAsync(int id)
        // {
        //     return await _context.T_Tareas
        //         .Include(d => d.TareasIncumplimientos) // Trae la colección relacionada
        //         .FirstOrDefaultAsync(d => d.CN_Id_tarea == id); // Filtra por el id
        // }

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
