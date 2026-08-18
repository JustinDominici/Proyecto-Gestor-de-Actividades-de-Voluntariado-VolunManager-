using VolunManager.Domain.Core;

namespace VolunManager.Domain.Entities
{
    public class Voluntario : BaseEntity
    {
        public string Nombre { get; private set; } = string.Empty;

        public string Apellido { get; private set; } = string.Empty;

        public string Correo { get; private set; } = string.Empty;

        public string Telefono { get; private set; } = string.Empty;

        public bool Activo { get; private set; } = true;

        public ICollection<Jornada> Jornadas { get; private set; } = new List<Jornada>();

        /// <summary>
        /// Constructor protegido y vacio: lo requiere Entity Framework Core
        /// para materializar la entidad al leerla de la base de datos.
        /// </summary>
        protected Voluntario()
        {
        }

        /// <summary>
        /// Constructor parametrizado: es el que usa la aplicacion para crear
        /// un voluntario nuevo con datos validos.
        /// </summary>
        public Voluntario(string nombre, string apellido, string correo, string telefono)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            Activo = true;
        }

        /// <summary>
        /// Unico punto por el que se pueden modificar los datos del voluntario.
        /// Evita que cualquier capa externa toque las propiedades directamente.
        /// </summary>
        public void Actualizar(string nombre, string apellido, string correo, string telefono, bool activo)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            Activo = activo;
        }

        public void Desactivar()
        {
            Activo = false;
        }

        public void Activar()
        {
            Activo = true;
        }

        public override string ObtenerResumen()
        {
            var estado = Activo ? "Activo" : "Inactivo";
            return $"Voluntario #{Id}: {Nombre} {Apellido} ({Correo}) - {estado}";
        }
    }
}
