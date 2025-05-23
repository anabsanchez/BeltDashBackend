namespace BeltDash.Domain.Entities.Common
{
    /// <summary>
    /// Clase base abstracta que proporciona propiedades comunes para todas las entidades del dominio.
    /// Implementa el patrón de diseño de entidad base para evitar la duplicación de código.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identificador único de la entidad.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha y hora de creación de la entidad en formato UTC.
        /// Se establece automáticamente al instanciar una nueva entidad.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora de la última actualización de la entidad en formato UTC.
        /// Se actualiza automáticamente cuando se modifica la entidad.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
