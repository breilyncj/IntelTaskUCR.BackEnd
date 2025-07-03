using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces;
using IntelTaskUCR.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdjuntosController : Controller
    {
        private readonly IAdjuntosRepository _repository;
        private readonly IAdjuntosRepository _adjuntoRepository;
        private readonly IAdjuntosXTareasRepository _adjuntosXTareasRepository;

        public AdjuntosController(
            IAdjuntosRepository repository, 
            IAdjuntosRepository adjuntoRepository, 
            IAdjuntosXTareasRepository adjuntosXTareasRepository) 
        { 
            _repository = repository;
            _adjuntoRepository = adjuntoRepository;
            _adjuntosXTareasRepository = adjuntosXTareasRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EAdjuntos entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.CN_Id_adjuntos }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EAdjuntos entity)
        {
            if (id != entity.CN_Id_adjuntos)
                return BadRequest("El id esta vacio. ");
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("SubirAdjunto")]
        public async Task<IActionResult> SubirAdjunto(IFormFile archivo, [FromQuery] int idTarea, [FromQuery] int idUsuario)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("No se recibió archivo.");

            // Ruta física
            var nombreArchivo = Path.GetFileName(archivo.FileName);
            var carpeta = Path.Combine("wwwroot", "uploads", "tareas", idTarea.ToString());
            Directory.CreateDirectory(carpeta); // Crea si no existe
            var rutaArchivo = Path.Combine(carpeta, nombreArchivo);

            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // Ruta relativa para guardar en base de datos
            var rutaBD = Path.Combine("uploads", "tareas", idTarea.ToString(), nombreArchivo);

            // Crear entidad Adjunto
            var adjunto = new EAdjuntos
            {
                CT_Archivo_ruta = rutaBD.Replace("\\", "/"),
                CN_Usuario_accion = idUsuario,
                CF_Fecha_registro = DateTime.Now
            };

            await _adjuntoRepository.AddAsync(adjunto);

            // Ahora asocia a tarea
            var relacion = new EAdjuntosXTareas
            {
                CN_Id_adjuntos = adjunto.CN_Id_adjuntos,
                CN_Id_tarea = idTarea
            };

            await _adjuntosXTareasRepository.AddAsync(relacion); // Guarda en tabla intermedia

            return Ok("Archivo guardado y asociado.");
        }

        [HttpDelete("Desasociar/{idAdjunto}/{idTarea}")]
        public async Task<IActionResult> DesasociarAdjunto(int idAdjunto, int idTarea)
        {
            await _adjuntosXTareasRepository.DesasociarAdjuntoDeTareaAsync(idAdjunto, idTarea);
            return NoContent();
        }


    }
}
