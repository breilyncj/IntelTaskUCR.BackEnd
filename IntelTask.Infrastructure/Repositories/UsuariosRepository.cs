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
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly IntelTaskDbContext _context;

        //Constructor de la clase
        public UsuariosRepository(IntelTaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EUsuarios>> GetAllAsync()
        {
            return await _context.T_Usuarios.ToListAsync();
        }
        public async Task<IEnumerable<EUsuarios>> GetAllWithRelacionesAsync()
        {
            return await _context.T_Usuarios
                .Include(u => u.FrecuenciaRecordatorios)
                .Include(u => u.TareasUsuarioAsignado)
                .ToListAsync();
        }

        public async Task<EUsuarios?> GetByIdAsync(int id)
        {
            return await _context.T_Usuarios.FindAsync(id);
        }
        public async Task<EUsuarios?> GetByIdWithFrecuenciaRecordatorioAsync(int id)
        {
            return await _context.T_Usuarios
                .Include(t => t.FrecuenciaRecordatorios)
                .FirstOrDefaultAsync(t => t.CN_Id_usuario == id);
        }
        
        public async Task<EUsuarios?> GetByIdWithTareasAsignadasAsync(int id)
        {
            return await _context.T_Usuarios
                .Include(t => t.TareasUsuarioAsignado)
                .FirstOrDefaultAsync(t => t.CN_Id_usuario == id);
        }
        
        public async Task<EUsuarios?> GetByCorreo(string correo)
        {
            return await _context.T_Usuarios
                .SingleOrDefaultAsync(u => u.CT_Correo_usuario.Equals(correo));
        }



        //Guarda
        public async Task AddAsync(EUsuarios usuarios)
        {
            await _context.T_Usuarios.AddAsync(usuarios);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(EUsuarios usuarios)
        {
            _context.T_Usuarios.Update(usuarios);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.T_Usuarios.FindAsync(id);

            if (entidad != null)
            {
                _context.T_Usuarios.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
