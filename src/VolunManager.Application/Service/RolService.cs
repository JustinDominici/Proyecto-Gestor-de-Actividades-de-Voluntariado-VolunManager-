using VolunManager.Application.Contract;
using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Roles;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;

namespace VolunManager.Application.Service
{
    public class RolService : BaseService, IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<ServiceResult<IEnumerable<RolDto>>> GetAllAsync()
        {
            var roles = await _rolRepository.GetAllAsync();

            var resultado = roles.Select(MapToDto);

            return Ok(resultado, "Roles obtenidos correctamente.");
        }

        public async Task<ServiceResult<RolDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<RolDto>("El ID del rol no es válido.");
            }

            var rol = await _rolRepository.GetByIdAsync(id);

            if (rol == null)
            {
                return NotFound<RolDto>("No se encontró el rol solicitado.");
            }

            return Ok(MapToDto(rol), "Rol obtenido correctamente.");
        }

        public async Task<ServiceResult<RolDto>> CreateAsync(RolCreateDto dto)
        {
            var validationMessage = ValidateRol(dto.Nombre);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<RolDto>(validationMessage);
            }

            var nombreEnUso = await _rolRepository.ExisteNombreAsync(dto.Nombre.Trim());

            if (nombreEnUso)
            {
                return Conflict<RolDto>("Ya existe un rol registrado con ese nombre.");
            }

            var rol = new Rol(dto.Nombre.Trim(), dto.Descripcion?.Trim() ?? string.Empty);

            await _rolRepository.AddAsync(rol);
            await _rolRepository.SaveChangesAsync();

            return Ok(MapToDto(rol), "Rol creado correctamente.");
        }

        public async Task<ServiceResult<bool>> UpdateAsync(int id, RolUpdateDto dto)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID del rol no es válido.");
            }

            var validationMessage = ValidateRol(dto.Nombre);

            if (!string.IsNullOrEmpty(validationMessage))
            {
                return Fail<bool>(validationMessage);
            }

            var rol = await _rolRepository.GetByIdAsync(id);

            if (rol == null)
            {
                return NotFound<bool>("No se encontró el rol que desea actualizar.");
            }

            var nombreEnUso = await _rolRepository.ExisteNombreAsync(dto.Nombre.Trim(), id);

            if (nombreEnUso)
            {
                return Conflict<bool>("Ya existe otro rol registrado con ese nombre.");
            }

            rol.Actualizar(dto.Nombre.Trim(), dto.Descripcion?.Trim() ?? string.Empty);

            await _rolRepository.SaveChangesAsync();

            return Ok(true, "Rol actualizado correctamente.");
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return Fail<bool>("El ID del rol no es válido.");
            }

            // Se busca primero para poder devolver 404 si no existe, en vez
            // de mezclar ese motivo con el de "tiene voluntarios asociados"
            // (409). Antes esta distincion la hacia el Controller llamando
            // dos veces al servicio; ahora la resuelve el servicio solo.
            var rol = await _rolRepository.GetByIdAsync(id);

            if (rol == null)
            {
                return NotFound<bool>("No se encontró el rol que desea eliminar.");
            }

            var tieneVoluntarios = await _rolRepository.TieneVoluntariosAsociadosAsync(id);

            if (tieneVoluntarios)
            {
                return Conflict<bool>("No se puede eliminar el rol porque tiene voluntarios asociados.");
            }

            var eliminado = await _rolRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return NotFound<bool>("No se encontró el rol que desea eliminar.");
            }

            return Ok(true, "Rol eliminado correctamente.");
        }

        private string ValidateRol(string nombre)
        {
            if (IsEmpty(nombre))
            {
                return "El nombre del rol es obligatorio.";
            }

            return string.Empty;
        }

        private static RolDto MapToDto(Rol rol)
        {
            return new RolDto
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion,
                CantidadVoluntarios = rol.Voluntarios.Count
            };
        }
    }
}
