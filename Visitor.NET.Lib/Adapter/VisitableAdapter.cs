using Visitor.NET.Lib.Core;

namespace Visitor.NET.Lib.Adapter
{
    public abstract class VisitableAdapter<TData, TVisitor, TReturn> : IVisitable<TVisitor, TReturn>
        where TVisitor : IVisitor
    {
        public TData Data { get; }

        protected VisitableAdapter(TData data) => Data = data;

        public abstract TReturn Accept(TVisitor visitor);
    }
}