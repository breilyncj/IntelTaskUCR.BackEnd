using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class ETareasJustificacionRechazo
    {
        public int CN_Id_tarea { get ; set; } 

        public DateTime CF_Fecha_hora_rechazo { get; set; } 

        public string CT_Descripcion_rechazo { get; set; } 

        public int CN_Id_tarea_rechazo { get; set; } 
    }
}
