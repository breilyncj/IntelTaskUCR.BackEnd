using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class ENotificaciones
    {
        public int CN_Id_notificacion {  get; set; } 

        public int CN_Tipo_notificacion { get; set; } 

        public string CT_Titulo_notificacion { get; set; } 

        public string CT_Texto_notificacion { get; set; } 

        public string CT_Correo_origen { get; set; } 

        public DateTime CF_Fecha_registro { get; set; } 

        public DateTime CF_Fecha_notificacion { get; set; } 

        public int CN_Id_recordatorio { get; set; } 

    }
}
