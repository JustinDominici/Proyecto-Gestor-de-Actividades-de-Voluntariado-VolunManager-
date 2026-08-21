using VolunManager.Domain.Core;

namespace VolunManager.Domain.Entities
{
    public class Asistencia : BaseEntity
    {
        public int VoluntarioId { get; private set; }

        public Voluntario? Voluntario { get; private set; }

        public int JornadaId { get; private set; }

        public Jornada? Jornada { get; private set; }

        public DateTime HoraEntrada { get; private set; }

        public DateTime? HoraSalida { get; private set; }

        public double HorasTrabajadas { get; private set; }

        /// <summary>
        /// Constructor protegido y vacio: lo requiere Entity Framework Core
        /// para materializar la entidad al leerla de la base de datos.
        /// </summary>
        protected Asistencia()
        {
        }

        /// <summary>
        /// Constructor parametrizado. La hora de salida es opcional (se puede
        /// registrar solo la entrada y completar la salida despues con
        /// Actualizar). Las horas trabajadas se calculan solas.
        /// </summary>
        public Asistencia(int voluntarioId, int jornadaId, DateTime horaEntrada, DateTime? horaSalida)
        {
            VoluntarioId = voluntarioId;
            JornadaId = jornadaId;
            HoraEntrada = horaEntrada;
            AsignarSalida(horaSalida);
        }

        /// <summary>
        /// Solo se pueden editar los horarios, no el voluntario ni la jornada
        /// asociados (para eso se crea un registro nuevo). Recalcula
        /// HorasTrabajadas automaticamente.
        /// </summary>
        public void Actualizar(DateTime horaEntrada, DateTime? horaSalida)
        {
            HoraEntrada = horaEntrada;
            AsignarSalida(horaSalida);
        }

        private void AsignarSalida(DateTime? horaSalida)
        {
            HoraSalida = horaSalida;

            HorasTrabajadas = horaSalida.HasValue
                ? Math.Round((horaSalida.Value - HoraEntrada).TotalHours, 2)
                : 0;
        }

        public override string ObtenerResumen()
        {
            var estado = HoraSalida.HasValue
                ? $"{HorasTrabajadas}h trabajadas"
                : "sin hora de salida registrada";

            return $"Asistencia #{Id}: Voluntario #{VoluntarioId} - Jornada #{JornadaId} ({estado})";
        }
    }
}
