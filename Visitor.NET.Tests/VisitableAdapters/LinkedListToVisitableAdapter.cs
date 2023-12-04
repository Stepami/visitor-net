using System;
using Visitor.NET.Adapter;

namespace Visitor.NET.Tests.VisitableAdapters;

public class LinkedListToVisitableAdapter<T> : 
    VisitableAdapter<LinkedListNode<T>>
{
    public LinkedListToVisitableAdapter(LinkedListNode<T> data) : base(data)
    {
    }

    public override TReturn Accept<TReturn>(IVisitor<TReturn> visitor)
    {
        if (visitor is IVisitor<LinkedListToVisitableAdapter<T>, TReturn> concreteVisitor)
            return concreteVisitor.Visit(this);
        throw new NotSupportedException();
    }
}

public class LinkedListToVisitableAdapterFactory<T> :
    VisitableAdapterFactory<LinkedListNode<T>>
{
    public override LinkedListToVisitableAdapter<T> Create(LinkedListNode<T> data) =>
        new(data);
}