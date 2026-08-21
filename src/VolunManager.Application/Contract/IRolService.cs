using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Roles;

namespace VolunManager.Application.Contract
{
    public interface IRolService
    {
        Task<ServiceResult<IEnumerable<RolDto>>> GetAllAsync();

        Task<ServiceResult<RolDto>> GetByIdAsync(int id);

        Task<ServiceResult<RolDto>> CreateAsync(RolCreateDto dto);

        Task<ServiceResult<bool>> UpdateAsync(int id, RolUpdateDto dto);

        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
