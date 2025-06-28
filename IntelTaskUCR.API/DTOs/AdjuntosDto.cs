namespace IntelTaskUCR.API.DTOs
{
    public class AdjuntosDto
    {
        public int CN_Id_adjuntos { get; set; }
        public string CT_Archivo_ruta { get; set; }
        public int CN_Usuario_accion {  get; set; }
        public DateTime CF_Fecha_registro { get; set; }

        public string NombreArchivo { get; set; } = string.Empty;
    }
}
