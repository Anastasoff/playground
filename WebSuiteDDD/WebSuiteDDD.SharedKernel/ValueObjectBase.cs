using System;

namespace WebSuiteDDD.SharedKernel
{
    public abstract class ValueObjectBase<T> : IEquatable<T>
        where T : ValueObjectBase<T>
    {
        public abstract bool Equals(T other);

        public abstract override bool Equals(object obj);

        public abstract override int GetHashCode();
    }
}