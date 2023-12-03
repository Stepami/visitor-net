using Visitor.NET.Adapter;
using Visitor.NET.Tests.Visitors;

namespace Visitor.NET.Tests.VisitableAdapters;

public class LinkedListToVisitableAdapter<T> : 
    VisitableAdapter<LinkedListNode<T>, LinkedListNodePrinter<T>>
{
    public LinkedListToVisitableAdapter(LinkedListNode<T> data) : base(data)
    {
    }

    public override VisitUnit Accept(LinkedListNodePrinter<T> visitor) =>
        visitor.Visit(this);
}

public class LinkedListToVisitableAdapterFactory<T> :
    VisitableAdapterFactory<LinkedListNode<T>, LinkedListNodePrinter<T>>
{
    public override LinkedListToVisitableAdapter<T> Create(LinkedListNode<T> data) =>
        new(data);
}