using VolunManager.Domain.Core;

namespace VolunManager.Domain.Entities
{
    public class Jornada : BaseEntity
    {
        public string Titulo { get; private set; } = string.Empty;

        public string Descripcion { get; private set; } = string.Empty;

        public DateTime Fecha { get; private set; }

        public string Lugar { get; private set; } = string.Empty;

        public int HorasEstimadas { get; private set; }

        public int VoluntarioId { get; private set; }

        public Voluntario? Voluntario { get; private set; }

        public ICollection<Tarea> Tareas { get; private set; } = new List<Tarea>();

        /// <summary>
        /// Constructor protegido y vacio: lo requiere Entity Framework Core
        /// para materializar la entidad al leerla de la base de datos.
        /// </summary>
        protected Jornada()
        {
        }

        /// <summary>
        /// Constructor parametrizado: es el que usa la aplicacion para crear
        /// una jornada nueva ya asociada a un voluntario existente.
        /// </summary>
        public Jornada(string titulo, string descripcion, DateTime fecha, string lugar, int horasEstimadas, int voluntarioId)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Fecha = fecha;
            Lugar = lugar;
            HorasEstimadas = horasEstimadas;
            VoluntarioId = voluntarioId;
        }

        /// <summary>
        /// Unico punto por el que se pueden modificar los datos de la jornada.
        /// </summary>
        public void Actualizar(string titulo, string descripcion, DateTime fecha, string lugar, int horasEstimadas, int voluntarioId)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Fecha = fecha;
            Lugar = lugar;
            HorasEstimadas = horasEstimadas;
            VoluntarioId = voluntarioId;
        }

        public override string ObtenerResumen()
        {
            return $"Jornada #{Id}: {Titulo} el {Fecha:dd/MM/yyyy} en {Lugar} ({HorasEstimadas}h estimadas)";
        }
    }
}
