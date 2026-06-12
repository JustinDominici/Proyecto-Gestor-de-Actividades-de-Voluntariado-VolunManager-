using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunManager.Api.Context;
using VolunManager.Api.DTOs.Voluntarios;
using VolunManager.Api.Models;

namespace VolunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoluntariosController : ControllerBase
    {
        private readonly VolunManagerContext _context;

        public VoluntariosController(VolunManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoluntarioDto>>> GetVoluntarios()
        {
            var voluntarios = await _context.Voluntarios
                .Select(v => new VoluntarioDto
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    Apellido = v.Apellido,
                    Correo = v.Correo,
                    Telefono = v.Telefono,
                    Activo = v.Activo
                })
                .ToListAsync();

            return Ok(voluntarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoluntarioDto>> GetVoluntario(int id)
        {
            var voluntario = await _context.Voluntarios
                .Where(v => v.Id == id)
                .Select(v => new VoluntarioDto
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    Apellido = v.Apellido,
                    Correo = v.Correo,
                    Telefono = v.Telefono,
                    Activo = v.Activo
                })
                .FirstOrDefaultAsync();

            if (voluntario == null)
            {
                return NotFound($"No se encontró un voluntario con el ID {id}.");
            }

            return Ok(voluntario);
        }

        [HttpPost]
        public async Task<ActionResult<VoluntarioDto>> CrearVoluntario(VoluntarioCreateDto voluntarioCreateDto)
        {
            var voluntario = new Voluntario
            {
                Nombre = voluntarioCreateDto.Nombre,
                Apellido = voluntarioCreateDto.Apellido,
                Correo = voluntarioCreateDto.Correo,
                Telefono = voluntarioCreateDto.Telefono,
                Activo = true
            };

            _context.Voluntarios.Add(voluntario);
            await _context.SaveChangesAsync();

            var voluntarioDto = new VoluntarioDto
            {
                Id = voluntario.Id,
                Nombre = voluntario.Nombre,
                Apellido = voluntario.Apellido,
                Correo = voluntario.Correo,
                Telefono = voluntario.Telefono,
                Activo = voluntario.Activo
            };

            return CreatedAtAction(nameof(GetVoluntario), new { id = voluntario.Id }, voluntarioDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVoluntario(int id, VoluntarioUpdateDto voluntarioUpdateDto)
        {
            var voluntario = await _context.Voluntarios.FindAsync(id);

            if (voluntario == null)
            {
                return NotFound($"No se encontró un voluntario con el ID {id}.");
            }

            voluntario.Nombre = voluntarioUpdateDto.Nombre;
            voluntario.Apellido = voluntarioUpdateDto.Apellido;
            voluntario.Correo = voluntarioUpdateDto.Correo;
            voluntario.Telefono = voluntarioUpdateDto.Telefono;
            voluntario.Activo = voluntarioUpdateDto.Activo;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVoluntario(int id)
        {
            var voluntario = await _context.Voluntarios.FindAsync(id);

            if (voluntario == null)
            {
                return NotFound($"No se encontró un voluntario con el ID {id}.");
            }

            _context.Voluntarios.Remove(voluntario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}