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
    /// Returns <see cref="DefaultVisit"/>.<br/>
    /// But depending on client needs it can be overriden.
    /// </returns>
    public virtual TReturn Visit(TBaseVisitable visitable) => DefaultVisit;

    /// <inheritdoc cref="IVisitor{TVisitable,TReturn}.DefaultVisit"/>
    public virtual TReturn DefaultVisit => default!;
}

/// <inheritdoc cref="VisitorBase{TBaseVisitable,TReturn}" />
public abstract class VisitorNoReturnBase<TBaseVisitable> :
    VisitorBase<TBaseVisitable, VisitUnit>, IVisitor<TBaseVisitable>
    where TBaseVisitable : IVisitable<TBaseVisitable>;