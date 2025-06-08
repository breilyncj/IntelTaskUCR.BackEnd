namespace IntelTaskUCR.API.DTOs
{
    public class TareasSeguimientoDto
    {
        public int CN_Id_seguimiento { get; set; }
        public int CN_Id_tarea { get; set; }
        public string CT_Comentario { get; set; }
        public DateTime CF_Fecha_seguimiento { get; set; }

        public TareasDto? Tareas { get; set; }
    }
}
