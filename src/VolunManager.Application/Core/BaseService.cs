using System.ComponentModel.DataAnnotations;

namespace VolunManager.Application.Core
{
    public abstract class BaseService
    {
        protected bool IsEmpty(string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        protected bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        protected ServiceResult<T> Ok<T>(T data, string message = "Operación realizada correctamente.")
        {
            return ServiceResult<T>.Ok(data, message);
        }

        protected ServiceResult<T> Fail<T>(string message)
        {
            return ServiceResult<T>.Fail(message);
        }
    }
}