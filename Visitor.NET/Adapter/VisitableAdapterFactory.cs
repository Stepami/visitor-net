namespace Visitor.NET.Adapter;

/// <summary>Adapter factory contract</summary>
/// <typeparam name="TData">Type wrapped with adapter</typeparam>
public abstract class VisitableAdapterFactory<TData>
{
    /// <summary>
    /// Factory method.
    /// Due to the fact that contract is class, not interface,
    /// declared return type can be more derived in concrete factory
    /// </summary>
    /// <param name="data">Data needs to be visitable</param>
    /// <returns>Visitable wrapper of data</returns>
    public abstract VisitableAdapter<TData> Create(TData data);
}