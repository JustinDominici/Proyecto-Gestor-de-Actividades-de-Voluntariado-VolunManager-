using Microsoft.AspNetCore.Mvc;
using VolunManager.Infrastructure.Interfaces;
using VolunManager.Infrastructure.Models;

namespace VolunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JornadasController : ControllerBase
    {
        private readonly IJornadaRepository _jornadaRepository;

        public JornadasController(IJornadaRepository jornadaRepository)
        {
            _jornadaRepository = jornadaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JornadaDto>>> GetJornadas()
        {
            var jornadas = await _jornadaRepository.GetAllAsync();

            return Ok(jornadas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JornadaDto>> GetJornada(int id)
        {
            var jornada = await _jornadaRepository.GetByIdAsync(id);

            if (jornada == null)
            {
                return NotFound($"No se encontró una jornada con el ID {id}.");
            }

            return Ok(jornada);
        }

        [HttpPost]
        public async Task<ActionResult<JornadaDto>> CrearJornada(JornadaCreateDto jornadaCreateDto)
        {
            try
            {
                var jornada = await _jornadaRepository.CreateAsync(jornadaCreateDto);

                return CreatedAtAction(nameof(GetJornada), new { id = jornada.Id }, jornada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarJornada(int id, JornadaUpdateDto jornadaUpdateDto)
        {
            try
            {
                var actualizado = await _jornadaRepository.UpdateAsync(id, jornadaUpdateDto);

                if (!actualizado)
                {
                    return NotFound($"No se encontró una jornada con el ID {id}.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarJornada(int id)
        {
            var eliminado = await _jornadaRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return NotFound($"No se encontró una jornada con el ID {id}.");
            }

            return NoContent();
        }
    }
}