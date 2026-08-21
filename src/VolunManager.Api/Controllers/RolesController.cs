using Microsoft.AspNetCore.Mvc;
using VolunManager.Api.Extensions;
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

            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRol(int id)
        {
            var result = await _rolService.GetByIdAsync(id);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(RolCreateDto rolCreateDto)
        {
            var result = await _rolService.CreateAsync(rolCreateDto);

            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, RolUpdateDto rolUpdateDto)
        {
            var result = await _rolService.UpdateAsync(id, rolUpdateDto);

            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            // Ya no hace falta el chequeo en dos pasos: DeleteAsync ahora
            // devuelve el ErrorType correcto (NotFound o Conflict) segun
            // el motivo del fallo, y ToActionResult() lo traduce solo.
            var result = await _rolService.DeleteAsync(id);

            return result.ToActionResult();
        }
    }
}
