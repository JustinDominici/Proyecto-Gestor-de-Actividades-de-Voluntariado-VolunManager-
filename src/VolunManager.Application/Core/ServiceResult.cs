namespace VolunManager.Application.Core
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public ErrorType ErrorType { get; set; } = ErrorType.None;

        public static ServiceResult<T> Ok(T data, string message = "Operación realizada correctamente.")
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = data,
                ErrorType = ErrorType.None
            };
        }

        /// <summary>
        /// Fallo por datos invalidos o que no cumplen una regla de negocio.
        /// Se traduce a 400 Bad Request.
        /// </summary>
        public static ServiceResult<T> Fail(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                Data = default,
                ErrorType = ErrorType.Validation
            };
        }

        /// <summary>
        /// Fallo porque el recurso solicitado no existe. Se traduce a 404 Not Found.
        /// </summary>
        public static ServiceResult<T> NotFound(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                Data = default,
                ErrorType = ErrorType.NotFound
            };
        }

        /// <summary>
        /// Fallo porque la operacion entra en conflicto con el estado actual
        /// de los datos (duplicados, registros dependientes, etc.).
        /// Se traduce a 409 Conflict.
        /// </summary>
        public static ServiceResult<T> Conflict(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                Data = default,
                ErrorType = ErrorType.Conflict
            };
        }
    }
}
