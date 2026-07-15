using Microsoft.AspNetCore.Mvc;
using VolunManager.Application.Contract;
using VolunManager.Application.Dtos.Jornadas;

namespace VolunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JornadasController : ControllerBase
    {
        private readonly IJornadaService _jornadaService;

        public JornadasController(IJornadaService jornadaService)
        {
            _jornadaService = jornadaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetJornadas()
        {
            var result = await _jornadaService.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJornada(int id)
        {
            var result = await _jornadaService.GetByIdAsync(id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearJornada(JornadaCreateDto jornadaCreateDto)
        {
            var result = await _jornadaService.CreateAsync(jornadaCreateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarJornada(int id, JornadaUpdateDto jornadaUpdateDto)
        {
            var result = await _jornadaService.UpdateAsync(id, jornadaUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarJornada(int id)
        {
            var result = await _jornadaService.DeleteAsync(id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}