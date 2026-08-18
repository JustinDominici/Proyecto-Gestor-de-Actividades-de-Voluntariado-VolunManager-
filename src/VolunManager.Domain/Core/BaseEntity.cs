namespace VolunManager.Domain.Core
{
    /// <summary>
    /// Clase base abstracta para todas las entidades del dominio.
    /// Define el contrato comun (Id) y obliga a cada entidad concreta
    /// a implementar su propia forma de resumirse (ObtenerResumen).
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }

        protected BaseEntity()
        {
        }

        /// <summary>
        /// Cada entidad concreta (Voluntario, Jornada, Tarea, Asistencia, Rol)
        /// debe implementar su propio resumen. Permite usar polimorfismo:
        /// BaseEntity entidad = voluntario;
        /// entidad.ObtenerResumen(); // ejecuta la version de Voluntario
        /// </summary>
        public abstract string ObtenerResumen();
    }
}
