using IntelTaskUCR.Domain.Entities;

namespace IntelTaskUCR.API.DTOs
{
    public class ComplejidadesDto
    {
        public byte CN_Id_complejidad { get; set; }
        public string CT_Nombre { get; set; } 
        
        public List<TareasDto>? Tareas { get; set; }
    }
}
