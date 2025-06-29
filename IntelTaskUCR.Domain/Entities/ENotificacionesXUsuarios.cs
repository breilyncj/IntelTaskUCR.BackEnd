using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class ENotificacionesXUsuarios
    {
        public int CN_Id_notificacion { get; set; } 

        public int CN_Id_usuario { get; set; } 

        public string CT_Correo_destino { get; set; }

        public ENotificaciones Notificacion { get; set; }

        public EUsuarios Usuario { get; set; }


    }
}
