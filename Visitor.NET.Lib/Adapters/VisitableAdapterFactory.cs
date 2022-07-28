using Visitor.NET.Lib.Core;

namespace Visitor.NET.Lib.Adapters
{
    public abstract class VisitableAdapterFactory<TData, TVisitor>
        where TVisitor : IVisitor
    {
        public abstract VisitableAdapter<TData, TVisitor> Create(TData data);
    }
}