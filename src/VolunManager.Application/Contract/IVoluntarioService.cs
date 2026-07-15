using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Voluntarios;

namespace VolunManager.Application.Contract
{
    public interface IVoluntarioService
    {
        Task<ServiceResult<IEnumerable<VoluntarioDto>>> GetAllAsync();

        Task<ServiceResult<VoluntarioDto>> GetByIdAsync(int id);

        Task<ServiceResult<VoluntarioDto>> CreateAsync(VoluntarioCreateDto dto);

        Task<ServiceResult<bool>> UpdateAsync(int id, VoluntarioUpdateDto dto);

        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}