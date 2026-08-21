using VolunManager.Application.Contract;
using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Asistencias;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;

namespace VolunManager.Application.Service
{
    public class AsistenciaService : BaseService, IAsistenciaService
    {
        private readonly IAsistenciaRepository _asistenciaRepository;

        public AsistenciaService(IAsistenciaRepository asistenciaRepository)
        {
            _asistenciaRepository = asistenciaRepository;
        }

        public async Task<ServiceResult<IEnumerable<AsistenciaDto>>> GetAllAsync()
        {
            var asistencias = await _asistenciaRepository.GetAllAsync();

            var resultado = asistencias.Select(MapToDto);

            return Ok(resultado, "Asistencias obtenidas correctamente.");
        }

        public async Task<ServiceResult<AsistenciaDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<AsistenciaDto>("El ID de la asistencia no es válido.");
            }

            var asistencia = await _asistenciaRepository.GetByIdAsync(id);

            if (asistencia == null)
            {
                return NotFound<AsistenciaDto>("No se encontró la asistencia solicitada.");
            }

            return Ok(MapToDto(asistencia), "Asistencia obtenida correctamente.");
        }

        public async Task<ServiceResult<AsistenciaDto>> CreateAsync(AsistenciaCreateDto dto)
        {
            var validationMessage = ValidateHorarios(dto.HoraEntrada, dto.HoraSalida);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<AsistenciaDto>(validationMessage);
            }

            var voluntarioExiste = await _asistenciaRepository.ExisteVoluntarioAsync(dto.VoluntarioId);

            if (!voluntarioExiste)
            {
                return Fail<AsistenciaDto>($"No existe un voluntario con el ID {dto.VoluntarioId}.");
            }

            var jornadaExiste = await _asistenciaRepository.ExisteJornadaAsync(dto.JornadaId);

            if (!jornadaExiste)
            {
                return Fail<AsistenciaDto>($"No existe una jornada con el ID {dto.JornadaId}.");
            }

            var yaRegistrado = await _asistenciaRepository.ExisteAsistenciaAsync(dto.VoluntarioId, dto.JornadaId);

            if (yaRegistrado)
            {
                return Conflict<AsistenciaDto>("Ese voluntario ya tiene una asistencia registrada para esa jornada.");
            }

            var asistencia = new Asistencia(dto.VoluntarioId, dto.JornadaId, dto.HoraEntrada, dto.HoraSalida);

            await _asistenciaRepository.AddAsync(asistencia);
            await _asistenciaRepository.SaveChangesAsync();

            var asistenciaCreada = await _asistenciaRepository.GetByIdAsync(asistencia.Id);

            return Ok(MapToDto(asistenciaCreada!), "Asistencia creada correctamente.");
        }

        public async Task<ServiceResult<bool>> UpdateAsync(int id, AsistenciaUpdateDto dto)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID de la asistencia no es válido.");
            }

            var validationMessage = ValidateHorarios(dto.HoraEntrada, dto.HoraSalida);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<bool>(validationMessage);
            }

            var asistencia = await _asistenciaRepository.GetByIdAsync(id);

            if (asistencia == null)
            {
                return NotFound<bool>("No se encontró la asistencia que desea actualizar.");
            }

            asistencia.Actualizar(dto.HoraEntrada, dto.HoraSalida);

            await _asistenciaRepository.SaveChangesAsync();

            return Ok(true, "Asistencia actualizada correctamente.");
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID de la asistencia no es válido.");
            }

            var eliminado = await _asistenciaRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return NotFound<bool>("No se encontró la asistencia que desea eliminar.");
            }

            return Ok(true, "Asistencia eliminada correctamente.");
        }

        private string ValidateHorarios(DateTime horaEntrada, DateTime? horaSalida)
        {
            if (horaSalida.HasValue && horaSalida.Value <= horaEntrada)
            {
                return "La hora de salida debe ser posterior a la hora de entrada.";
            }

            return string.Empty;
        }

        private static AsistenciaDto MapToDto(Asistencia asistencia)
        {
            return new AsistenciaDto
            {
                Id = asistencia.Id,
                VoluntarioId = asistencia.VoluntarioId,
                NombreVoluntario = asistencia.Voluntario != null
                    ? $"{asistencia.Voluntario.Nombre} {asistencia.Voluntario.Apellido}"
                    : null,
                JornadaId = asistencia.JornadaId,
                TituloJornada = asistencia.Jornada?.Titulo,
                HoraEntrada = asistencia.HoraEntrada,
                HoraSalida = asistencia.HoraSalida,
                HorasTrabajadas = asistencia.HorasTrabajadas
            };
        }
    }
}
