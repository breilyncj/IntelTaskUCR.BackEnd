﻿using IntelTaskUCR.Domain.Entities;

namespace IntelTaskUCR.API.DTOs
{
    public class FrecuenciaRecordatorioDto
    {
        public int CN_Id_recordatorio { get; set; }
        public string CT_Texto_recordatorio { get; set; }
        public DateTime CF_Fecha_hora_registro { get; set; }
        public DateTime CF_Fecha_hora_recordatorio { get; set; }
        public DateTime CF_Fecha_final_evento { get; set; }
        public DateTime? CF_Fecha_hora_evento_pospuesto { get; set; }
        public int CN_Frecuencia_recordatorio { get; set; }
        public int CN_Id_usuario_creador { get; set; }
        public bool CB_Estado {  get; set; }
        
        public UsuariosDto? Usuarios { get; set; }
    }
}
