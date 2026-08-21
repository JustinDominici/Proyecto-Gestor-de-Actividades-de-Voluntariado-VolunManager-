using Microsoft.AspNetCore.Mvc;
using VolunManager.Api.Extensions;
using VolunManager.Application.Contract;
using VolunManager.Application.Dtos.Asistencias;

namespace VolunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsistenciasController : ControllerBase
    {
        private readonly IAsistenciaService _asistenciaService;

        public AsistenciasController(IAsistenciaService asistenciaService)
        {
            _asistenciaService = asistenciaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsistencias()
        {
            var result = await _asistenciaService.GetAllAsync();

            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsistencia(int id)
        {
            var result = await _asistenciaService.GetByIdAsync(id);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CrearAsistencia(AsistenciaCreateDto asistenciaCreateDto)
        {
            var result = await _asistenciaService.CreateAsync(asistenciaCreateDto);

            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAsistencia(int id, AsistenciaUpdateDto asistenciaUpdateDto)
        {
            var result = await _asistenciaService.UpdateAsync(id, asistenciaUpdateDto);

            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarAsistencia(int id)
        {
            var result = await _asistenciaService.DeleteAsync(id);

            return result.ToActionResult();
        }
    }
}
