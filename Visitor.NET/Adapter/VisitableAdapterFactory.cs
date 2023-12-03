namespace Visitor.NET.Adapter;

/// <summary>Adapter factory contract</summary>
/// <typeparam name="TData">Type wrapped with adapter</typeparam>
/// <typeparam name="TVisitor">Visitor type</typeparam>
/// <typeparam name="TReturn">Return type of a visitor</typeparam>
public abstract class VisitableAdapterFactory<TData, TVisitor, TReturn>
    where TVisitor : IVisitor
{
    /// <summary>
    /// Factory method.
    /// Due to the fact that contract is class, not interface,
    /// declared return type can be more derived in concrete factory
    /// </summary>
    /// <param name="data">Data needs to be visitable</param>
    /// <returns>Visitable wrapper of data</returns>
    public abstract VisitableAdapter<TData, TVisitor, TReturn> Create(TData data);
}

/// <inheritdoc />
public abstract class VisitableAdapterFactory<TData, TVisitor> :
    VisitableAdapterFactory<TData, TVisitor, VisitUnit>
    where TVisitor : IVisitor
{
    /// <inheritdoc />
    public abstract override VisitableAdapter<TData, TVisitor> Create(TData data);
}