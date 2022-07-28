using Visitor.NET.Lib.Core;

namespace Visitor.NET.Lib.Adapter
{
    /// <summary>Wrapper around the data not implementing <see cref="IVisitable{TVisitor,T}"/></summary>
    /// <typeparam name="TData">Type of wrapped data</typeparam>
    /// <typeparam name="TVisitor">Visitor type</typeparam>
    /// <typeparam name="TReturn">Return type of a visitor</typeparam>
    public abstract class VisitableAdapter<TData, TVisitor, TReturn> : 
        IVisitable<TVisitor, TReturn>
        where TVisitor : IVisitor
    {
        /// <summary>Data that needs to be visitable</summary>
        public TData Data { get; }

        /// <summary>Protected constructor with parameters</summary>
        /// <param name="data">Wrapped data</param>
        protected VisitableAdapter(TData data) => Data = data;

        /// <inheritdoc cref="IVisitable{TVisitor,T}.Accept"/>
        public abstract TReturn Accept(TVisitor visitor);
    }
}