﻿namespace IntelTaskUCR.API.DTOs
{
    public class TareaIncumplimientoDto
    {
        public int CN_Id_tarea_incumplimiento { get; set; }
        public int CN_Id_tarea { get; set; }
        public string CT_Justificacion_incumplimiento { get; set; }
        public DateTime CF_Fecha_incumplimiento { get; set; }
        
        public TareasDto? Tareas { get; set; }
    }
}
