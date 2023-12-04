namespace Visitor.NET;
    
/// <summary>Contract of visitable type</summary>
public interface IVisitable
{
    /// <summary>Necessary part of the visitor pattern</summary>
    /// <param name="visitor">The visitor</param>
    /// <typeparam name="TReturn">Type visitor returns. If no return value supposed use <see cref="VisitUnit"/></typeparam>
    /// <returns><code>visitor.Visit(this)</code></returns>
    TReturn Accept<TReturn>(IVisitor<TReturn> visitor);
}