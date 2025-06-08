using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class ETareas
    {
        public int CN_Id_tarea  {  get; set; }
        public int? CN_Tarea_origen { get; set; } 

        public string CT_Titulo_tarea { get; set; } 

        public string CT_Descripcion_tarea { get; set; } 

        public string CT_Descripcion_espera { get; set; }

        public byte CN_Id_complejidad { get; set; } 

        public byte CN_Id_estado { get; set; } 

        public byte CN_Id_prioridad { get; set; } 

        public string CN_Numero_GIS { get; set; } 

        public DateTime CF_Fecha_asignacion { get; set; } 

        public DateTime CF_Fecha_limite { get; set; } 

        public DateTime CF_Fecha_finalizacion {  get; set; } 

        public int CN_Usuario_creador { get ; set; }     

        public int? CN_Usuario_asignado { get; set; } 
        
        // Propiedad de nevagacion a la tarea origen
        
        public ETareas? TareaOrigen { get; set; }
        
        public EPrioridades? Prioridades { get; set; }
        
        // Propiedad de navegacion (uno a muchos)
        
        public ICollection<ETareas>? TareasHijas { get; set; }
        public ICollection<ETareasIncumplimientos>? TareasIncumplimientos { get; set; }
        
        public ICollection<ETareasJustificacionRechazo>? TareasJustificacionRechazo { get; set; }

        public ICollection<ETareasSeguimiento>? TareasSeguimiento { get; set; }
    }
}
