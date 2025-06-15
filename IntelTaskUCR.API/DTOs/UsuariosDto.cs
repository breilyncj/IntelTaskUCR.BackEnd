namespace IntelTaskUCR.API.DTOs
{
    public class UsuariosDto
    {
        public int CN_Id_usuario { get; set; } 

        public string CT_Nombre_usuario { get; set; } 

        public string CT_Correo_usuario {  get; set; } 

        public DateTime? CF_Fecha_nacimiento  { get; set; } 

        public string CT_Contrasenna { get; set; } 

        public bool CB_Estado_usuario { get; set; } 

        public DateTime? CF_Fecha_creacion_usuario { get; set; } 

        public DateTime? CF_Fecha_modificacion_usuario { get; set; } 

        public int CN_Id_rol { get; set; } 
        
        public List<FrecuenciaRecordatorioDto>? FrecuenciaRecordatorios { get; set; }

        public List<TareasDto>? TareasUsuarioAsignado { get; set; }
        
        public List<TareasDto>? TareasUsuarioCreador { get; set; }
    }
}
