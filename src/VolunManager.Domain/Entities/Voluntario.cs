using VolunManager.Domain.Core;

namespace VolunManager.Domain.Entities
{
    public class Voluntario : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public ICollection<Jornada> Jornadas { get; set; } = new List<Jornada>();
    }
}