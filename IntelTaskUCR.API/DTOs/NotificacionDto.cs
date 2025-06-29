namespace IntelTaskUCR.API.DTOs
{
    public class NotificacionDto
    {
        public int CN_Tipo_notificacion { get; set; }
        public string CT_Titulo_notificacion { get; set; }
        public string CT_Texto_notificacion { get; set; }
        public string CT_Correo_origen { get; set; }
        public DateTime CF_Fecha_registro { get; set; }
        public DateTime CF_Fecha_notificacion { get; set; }
        public int? CN_Id_recordatorio { get; set; }

        public ICollection<NotificacionesXUsuariosDto> NotificacionesXUsuarios { get; set; }

    }
}
