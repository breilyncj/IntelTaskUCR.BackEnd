namespace IntelTaskUCR.API.DTOs
{
    public class TareasDto
    {
        public int CN_Id_tarea { get; set; }
        public int? CN_Tarea_origen { get; set; }
        public string CT_Titulo_tarea { get; set; }
        public string CT_Descripcion_tarea { get; set; }
        public string CT_Descripcion_espera { get; set; }
        public byte CN_Id_complejidad { get; set; }
        public byte CN_Id_estado { get; set; }
        public byte CN_Id_prioridad { get; set; }
        public string CN_Numero_GIS { get; set; }
        public DateTime CF_Fecha_asignacion { get; set; }
        public DateTime CF_Fecha_limite { get; set; }
        public DateTime CF_Fecha_finalizacion { get; set; }
        public int CN_Usuario_creador { get; set; }
        public int? CN_Usuario_asignado { get; set; }
        
        public TareasDto? TareaOrigen { get; set; }

        public EstadosDto? Estados { get; set; }
        
        public PrioridadesDTO? Prioridades { get; set; }
        
        public ComplejidadesDto? Complejidades { get; set; }

        public UsuariosDto? UsuarioAsignado { get; set; }

        public UsuariosDto? UsuarioCreador { get; set; }

        public List<TareasDto>? TareasHijas { get; set; }

        
        public List<TareaIncumplimientoDto>? TareasIncumplimientos { get; set; }
        
        public List<TareasJustificacionRechazoDto>? TareasJustificacionRechazo { get; set; }

        public List<TareasSeguimientoDto>? TareasSeguimiento { get; set; }




    }
}
