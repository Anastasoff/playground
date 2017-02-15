using System;

namespace WebSuiteDDD.SharedKernel
{
    public abstract class EntityBase<TKey> : IEquatable<EntityBase<TKey>>
    {
        protected EntityBase(TKey id)
        {
            Id = id;
        }

        public TKey Id { get; }

        public bool Equals(EntityBase<TKey> other)
        {
            if (other == null) return false;

            return this.Id.Equals(other.Id);
        }

        public override bool Equals(object entity)
        {
            if (entity == null) return false;

            return entity is EntityBase<TKey> && this == (EntityBase<TKey>)entity;
        }

        public override int GetHashCode() => this.Id.GetHashCode();

        public static bool operator ==(EntityBase<TKey> entity1, EntityBase<TKey> entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            if (entity1.Id.ToString() == entity2.Id.ToString())
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(EntityBase<TKey> entity1, EntityBase<TKey> entity2) => !(entity1 == entity2);
    }
}