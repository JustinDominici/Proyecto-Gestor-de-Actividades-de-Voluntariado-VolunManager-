using Microsoft.AspNetCore.Mvc;
using VolunManager.Api.Extensions;
using VolunManager.Application.Contract;
using VolunManager.Application.Dtos.Voluntarios;

namespace VolunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoluntariosController : ControllerBase
    {
        private readonly IVoluntarioService _voluntarioService;

        public VoluntariosController(IVoluntarioService voluntarioService)
        {
            _voluntarioService = voluntarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVoluntarios()
        {
            var result = await _voluntarioService.GetAllAsync();

            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoluntario(int id)
        {
            var result = await _voluntarioService.GetByIdAsync(id);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CrearVoluntario(VoluntarioCreateDto voluntarioCreateDto)
        {
            var result = await _voluntarioService.CreateAsync(voluntarioCreateDto);

            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVoluntario(int id, VoluntarioUpdateDto voluntarioUpdateDto)
        {
            var result = await _voluntarioService.UpdateAsync(id, voluntarioUpdateDto);

            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVoluntario(int id)
        {
            // Ya no hace falta el chequeo en dos pasos: DeleteAsync ahora
            // devuelve el ErrorType correcto (NotFound o Conflict) segun
            // el motivo del fallo, y ToActionResult() lo traduce solo.
            var result = await _voluntarioService.DeleteAsync(id);

            return result.ToActionResult();
        }
    }
}
