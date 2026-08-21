using Microsoft.AspNetCore.Mvc;
using VolunManager.Api.Extensions;
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

            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJornada(int id)
        {
            var result = await _jornadaService.GetByIdAsync(id);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CrearJornada(JornadaCreateDto jornadaCreateDto)
        {
            var result = await _jornadaService.CreateAsync(jornadaCreateDto);

            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarJornada(int id, JornadaUpdateDto jornadaUpdateDto)
        {
            var result = await _jornadaService.UpdateAsync(id, jornadaUpdateDto);

            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarJornada(int id)
        {
            var result = await _jornadaService.DeleteAsync(id);

            return result.ToActionResult();
        }
    }
}
