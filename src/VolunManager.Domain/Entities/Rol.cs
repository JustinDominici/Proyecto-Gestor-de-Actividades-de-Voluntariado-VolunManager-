using VolunManager.Domain.Core;

namespace VolunManager.Domain.Entities
{
    public class Rol : BaseEntity
    {
        public string Nombre { get; private set; } = string.Empty;

        public string Descripcion { get; private set; } = string.Empty;

        public ICollection<Voluntario> Voluntarios { get; private set; } = new List<Voluntario>();

        /// <summary>
        /// Constructor protegido y vacio: lo requiere Entity Framework Core
        /// para materializar la entidad al leerla de la base de datos.
        /// </summary>
        protected Rol()
        {
        }

        /// <summary>
        /// Constructor parametrizado: es el que usa la aplicacion para crear
        /// un rol nuevo (por ejemplo Voluntario, Coordinador, Administrador).
        /// </summary>
        public Rol(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public void Actualizar(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public override string ObtenerResumen()
        {
            return $"Rol #{Id}: {Nombre} ({Voluntarios.Count} voluntario(s) asignado(s))";
        }
    }
}
