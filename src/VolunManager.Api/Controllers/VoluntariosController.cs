using Microsoft.AspNetCore.Mvc;
using VolunManager.Infrastructure.Interfaces;
using VolunManager.Infrastructure.Models;

namespace VolunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoluntariosController : ControllerBase
    {
        private readonly IVoluntarioRepository _voluntarioRepository;

        public VoluntariosController(IVoluntarioRepository voluntarioRepository)
        {
            _voluntarioRepository = voluntarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoluntarioDto>>> GetVoluntarios()
        {
            var voluntarios = await _voluntarioRepository.GetAllAsync();

            return Ok(voluntarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoluntarioDto>> GetVoluntario(int id)
        {
            var voluntario = await _voluntarioRepository.GetByIdAsync(id);

            if (voluntario == null)
            {
                return NotFound($"No se encontró un voluntario con el ID {id}.");
            }

            return Ok(voluntario);
        }

        [HttpPost]
        public async Task<ActionResult<VoluntarioDto>> CrearVoluntario(VoluntarioCreateDto voluntarioCreateDto)
        {
            var voluntario = await _voluntarioRepository.CreateAsync(voluntarioCreateDto);

            return CreatedAtAction(nameof(GetVoluntario), new { id = voluntario.Id }, voluntario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVoluntario(int id, VoluntarioUpdateDto voluntarioUpdateDto)
        {
            var actualizado = await _voluntarioRepository.UpdateAsync(id, voluntarioUpdateDto);

            if (!actualizado)
            {
                return NotFound($"No se encontró un voluntario con el ID {id}.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVoluntario(int id)
        {
            var eliminado = await _voluntarioRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return NotFound($"No se encontró un voluntario con el ID {id}.");
            }

            return NoContent();
        }
    }
}