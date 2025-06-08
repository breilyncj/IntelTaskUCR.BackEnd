namespace IntelTaskUCR.API.DTOs
{
    public class TareasJustificacionRechazoDto
    {
        
        public int CN_Id_tarea { get; set; }
        public DateTime CF_Fecha_hora_rechazo { get; set; }
        public string CT_Descripcion_rechazo { get; set; }
        public int CN_Id_tarea_rechazo { get; set; }
        
        public TareasDto? Tareas { get; set; }
        
    }
}
