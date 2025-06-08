using IntelTaskUCR.Domain.Entities;

namespace IntelTaskUCR.Domain.Interfaces
{
    public interface ITareasIncumplimientosRepository
    {
        Task<IEnumerable<ETareasIncumplimientos>> GetAllAsync();
        
        Task<IEnumerable<ETareasIncumplimientos>> GetAllWithTareasAsync();
        
        Task<ETareasIncumplimientos?> GetByIdAsync(int id);
        
        Task<ETareasIncumplimientos?> GetByIdWithTareasAsync(int id);
        
        Task AddAsync(ETareasIncumplimientos tareaIncumplimientos);
        
        Task UpdateAsync(ETareasIncumplimientos tareaIncumplimientos);
        
        Task DeleteAsync(int idTareasIncumplimientos);
        
    }
}
