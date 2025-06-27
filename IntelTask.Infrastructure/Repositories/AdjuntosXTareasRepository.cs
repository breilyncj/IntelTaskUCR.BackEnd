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
    public class AdjuntosXTareasRepository : IAdjuntosXTareasRepository
    {
        private readonly IntelTaskDbContext _context;

        public AdjuntosXTareasRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EAdjuntosXTareas>> GetAllAsync()
        {
            return await _context.T_Adjuntos_X_Tareas.ToListAsync();
        }

        // Nuevo método para claves compuestas
        public async Task<EAdjuntosXTareas?> GetByIdsAsync(int idAdjunto, int idTarea)
        {
            return await _context.T_Adjuntos_X_Tareas
                .FirstOrDefaultAsync(x => x.CN_Id_adjuntos == idAdjunto && x.CN_Id_tarea == idTarea);
        }



        public async Task AddAsync(EAdjuntosXTareas porTarea)
        {
            await _context.T_Adjuntos_X_Tareas.AddAsync(porTarea);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EAdjuntosXTareas porTarea)
        {
            _context.T_Adjuntos_X_Tareas.Update(porTarea);
            await _context.SaveChangesAsync();
        }

        // Eliminar con clave compuesta
        public async Task DeleteAsync(int idAdjunto, int idTarea)
        {
            var entidad = await GetByIdsAsync(idAdjunto, idTarea);
            if (entidad != null)
            {
                _context.T_Adjuntos_X_Tareas.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

        public Task<EAdjuntosXTareas> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AgregarAsync(EAdjuntosXTareas relacion)
        {
            throw new NotImplementedException();
        }

    }
}
