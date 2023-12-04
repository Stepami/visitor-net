namespace Visitor.NET.Adapter;

/// <summary>Wrapper around the data not implementing <see cref="IVisitable"/></summary>
/// <typeparam name="TData">Type of wrapped data</typeparam>
public abstract class VisitableAdapter<TData> : IVisitable
{
    /// <summary>Data that needs to be visitable</summary>
    public TData Data { get; }

    /// <summary>Protected constructor with parameters</summary>
    /// <param name="data">Wrapped data</param>
    protected VisitableAdapter(TData data) => Data = data;

    /// <inheritdoc cref="IVisitable.Accept{TReturn}"/>
    public abstract TReturn Accept<TReturn>(IVisitor<TReturn> visitor);
}