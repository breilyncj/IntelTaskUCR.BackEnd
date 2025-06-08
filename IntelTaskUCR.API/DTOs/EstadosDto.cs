using IntelTaskUCR.Domain.Entities;

namespace IntelTaskUCR.API.DTOs
{
    public class EstadosDto
    {
        public byte CN_Id_estado { get; set; }

        public string CT_Estado { get; set; }

        public string CT_Descripcion { get; set; }

        public List<TareasDto>? Tareas { get; set; }
    }
}
