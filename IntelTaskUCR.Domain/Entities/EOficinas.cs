using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class EOficinas
    {
        public int CN_Codigo_oficina { get; set; }
        public string CT_Nombre_oficina { get; set; }
        public int CN_Oficina_encargada { get; set; }
    }
}
