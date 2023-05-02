using System;

namespace Visitor.NET
{
    /// <summary>https://en.wikipedia.org/wiki/Unit_type</summary>
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>
    {
        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(Unit other) => true;

        /// <inheritdoc cref="IComparable{T}.CompareTo"/>
        public int CompareTo(Unit other) => 0;

        /// <inheritdoc cref="Object.Equals(object?)"/>
        public override bool Equals(object obj) => 
            obj is Unit;

        /// <inheritdoc cref="Object.GetHashCode"/>
        public override int GetHashCode() => 0;

        /// <inheritdoc cref="Object.ToString"/>
        public override string ToString() => "()";

        /// <summary>== operator implementation</summary>
        public static bool operator ==(Unit a, Unit b) => a.Equals(b);

        /// <summary>!= operator implementation</summary>
        public static bool operator !=(Unit a, Unit b) => !(a == b);
    }
}