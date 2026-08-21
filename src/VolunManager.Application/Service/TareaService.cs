using VolunManager.Application.Contract;
using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Tareas;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;

namespace VolunManager.Application.Service
{
    public class TareaService : BaseService, ITareaService
    {
        private readonly ITareaRepository _tareaRepository;

        public TareaService(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<ServiceResult<IEnumerable<TareaDto>>> GetAllAsync()
        {
            var tareas = await _tareaRepository.GetAllAsync();

            var resultado = tareas.Select(MapToDto);

            return Ok(resultado, "Tareas obtenidas correctamente.");
        }

        public async Task<ServiceResult<TareaDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<TareaDto>("El ID de la tarea no es válido.");
            }

            var tarea = await _tareaRepository.GetByIdAsync(id);

            if (tarea == null)
            {
                return Fail<TareaDto>("No se encontró la tarea solicitada.");
            }

            return Ok(MapToDto(tarea), "Tarea obtenida correctamente.");
        }

        public async Task<ServiceResult<TareaDto>> CreateAsync(TareaCreateDto dto)
        {
            var validationMessage = ValidateTarea(dto.Titulo);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<TareaDto>(validationMessage);
            }

            var jornadaExiste = await _tareaRepository.ExisteJornadaAsync(dto.JornadaId);

            if (!jornadaExiste)
            {
                return Fail<TareaDto>($"No existe una jornada con el ID {dto.JornadaId}.");
            }

            var voluntarioExiste = await _tareaRepository.ExisteVoluntarioAsync(dto.VoluntarioId);

            if (!voluntarioExiste)
            {
                return Fail<TareaDto>($"No existe un voluntario con el ID {dto.VoluntarioId}.");
            }

            var tarea = new Tarea(dto.Titulo.Trim(), dto.Descripcion?.Trim() ?? string.Empty, dto.JornadaId, dto.VoluntarioId);

            await _tareaRepository.AddAsync(tarea);
            await _tareaRepository.SaveChangesAsync();

            var tareaCreada = await _tareaRepository.GetByIdAsync(tarea.Id);

            return Ok(MapToDto(tareaCreada!), "Tarea creada correctamente.");
        }

        public async Task<ServiceResult<bool>> UpdateAsync(int id, TareaUpdateDto dto)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID de la tarea no es válido.");
            }

            var validationMessage = ValidateTarea(dto.Titulo);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<bool>(validationMessage);
            }

            var tarea = await _tareaRepository.GetByIdAsync(id);

            if (tarea == null)
            {
                return Fail<bool>("No se encontró la tarea que desea actualizar.");
            }

            var jornadaExiste = await _tareaRepository.ExisteJornadaAsync(dto.JornadaId);

            if (!jornadaExiste)
            {
                return Fail<bool>($"No existe una jornada con el ID {dto.JornadaId}.");
            }

            var voluntarioExiste = await _tareaRepository.ExisteVoluntarioAsync(dto.VoluntarioId);

            if (!voluntarioExiste)
            {
                return Fail<bool>($"No existe un voluntario con el ID {dto.VoluntarioId}.");
            }

            tarea.Actualizar(dto.Titulo.Trim(), dto.Descripcion?.Trim() ?? string.Empty, dto.JornadaId, dto.VoluntarioId, dto.Estado);

            await _tareaRepository.SaveChangesAsync();

            return Ok(true, "Tarea actualizada correctamente.");
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID de la tarea no es válido.");
            }

            var eliminado = await _tareaRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return Fail<bool>("No se encontró la tarea que desea eliminar.");
            }

            return Ok(true, "Tarea eliminada correctamente.");
        }

        private string ValidateTarea(string titulo)
        {
            if (IsEmpty(titulo))
            {
                return "El título de la tarea es obligatorio.";
            }

            return string.Empty;
        }

        private static TareaDto MapToDto(Tarea tarea)
        {
            return new TareaDto
            {
                Id = tarea.Id,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                Estado = tarea.Estado.ToString(),
                JornadaId = tarea.JornadaId,
                TituloJornada = tarea.Jornada?.Titulo,
                VoluntarioId = tarea.VoluntarioId,
                NombreVoluntario = tarea.Voluntario != null
                    ? $"{tarea.Voluntario.Nombre} {tarea.Voluntario.Apellido}"
                    : null,
                FechaAsignacion = tarea.FechaAsignacion,
                FechaCompletada = tarea.FechaCompletada
            };
        }
    }
}
