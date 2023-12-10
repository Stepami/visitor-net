namespace Visitor.NET;

/// <summary>https://en.wikipedia.org/wiki/Unit_type</summary>
public readonly struct VisitUnit :
    IEquatable<VisitUnit>,
    IComparable<VisitUnit>
{
    /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
    public bool Equals(VisitUnit other) => true;

    /// <inheritdoc cref="IComparable{T}.CompareTo"/>
    public int CompareTo(VisitUnit other) => 0;

    /// <inheritdoc cref="Object.Equals(object?)"/>
    public override bool Equals(object? obj) => 
        obj is VisitUnit;

    /// <inheritdoc cref="Object.GetHashCode"/>
    public override int GetHashCode() => 0;

    /// <inheritdoc cref="Object.ToString"/>
    public override string ToString() => "()";

    /// <summary>== operator implementation</summary>
    public static bool operator ==(VisitUnit a, VisitUnit b) => a.Equals(b);

    /// <summary>!= operator implementation</summary>
    public static bool operator !=(VisitUnit a, VisitUnit b) => !(a == b);
}