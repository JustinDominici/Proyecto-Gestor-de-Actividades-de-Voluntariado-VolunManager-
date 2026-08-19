using Microsoft.AspNetCore.Mvc;
using VolunManager.Application.Contract;
using VolunManager.Application.Dtos.Roles;

namespace VolunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolesController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _rolService.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRol(int id)
        {
            var result = await _rolService.GetByIdAsync(id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(RolCreateDto rolCreateDto)
        {
            var result = await _rolService.CreateAsync(rolCreateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, RolUpdateDto rolUpdateDto)
        {
            var result = await _rolService.UpdateAsync(id, rolUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            // Se verifica primero si el rol existe (404) para poder distinguir
            // ese caso del de "no se puede eliminar porque tiene voluntarios
            // asociados" (400), que es un motivo de fallo distinto.
            var existente = await _rolService.GetByIdAsync(id);

            if (!existente.Success)
            {
                return NotFound(existente);
            }

            var result = await _rolService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
