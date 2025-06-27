using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class EAdjuntos
    {
        public int CN_Id_adjuntos { get; set; }
        public string CT_Archivo_ruta { get; set; }
        public int CN_Usuario_accion { get; set; }
        public DateTime CF_Fecha_registro { get; set; }

        public ICollection<EAdjuntosXTareas> AdjuntosXTareas { get; set; }
    }
}
