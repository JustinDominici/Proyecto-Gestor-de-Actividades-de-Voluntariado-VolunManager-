using VolunManager.Application.Contract;
using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Voluntarios;
using VolunManager.Infrastructure.Interfaces;

using InfraVoluntarioDto = VolunManager.Infrastructure.Models.VoluntarioDto;
using InfraVoluntarioCreateDto = VolunManager.Infrastructure.Models.VoluntarioCreateDto;
using InfraVoluntarioUpdateDto = VolunManager.Infrastructure.Models.VoluntarioUpdateDto;

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

            var resultado = voluntarios.Select(MapToApplicationDto);

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

            return Ok(MapToApplicationDto(voluntario), "Voluntario obtenido correctamente.");
        }

        public async Task<ServiceResult<VoluntarioDto>> CreateAsync(VoluntarioCreateDto dto)
        {
            var validationMessage = ValidateVoluntario(dto.Nombre, dto.Apellido, dto.Correo, dto.Telefono);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<VoluntarioDto>(validationMessage);
            }

            var infraDto = new InfraVoluntarioCreateDto
            {
                Nombre = dto.Nombre.Trim(),
                Apellido = dto.Apellido.Trim(),
                Correo = dto.Correo.Trim(),
                Telefono = dto.Telefono.Trim()
            };

            var voluntarioCreado = await _voluntarioRepository.CreateAsync(infraDto);

            return Ok(MapToApplicationDto(voluntarioCreado), "Voluntario creado correctamente.");
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

            var infraDto = new InfraVoluntarioUpdateDto
            {
                Nombre = dto.Nombre.Trim(),
                Apellido = dto.Apellido.Trim(),
                Correo = dto.Correo.Trim(),
                Telefono = dto.Telefono.Trim(),
                Activo = dto.Activo
            };

            var actualizado = await _voluntarioRepository.UpdateAsync(id, infraDto);

            if (!actualizado)
            {
                return Fail<bool>("No se encontró el voluntario que desea actualizar.");
            }

            return Ok(true, "Voluntario actualizado correctamente.");
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID del voluntario no es válido.");
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

        private VoluntarioDto MapToApplicationDto(InfraVoluntarioDto dto)
        {
            return new VoluntarioDto
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Activo = dto.Activo
            };
        }
    }
}