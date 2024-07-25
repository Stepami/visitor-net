namespace Visitor.NET;

/// <summary>Base visitor</summary>
/// <typeparam name="TBaseVisitable">Hierarchy root type</typeparam>
/// <typeparam name="TReturn">Visit operation result type</typeparam>
public abstract class VisitorBase<TBaseVisitable, TReturn> :
    IVisitor<TBaseVisitable, TReturn>
    where TBaseVisitable : IVisitable<TBaseVisitable>
{
    /// <summary>
    /// Current visitor explicitly declared
    /// with type of <see cref="IVisitor{TVisitable,TReturn}"/>
    /// for type inference
    /// </summary>
    protected IVisitor<TBaseVisitable, TReturn> This => this;

    /// <summary>Visiting root element of Composite</summary>
    /// <param name="visitable">Hierarchy root</param>
    /// <returns>
    /// Base version throws exception as in some scenarios this method will never be called.
    /// But depending on client needs it can be overriden.
    /// </returns>
    /// <exception cref="NotSupportedException">Thrown in base version of method</exception>
    public virtual TReturn Visit(TBaseVisitable visitable) =>
        throw new NotSupportedException();

    /// <inheritdoc cref="IVisitor{TVisitable,TReturn}.DefaultVisit"/>
    public virtual TReturn DefaultVisit => default!;
}

/// <inheritdoc cref="VisitorBase{TBaseVisitable,TReturn}" />
public abstract class VisitorNoReturnBase<TBaseVisitable> :
    VisitorBase<TBaseVisitable, VisitUnit>, IVisitor<TBaseVisitable>
    where TBaseVisitable : IVisitable<TBaseVisitable>;