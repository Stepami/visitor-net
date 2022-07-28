using Visitor.NET.Lib.Core;

namespace Visitor.NET.Lib.Adapters
{
    public abstract class VisitableAdapterFactory<TData, TVisitor, TReturn>
        where TVisitor : IVisitor
    {
        public abstract VisitableAdapter<TData, TVisitor, TReturn> Create(TData data);
    }
}