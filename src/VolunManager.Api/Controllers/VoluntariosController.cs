using Microsoft.AspNetCore.Mvc;
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

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoluntario(int id)
        {
            var result = await _voluntarioService.GetByIdAsync(id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearVoluntario(VoluntarioCreateDto voluntarioCreateDto)
        {
            var result = await _voluntarioService.CreateAsync(voluntarioCreateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVoluntario(int id, VoluntarioUpdateDto voluntarioUpdateDto)
        {
            var result = await _voluntarioService.UpdateAsync(id, voluntarioUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVoluntario(int id)
        {
            // Igual que en RolesController: se verifica primero si el voluntario
            // existe (404) para distinguirlo del caso "no se puede eliminar
            // porque tiene tareas asociadas" (400), que es otro motivo de fallo.
            var existente = await _voluntarioService.GetByIdAsync(id);

            if (!existente.Success)
            {
                return NotFound(existente);
            }

            var result = await _voluntarioService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}