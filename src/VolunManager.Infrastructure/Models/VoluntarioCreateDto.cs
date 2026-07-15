namespace VolunManager.Infrastructure.Models
{
    public class VoluntarioCreateDto
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;
    }
}