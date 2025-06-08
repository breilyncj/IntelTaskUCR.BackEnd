using IntelTaskUCR.Domain.Entities;

namespace IntelTaskUCR.API.DTOs
{
    public class PrioridadesDTO
    {
        public byte CN_Id_prioridad { get; set; }
        public string CT_Nombre_prioridad { get; set; }
        public string CT_Descripcion_prioridad { get; set; }
        
        public List<TareasDto>? Tareas { get; set; }
    }
}
