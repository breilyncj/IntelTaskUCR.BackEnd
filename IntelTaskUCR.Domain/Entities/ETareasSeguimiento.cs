using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class ETareasSeguimiento
    {
        public  int CN_Id_seguimiento { get; set; } 

        public int CN_Id_tarea { get ; set; } 

        public string CT_Comentario { get; set; } 

        public DateTime CF_Fecha_seguimiento { get; set; } 

    }
}
