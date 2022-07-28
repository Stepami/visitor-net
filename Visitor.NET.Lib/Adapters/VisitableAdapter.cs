using Visitor.NET.Lib.Core;

namespace Visitor.NET.Lib.Adapters
{
    public abstract class VisitableAdapter<TData, TVisitor> : IVisitable<TVisitor>
        where TVisitor : IVisitor
    {
        public TData Data { get; }

        protected VisitableAdapter(TData data) => Data = data;

        public abstract void Accept(TVisitor visitor);
    }
}