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

        public int RolId { get; private set; }

        public Rol? Rol { get; private set; }

        public ICollection<Jornada> Jornadas { get; private set; } = new List<Jornada>();

        public ICollection<Tarea> Tareas { get; private set; } = new List<Tarea>();

        public ICollection<Asistencia> Asistencias { get; private set; } = new List<Asistencia>();

        /// <summary>
        /// Constructor protegido y vacio: lo requiere Entity Framework Core
        /// para materializar la entidad al leerla de la base de datos.
        /// </summary>
        protected Voluntario()
        {
        }

        /// <summary>
        /// Constructor parametrizado: es el que usa la aplicacion para crear
        /// un voluntario nuevo con datos validos, ya asociado a un rol.
        /// </summary>
        public Voluntario(string nombre, string apellido, string correo, string telefono, int rolId)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            Activo = true;
            RolId = rolId;
        }

        /// <summary>
        /// Unico punto por el que se pueden modificar los datos del voluntario.
        /// Evita que cualquier capa externa toque las propiedades directamente.
        /// </summary>
        public void Actualizar(string nombre, string apellido, string correo, string telefono, bool activo, int rolId)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            Activo = activo;
            RolId = rolId;
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
            var rol = Rol?.Nombre ?? "sin rol cargado";
            return $"Voluntario #{Id}: {Nombre} {Apellido} ({Correo}) - {estado} - Rol: {rol}";
        }
    }
}
