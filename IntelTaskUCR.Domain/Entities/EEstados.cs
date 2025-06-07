using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class EEstados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte CN_Id_estado { get; set; }

        public string CT_Estado { get; set; }

        public string CT_Descripcion {  get; set; } 
    }
}
