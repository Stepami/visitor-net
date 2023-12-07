namespace Visitor.NET;
    
/// <summary>Contract of visitor type</summary>
/// <typeparam name="TVisitable">What we visit</typeparam>
/// <typeparam name="TReturn">What we return after visiting. If no return value supposed use <see cref="VisitUnit"/></typeparam>
public interface IVisitor<in TVisitable, out TReturn>
    where TVisitable : IVisitable<TVisitable>
{
    /// <summary>
    /// Method with logics of visiting.
    /// If return type is <see cref="VisitUnit"/> simply write:
    /// <code>return default</code>
    /// </summary>
    /// <param name="visitable">An entity we visit</param>
    /// <returns>Product of visiting</returns>
    TReturn Visit(TVisitable visitable);
}