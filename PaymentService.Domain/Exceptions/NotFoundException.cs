using System;

namespace PaymentService.Domain.Exceptions
{
    /// <summary>
    /// Not found exception.
    /// </summary>
    /// <param name="entity">Entity name.</param>
    /// <param name="id">Entity id.</param>
    [Serializable]
    public class NotFoundException(string entity, object id) : Exception($"The {entity} with ID '{id}' was not found.")
    {
        /// <summary>
        /// Type of entity.
        /// </summary>
        public string Entity { get; private set; } = entity;
        /// <summary>
        /// Entity id.
        /// </summary>
        public object Id { get; private set; } = id;
    }
}
