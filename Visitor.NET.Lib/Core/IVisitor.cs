namespace Visitor.NET.Lib.Core
{
    /// <summary>Marker interface of a visitor entity</summary>
    public interface IVisitor{}
    
    /// <summary>Contract of visitor type</summary>
    /// <typeparam name="TVisitable">What we visit</typeparam>
    /// <typeparam name="T">What we return after visiting. If no return value supposed use <see cref="Unit"/></typeparam>
    public interface IVisitor<in TVisitable, out T> : IVisitor
        where TVisitable : IVisitable
    {
        /// <summary>
        /// Method with logics of visiting.
        /// If return type is <see cref="Unit"/> simply write:
        /// <code>return default</code>
        /// </summary>
        /// <param name="visitable">An entity we visit</param>
        /// <returns>Product of visiting</returns>
        T Visit(TVisitable visitable);
    }

    /// <inheritdoc />
    public interface IVisitor<in TVisitable> :
        IVisitor<TVisitable, Unit>
        where TVisitable : IVisitable
    {
    }
}