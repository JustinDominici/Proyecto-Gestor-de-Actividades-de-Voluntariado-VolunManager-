using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunManager.Api.Context;
using VolunManager.Api.DTOs.Jornadas;
using VolunManager.Api.Models;

namespace VolunManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JornadasController : ControllerBase
    {
        private readonly VolunManagerContext _context;

        public JornadasController(VolunManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JornadaDto>>> GetJornadas()
        {
            var jornadas = await _context.Jornadas
                .Include(j => j.Voluntario)
                .Select(j => new JornadaDto
                {
                    Id = j.Id,
                    Titulo = j.Titulo,
                    Descripcion = j.Descripcion,
                    Fecha = j.Fecha,
                    Lugar = j.Lugar,
                    HorasEstimadas = j.HorasEstimadas,
                    VoluntarioId = j.VoluntarioId,
                    NombreVoluntario = j.Voluntario != null
                        ? j.Voluntario.Nombre + " " + j.Voluntario.Apellido
                        : null
                })
                .ToListAsync();

            return Ok(jornadas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JornadaDto>> GetJornada(int id)
        {
            var jornada = await _context.Jornadas
                .Include(j => j.Voluntario)
                .Where(j => j.Id == id)
                .Select(j => new JornadaDto
                {
                    Id = j.Id,
                    Titulo = j.Titulo,
                    Descripcion = j.Descripcion,
                    Fecha = j.Fecha,
                    Lugar = j.Lugar,
                    HorasEstimadas = j.HorasEstimadas,
                    VoluntarioId = j.VoluntarioId,
                    NombreVoluntario = j.Voluntario != null
                        ? j.Voluntario.Nombre + " " + j.Voluntario.Apellido
                        : null
                })
                .FirstOrDefaultAsync();

            if (jornada == null)
            {
                return NotFound($"No se encontró una jornada con el ID {id}.");
            }

            return Ok(jornada);
        }

        [HttpPost]
        public async Task<ActionResult<JornadaDto>> CrearJornada(JornadaCreateDto jornadaCreateDto)
        {
            var voluntarioExiste = await _context.Voluntarios
                .AnyAsync(v => v.Id == jornadaCreateDto.VoluntarioId);

            if (!voluntarioExiste)
            {
                return BadRequest($"No existe un voluntario con el ID {jornadaCreateDto.VoluntarioId}.");
            }

            var jornada = new Jornada
            {
                Titulo = jornadaCreateDto.Titulo,
                Descripcion = jornadaCreateDto.Descripcion,
                Fecha = jornadaCreateDto.Fecha,
                Lugar = jornadaCreateDto.Lugar,
                HorasEstimadas = jornadaCreateDto.HorasEstimadas,
                VoluntarioId = jornadaCreateDto.VoluntarioId
            };

            _context.Jornadas.Add(jornada);
            await _context.SaveChangesAsync();

            var jornadaDto = await _context.Jornadas
                .Include(j => j.Voluntario)
                .Where(j => j.Id == jornada.Id)
                .Select(j => new JornadaDto
                {
                    Id = j.Id,
                    Titulo = j.Titulo,
                    Descripcion = j.Descripcion,
                    Fecha = j.Fecha,
                    Lugar = j.Lugar,
                    HorasEstimadas = j.HorasEstimadas,
                    VoluntarioId = j.VoluntarioId,
                    NombreVoluntario = j.Voluntario != null
                        ? j.Voluntario.Nombre + " " + j.Voluntario.Apellido
                        : null
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetJornada), new { id = jornada.Id }, jornadaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarJornada(int id, JornadaUpdateDto jornadaUpdateDto)
        {
            var jornada = await _context.Jornadas.FindAsync(id);

            if (jornada == null)
            {
                return NotFound($"No se encontró una jornada con el ID {id}.");
            }

            var voluntarioExiste = await _context.Voluntarios
                .AnyAsync(v => v.Id == jornadaUpdateDto.VoluntarioId);

            if (!voluntarioExiste)
            {
                return BadRequest($"No existe un voluntario con el ID {jornadaUpdateDto.VoluntarioId}.");
            }

            jornada.Titulo = jornadaUpdateDto.Titulo;
            jornada.Descripcion = jornadaUpdateDto.Descripcion;
            jornada.Fecha = jornadaUpdateDto.Fecha;
            jornada.Lugar = jornadaUpdateDto.Lugar;
            jornada.HorasEstimadas = jornadaUpdateDto.HorasEstimadas;
            jornada.VoluntarioId = jornadaUpdateDto.VoluntarioId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarJornada(int id)
        {
            var jornada = await _context.Jornadas.FindAsync(id);

            if (jornada == null)
            {
                return NotFound($"No se encontró una jornada con el ID {id}.");
            }

            _context.Jornadas.Remove(jornada);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}