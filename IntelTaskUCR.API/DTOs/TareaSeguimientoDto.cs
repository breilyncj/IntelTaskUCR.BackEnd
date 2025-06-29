namespace IntelTaskUCR.API.DTOs
{
    public class TareaSeguimientoDto
    {
        public int CN_Id_tarea { get; set; }     // ID de la tarea a delegar
        public int CN_Id_delegante { get; set; } // Usuario que delega 
        public int CN_Id_receptor { get; set; }  // Usuario que recibe la tarea
        public string? CT_Comentario { get; set; }

    }
}
