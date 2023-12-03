namespace Visitor.NET;

/// <summary>Marker interface of a visitable entity</summary>
public interface IVisitable{}
    
/// <summary>Contract of visitable type</summary>
/// <typeparam name="TVisitor">What it is visited with</typeparam>
/// <typeparam name="T">Type visitor returns. If no return value supposed use <see cref="Unit"/></typeparam>
public interface IVisitable<in TVisitor, out T> : IVisitable
    where TVisitor : IVisitor
{
    /// <summary>Necessary part of the visitor pattern</summary>
    /// <param name="visitor">The visitor</param>
    /// <returns><code>visitor.Visit(this)</code></returns>
    T Accept(TVisitor visitor);
}

/// <inheritdoc />
public interface IVisitable<in TVisitor> :
    IVisitable<TVisitor, Unit>
    where TVisitor : IVisitor
{
}