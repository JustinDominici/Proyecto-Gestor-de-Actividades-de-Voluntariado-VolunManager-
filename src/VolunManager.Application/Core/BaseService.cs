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

        /// <summary>400: datos invalidos o regla de negocio incumplida.</summary>
        protected ServiceResult<T> Fail<T>(string message)
        {
            return ServiceResult<T>.Fail(message);
        }

        /// <summary>404: el recurso solicitado no existe.</summary>
        protected ServiceResult<T> NotFound<T>(string message)
        {
            return ServiceResult<T>.NotFound(message);
        }

        /// <summary>409: la operacion entra en conflicto con datos existentes.</summary>
        protected ServiceResult<T> Conflict<T>(string message)
        {
            return ServiceResult<T>.Conflict(message);
        }
    }
}
