namespace VolunManager.Application.Core
{
    /// <summary>
    /// Clasifica el motivo de un ServiceResult fallido, para que la capa Api
    /// pueda traducirlo al codigo HTTP correcto sin tener que adivinar
    /// leyendo el mensaje.
    /// </summary>
    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Conflict
    }
}
