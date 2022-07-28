using System;

namespace Visitor.NET.Lib.Core
{
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>
    {
        public bool Equals(Unit other) => true;

        public int CompareTo(Unit other) => 0;

        public override bool Equals(object obj) => 
            obj is Unit;

        public override int GetHashCode() => 0;

        public override string ToString() => "()";

        public static bool operator ==(Unit a, Unit b) => a.Equals(b);

        public static bool operator !=(Unit a, Unit b) => !(a == b);
    }
}