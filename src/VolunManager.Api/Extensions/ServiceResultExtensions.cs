using Microsoft.AspNetCore.Mvc;
using VolunManager.Application.Core;

namespace VolunManager.Api.Extensions
{
    /// <summary>
    /// Traduce un ServiceResult al codigo HTTP correcto segun su ErrorType,
    /// para que los controllers no tengan que repetir el mismo if/else en
    /// cada metodo (y para que un mismo fallo siempre se traduzca igual en
    /// toda la Api).
    /// </summary>
    public static class ServiceResultExtensions
    {
        public static IActionResult ToActionResult<T>(this ServiceResult<T> result)
        {
            if (result.Success)
            {
                return new OkObjectResult(result);
            }

            return result.ErrorType switch
            {
                ErrorType.NotFound => new NotFoundObjectResult(result),
                ErrorType.Conflict => new ConflictObjectResult(result),
                _ => new BadRequestObjectResult(result)
            };
        }
    }
}
