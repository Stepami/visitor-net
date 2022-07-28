using Visitor.NET.Lib.Adapter;
using Visitor.NET.Lib.Core;
using Visitor.NET.Tests.Visitors;

namespace Visitor.NET.Tests.VisitableAdapters;

public class LinkedListToVisitableAdapter<T> : 
    VisitableAdapter<LinkedListNode<T>, LinkedListNodePrinter<T>, Unit>
{
    public LinkedListToVisitableAdapter(LinkedListNode<T> data) : base(data)
    {
    }

    public override Unit Accept(LinkedListNodePrinter<T> visitor) =>
        visitor.Visit(this);
}

public class LinkedListToVisitableAdapterFactory<T> :
    VisitableAdapterFactory<LinkedListNode<T>, LinkedListNodePrinter<T>, Unit>
{
    public override LinkedListToVisitableAdapter<T> Create(LinkedListNode<T> data) =>
        new(data);
}