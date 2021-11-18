using System;

namespace Data.Entities.Interfaces
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
        static IEntity<T> GetBaseEntity => throw new NotImplementedException();
    }
}