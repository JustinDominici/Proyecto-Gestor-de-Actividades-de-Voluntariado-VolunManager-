using Microsoft.AspNetCore.Diagnostics;
using VolunManager.Application.Core;

namespace VolunManager.Api.ExceptionHandling
{
    /// <summary>
    /// Atrapa cualquier excepcion que se escape sin manejar de los
    /// controllers/servicios (por ejemplo, una caida de conexion a la base
    /// de datos) y devuelve una respuesta 500 con el mismo formato que usa
    /// el resto de la Api, en vez de una pagina de error generica o un
    /// stack trace crudo. La excepcion original se registra en el log del
    /// servidor para poder diagnosticarla despues.
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Excepción no controlada: {Message}", exception.Message);

            var result = ServiceResult<object>.Fail("Ocurrió un error interno. Intente nuevamente más tarde.");

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}
