namespace VolunManager.Api.Models
{
    public class Voluntario
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public ICollection<Jornada> Jornadas { get; set; } = new List<Jornada>();
    }
}