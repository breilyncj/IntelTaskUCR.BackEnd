using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class EComplejidades
    {
        public byte CN_Id_complejidad { get; set; }
        public string CT_Nombre { get; set; } 
        
        public ICollection<ETareas>? Tareas { get; set; }
    }
}
