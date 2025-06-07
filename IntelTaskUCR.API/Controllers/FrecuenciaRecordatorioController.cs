using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FrecuenciaRecordatorioController : ControllerBase
    {
        private readonly IFrecuenciaRecordatorioRepository _frecuenciaRecordatorioRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        public FrecuenciaRecordatorioController(IFrecuenciaRecordatorioRepository frecuenciaRecordatorioRepository, IUsuariosRepository usuariosRepository) { 
            _frecuenciaRecordatorioRepository = frecuenciaRecordatorioRepository;
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _frecuenciaRecordatorioRepository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _frecuenciaRecordatorioRepository.GetByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EFrecuenciaRecordatorio entity)
        {

            EUsuarios usuarioCreador = await _usuariosRepository.GetByIdAsync(entity.CN_Id_usuario_creador);

            if (usuarioCreador != null)
            {
                await _frecuenciaRecordatorioRepository.AddAsync(entity);
                return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_recordatorio }, entity);
            }
            else
            {
                return NotFound("No se encontró el usuario creador");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EFrecuenciaRecordatorio entity)
        {

            EFrecuenciaRecordatorio findFrecuenciaRecordatorio = await _frecuenciaRecordatorioRepository.GetByIdAsync(id);

            EUsuarios usuarioCreador = await _usuariosRepository.GetByIdAsync(entity.CN_Id_usuario_creador);

            if (usuarioCreador != null && findFrecuenciaRecordatorio != null)
            {
                findFrecuenciaRecordatorio.CN_Id_recordatorio = entity.CN_Id_recordatorio; // Si hubiera incremento en la bd esta linea se ignora
                findFrecuenciaRecordatorio.CT_Texto_recordatorio = entity.CT_Texto_recordatorio;
                findFrecuenciaRecordatorio.CF_Fecha_hora_recordatorio = entity.CF_Fecha_hora_recordatorio;
                findFrecuenciaRecordatorio.CF_Fecha_hora_registro = entity.CF_Fecha_hora_registro;
                findFrecuenciaRecordatorio.CF_Fecha_final_evento = entity.CF_Fecha_final_evento;
                findFrecuenciaRecordatorio.CF_Fecha_hora_evento_pospuesto = entity.CF_Fecha_hora_evento_pospuesto;
                findFrecuenciaRecordatorio.CN_Frecuencia_recordatorio = entity.CN_Frecuencia_recordatorio;
                findFrecuenciaRecordatorio.CN_Id_usuario_creador = entity.CN_Id_usuario_creador;
                findFrecuenciaRecordatorio.CB_Estado = entity.CB_Estado;

                await _frecuenciaRecordatorioRepository.UpdateAsync(findFrecuenciaRecordatorio);
                return NoContent();

            }
            else if (findFrecuenciaRecordatorio == null)
            {
                return NotFound("No se encontró la frecuencia del recordatorio");
            }
            else
            {
                return NotFound("No se encontró el usuario creador");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _frecuenciaRecordatorioRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
