using VolunManager.Application.Contract;
using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Voluntarios;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;

namespace VolunManager.Application.Service
{
    public class VoluntarioService : BaseService, IVoluntarioService
    {
        private readonly IVoluntarioRepository _voluntarioRepository;

        public VoluntarioService(IVoluntarioRepository voluntarioRepository)
        {
            _voluntarioRepository = voluntarioRepository;
        }

        public async Task<ServiceResult<IEnumerable<VoluntarioDto>>> GetAllAsync()
        {
            var voluntarios = await _voluntarioRepository.GetAllAsync();

            var resultado = voluntarios.Select(MapToDto);

            return Ok(resultado, "Voluntarios obtenidos correctamente.");
        }

        public async Task<ServiceResult<VoluntarioDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<VoluntarioDto>("El ID del voluntario no es válido.");
            }

            var voluntario = await _voluntarioRepository.GetByIdAsync(id);

            if (voluntario == null)
            {
                return Fail<VoluntarioDto>("No se encontró el voluntario solicitado.");
            }

            return Ok(MapToDto(voluntario), "Voluntario obtenido correctamente.");
        }

        public async Task<ServiceResult<VoluntarioDto>> CreateAsync(VoluntarioCreateDto dto)
        {
            var validationMessage = ValidateVoluntario(dto.Nombre, dto.Apellido, dto.Correo, dto.Telefono);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<VoluntarioDto>(validationMessage);
            }

            var correoEnUso = await _voluntarioRepository.ExisteCorreoAsync(dto.Correo.Trim());

            if (correoEnUso)
            {
                return Fail<VoluntarioDto>("Ya existe un voluntario registrado con ese correo.");
            }

            var rolExiste = await _voluntarioRepository.ExisteRolAsync(dto.RolId);

            if (!rolExiste)
            {
                return Fail<VoluntarioDto>($"No existe un rol con el ID {dto.RolId}.");
            }

            var voluntario = new Voluntario(dto.Nombre.Trim(), dto.Apellido.Trim(), dto.Correo.Trim(), dto.Telefono.Trim(), dto.RolId);

            await _voluntarioRepository.AddAsync(voluntario);
            await _voluntarioRepository.SaveChangesAsync();

            return Ok(MapToDto(voluntario), "Voluntario creado correctamente.");
        }

        public async Task<ServiceResult<bool>> UpdateAsync(int id, VoluntarioUpdateDto dto)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID del voluntario no es válido.");
            }

            var validationMessage = ValidateVoluntario(dto.Nombre, dto.Apellido, dto.Correo, dto.Telefono);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<bool>(validationMessage);
            }

            var voluntario = await _voluntarioRepository.GetByIdAsync(id);

            if (voluntario == null)
            {
                return Fail<bool>("No se encontró el voluntario que desea actualizar.");
            }

            var correoEnUso = await _voluntarioRepository.ExisteCorreoAsync(dto.Correo.Trim(), id);

            if (correoEnUso)
            {
                return Fail<bool>("Ya existe otro voluntario registrado con ese correo.");
            }

            var rolExiste = await _voluntarioRepository.ExisteRolAsync(dto.RolId);

            if (!rolExiste)
            {
                return Fail<bool>($"No existe un rol con el ID {dto.RolId}.");
            }

            voluntario.Actualizar(dto.Nombre.Trim(), dto.Apellido.Trim(), dto.Correo.Trim(), dto.Telefono.Trim(), dto.Activo, dto.RolId);

            await _voluntarioRepository.SaveChangesAsync();

            return Ok(true, "Voluntario actualizado correctamente.");
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID del voluntario no es válido.");
            }

            var voluntario = await _voluntarioRepository.GetByIdAsync(id);

            if (voluntario == null)
            {
                return Fail<bool>("No se encontró el voluntario que desea eliminar.");
            }

            var tieneTareas = await _voluntarioRepository.TieneTareasAsociadasAsync(id);

            if (tieneTareas)
            {
                return Fail<bool>("No se puede eliminar el voluntario porque tiene tareas asociadas.");
            }

            var tieneAsistencias = await _voluntarioRepository.TieneAsistenciasAsociadasAsync(id);

            if (tieneAsistencias)
            {
                return Fail<bool>("No se puede eliminar el voluntario porque tiene asistencias registradas.");
            }

            var eliminado = await _voluntarioRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return Fail<bool>("No se encontró el voluntario que desea eliminar.");
            }

            return Ok(true, "Voluntario eliminado correctamente.");
        }

        private string ValidateVoluntario(string nombre, string apellido, string correo, string telefono)
        {
            if (IsEmpty(nombre))
            {
                return "El nombre del voluntario es obligatorio.";
            }

            if (IsEmpty(apellido))
            {
                return "El apellido del voluntario es obligatorio.";
            }

            if (IsEmpty(correo))
            {
                return "El correo del voluntario es obligatorio.";
            }

            if (!IsValidEmail(correo))
            {
                return "El correo del voluntario no tiene un formato válido.";
            }

            if (IsEmpty(telefono))
            {
                return "El teléfono del voluntario es obligatorio.";
            }

            return string.Empty;
        }

        private static VoluntarioDto MapToDto(Voluntario voluntario)
        {
            return new VoluntarioDto
            {
                Id = voluntario.Id,
                Nombre = voluntario.Nombre,
                Apellido = voluntario.Apellido,
                Correo = voluntario.Correo,
                Telefono = voluntario.Telefono,
                Activo = voluntario.Activo,
                RolId = voluntario.RolId,
                NombreRol = voluntario.Rol?.Nombre
            };
        }
    }
}
