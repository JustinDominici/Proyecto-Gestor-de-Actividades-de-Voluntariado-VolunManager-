using VolunManager.Application.Contract;
using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Jornadas;
using VolunManager.Infrastructure.Interfaces;

using InfraJornadaDto = VolunManager.Infrastructure.Models.JornadaDto;
using InfraJornadaCreateDto = VolunManager.Infrastructure.Models.JornadaCreateDto;
using InfraJornadaUpdateDto = VolunManager.Infrastructure.Models.JornadaUpdateDto;

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

            var resultado = jornadas.Select(MapToApplicationDto);

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
                return Fail<JornadaDto>("No se encontró la jornada solicitada.");
            }

            return Ok(MapToApplicationDto(jornada), "Jornada obtenida correctamente.");
        }

        public async Task<ServiceResult<JornadaDto>> CreateAsync(JornadaCreateDto dto)
        {
            var validationMessage = ValidateJornada(dto.Titulo, dto.Descripcion, dto.Fecha, dto.Lugar, dto.HorasEstimadas, dto.VoluntarioId);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<JornadaDto>(validationMessage);
            }

            var infraDto = new InfraJornadaCreateDto
            {
                Titulo = dto.Titulo.Trim(),
                Descripcion = dto.Descripcion.Trim(),
                Fecha = dto.Fecha,
                Lugar = dto.Lugar.Trim(),
                HorasEstimadas = dto.HorasEstimadas,
                VoluntarioId = dto.VoluntarioId
            };

            try
            {
                var jornadaCreada = await _jornadaRepository.CreateAsync(infraDto);

                return Ok(MapToApplicationDto(jornadaCreada), "Jornada creada correctamente.");
            }
            catch (Exception ex)
            {
                return Fail<JornadaDto>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> UpdateAsync(int id, JornadaUpdateDto dto)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID de la jornada no es válido.");
            }

            var validationMessage = ValidateJornada(dto.Titulo, dto.Descripcion, dto.Fecha, dto.Lugar, dto.HorasEstimadas, dto.VoluntarioId);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<bool>(validationMessage);
            }

            var infraDto = new InfraJornadaUpdateDto
            {
                Titulo = dto.Titulo.Trim(),
                Descripcion = dto.Descripcion.Trim(),
                Fecha = dto.Fecha,
                Lugar = dto.Lugar.Trim(),
                HorasEstimadas = dto.HorasEstimadas,
                VoluntarioId = dto.VoluntarioId
            };

            try
            {
                var actualizado = await _jornadaRepository.UpdateAsync(id, infraDto);

                if (!actualizado)
                {
                    return Fail<bool>("No se encontró la jornada que desea actualizar.");
                }

                return Ok(true, "Jornada actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
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
                return Fail<bool>("No se encontró la jornada que desea eliminar.");
            }

            return Ok(true, "Jornada eliminada correctamente.");
        }

        private string ValidateJornada(string titulo, string descripcion, DateTime fecha, string lugar, int horasEstimadas, int voluntarioId)
        {
            if (IsEmpty(titulo))
            {
                return "El título de la jornada es obligatorio.";
            }

            if (IsEmpty(descripcion))
            {
                return "La descripción de la jornada es obligatoria.";
            }

            if (fecha == default)
            {
                return "La fecha de la jornada es obligatoria.";
            }

            if (fecha.Date < DateTime.Today)
            {
                return "La fecha de la jornada no puede ser menor a la fecha actual.";
            }

            if (IsEmpty(lugar))
            {
                return "El lugar de la jornada es obligatorio.";
            }

            if (horasEstimadas <= 0)
            {
                return "Las horas estimadas deben ser mayores que cero.";
            }

            if (voluntarioId <= 0)
            {
                return "Debe indicar un voluntario válido.";
            }

            return string.Empty;
        }

        private JornadaDto MapToApplicationDto(InfraJornadaDto dto)
        {
            return new JornadaDto
            {
                Id = dto.Id,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Fecha = dto.Fecha,
                Lugar = dto.Lugar,
                HorasEstimadas = dto.HorasEstimadas,
                VoluntarioId = dto.VoluntarioId,
                NombreVoluntario = dto.NombreVoluntario
            };
        }
    }
}