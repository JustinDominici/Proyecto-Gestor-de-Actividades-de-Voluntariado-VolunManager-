using VolunManager.Application.Contract;
using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Jornadas;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;

namespace VolunManager.Application.Service
{
    public class JornadaService : BaseService, IJornadaService
    {
        private readonly IJornadaRepository _jornadaRepository;

        public JornadaService(IJornadaRepository jornadaRepository)
        {
            _jornadaRepository = jornadaRepository;
        }

        public async Task<ServiceResult<IEnumerable<JornadaDto>>> GetAllAsync()
        {
            var jornadas = await _jornadaRepository.GetAllAsync();

            var resultado = jornadas.Select(MapToDto);

            return Ok(resultado, "Jornadas obtenidas correctamente.");
        }

        public async Task<ServiceResult<JornadaDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<JornadaDto>("El ID de la jornada no es válido.");
            }

            var jornada = await _jornadaRepository.GetByIdAsync(id);

            if (jornada == null)
            {
                return NotFound<JornadaDto>("No se encontró la jornada solicitada.");
            }

            return Ok(MapToDto(jornada), "Jornada obtenida correctamente.");
        }

        public async Task<ServiceResult<JornadaDto>> CreateAsync(JornadaCreateDto dto)
        {
            var validationMessage = ValidateJornada(dto.Titulo, dto.Lugar, dto.HorasEstimadas);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<JornadaDto>(validationMessage);
            }

            var voluntarioExiste = await _jornadaRepository.ExisteVoluntarioAsync(dto.VoluntarioId);

            if (!voluntarioExiste)
            {
                return Fail<JornadaDto>($"No existe un voluntario con el ID {dto.VoluntarioId}.");
            }

            var jornada = new Jornada(dto.Titulo.Trim(), dto.Descripcion.Trim(), dto.Fecha, dto.Lugar.Trim(), dto.HorasEstimadas, dto.VoluntarioId);

            await _jornadaRepository.AddAsync(jornada);
            await _jornadaRepository.SaveChangesAsync();

            var jornadaCreada = await _jornadaRepository.GetByIdAsync(jornada.Id);

            return Ok(MapToDto(jornadaCreada!), "Jornada creada correctamente.");
        }

        public async Task<ServiceResult<bool>> UpdateAsync(int id, JornadaUpdateDto dto)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID de la jornada no es válido.");
            }

            var validationMessage = ValidateJornada(dto.Titulo, dto.Lugar, dto.HorasEstimadas);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<bool>(validationMessage);
            }

            var jornada = await _jornadaRepository.GetByIdAsync(id);

            if (jornada == null)
            {
                return NotFound<bool>("No se encontró la jornada que desea actualizar.");
            }

            var voluntarioExiste = await _jornadaRepository.ExisteVoluntarioAsync(dto.VoluntarioId);

            if (!voluntarioExiste)
            {
                return Fail<bool>($"No existe un voluntario con el ID {dto.VoluntarioId}.");
            }

            jornada.Actualizar(dto.Titulo.Trim(), dto.Descripcion.Trim(), dto.Fecha, dto.Lugar.Trim(), dto.HorasEstimadas, dto.VoluntarioId);

            await _jornadaRepository.SaveChangesAsync();

            return Ok(true, "Jornada actualizada correctamente.");
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID de la jornada no es válido.");
            }

            var eliminado = await _jornadaRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return NotFound<bool>("No se encontró la jornada que desea eliminar.");
            }

            return Ok(true, "Jornada eliminada correctamente.");
        }

        private string ValidateJornada(string titulo, string lugar, int horasEstimadas)
        {
            if (IsEmpty(titulo))
            {
                return "El título de la jornada es obligatorio.";
            }

            if (IsEmpty(lugar))
            {
                return "El lugar de la jornada es obligatorio.";
            }

            if (horasEstimadas <= 0)
            {
                return "Las horas estimadas deben ser mayores a cero.";
            }

            return string.Empty;
        }

        private static JornadaDto MapToDto(Jornada jornada)
        {
            return new JornadaDto
            {
                Id = jornada.Id,
                Titulo = jornada.Titulo,
                Descripcion = jornada.Descripcion,
                Fecha = jornada.Fecha,
                Lugar = jornada.Lugar,
                HorasEstimadas = jornada.HorasEstimadas,
                VoluntarioId = jornada.VoluntarioId,
                NombreVoluntario = jornada.Voluntario != null
                    ? $"{jornada.Voluntario.Nombre} {jornada.Voluntario.Apellido}"
                    : null
            };
        }
    }
}
