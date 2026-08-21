using Microsoft.AspNetCore.Mvc;
using VolunManager.Api.Extensions;
using VolunManager.Application.Contract;
using VolunManager.Application.Dtos.Tareas;

namespace VolunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareasController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTareas()
        {
            var result = await _tareaService.GetAllAsync();

            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarea(int id)
        {
            var result = await _tareaService.GetByIdAsync(id);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CrearTarea(TareaCreateDto tareaCreateDto)
        {
            var result = await _tareaService.CreateAsync(tareaCreateDto);

            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTarea(int id, TareaUpdateDto tareaUpdateDto)
        {
            var result = await _tareaService.UpdateAsync(id, tareaUpdateDto);

            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTarea(int id)
        {
            var result = await _tareaService.DeleteAsync(id);

            return result.ToActionResult();
        }
    }
}
