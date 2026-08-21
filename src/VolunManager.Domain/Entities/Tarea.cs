using VolunManager.Domain.Core;
using VolunManager.Domain.Enums;

namespace VolunManager.Domain.Entities
{
    public class Tarea : BaseEntity
    {
        public string Titulo { get; private set; } = string.Empty;

        public string Descripcion { get; private set; } = string.Empty;

        public EstadoTarea Estado { get; private set; }

        public int JornadaId { get; private set; }

        public Jornada? Jornada { get; private set; }

        public int VoluntarioId { get; private set; }

        public Voluntario? Voluntario { get; private set; }

        public DateTime FechaAsignacion { get; private set; }

        public DateTime? FechaCompletada { get; private set; }

        /// <summary>
        /// Constructor protegido y vacio: lo requiere Entity Framework Core
        /// para materializar la entidad al leerla de la base de datos.
        /// </summary>
        protected Tarea()
        {
        }

        /// <summary>
        /// Constructor parametrizado: toda tarea nueva nace en estado
        /// Pendiente, con la fecha de asignacion fijada al momento de creacion.
        /// </summary>
        public Tarea(string titulo, string descripcion, int jornadaId, int voluntarioId)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            JornadaId = jornadaId;
            VoluntarioId = voluntarioId;
            Estado = EstadoTarea.Pendiente;
            FechaAsignacion = DateTime.UtcNow;
        }

        /// <summary>
        /// Actualiza los datos editables de la tarea, incluido el estado.
        /// FechaCompletada se administra sola: se fija al pasar a Completada
        /// y se limpia si el estado vuelve para atras.
        /// </summary>
        public void Actualizar(string titulo, string descripcion, int jornadaId, int voluntarioId, EstadoTarea estado)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            JornadaId = jornadaId;
            VoluntarioId = voluntarioId;

            if (estado == EstadoTarea.Completada && Estado != EstadoTarea.Completada)
            {
                FechaCompletada = DateTime.UtcNow;
            }
            else if (estado != EstadoTarea.Completada)
            {
                FechaCompletada = null;
            }

            Estado = estado;
        }

        public override string ObtenerResumen()
        {
            return $"Tarea #{Id}: {Titulo} - Estado: {Estado}";
        }
    }
}
