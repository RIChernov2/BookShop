using Data.Entities.Interfaces;

namespace Data.Entities
{
    public abstract class BaseEntity : BaseEntity<long>
    {

    }

    public abstract class BaseEntity<T> : IEntity<T>
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public T Id { get; set; }
    }
}