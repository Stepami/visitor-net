using Visitor.NET.Adapter;

namespace Visitor.NET.Examples.VisitableAdapters;

public class LinkedListToVisitableAdapter<T> :
    VisitableAdapter<LinkedListNode<T>>,
    IVisitable<LinkedListToVisitableAdapter<T>>
{
    public LinkedListToVisitableAdapter(LinkedListNode<T> data) :
        base(data)
    {
    }

    public override TReturn Accept<TReturn>(
        IVisitor<VisitableAdapter<LinkedListNode<T>>, TReturn> visitor) =>
        Accept(visitor);

    public TReturn Accept<TReturn>(
        IVisitor<LinkedListToVisitableAdapter<T>, TReturn> visitor) =>
        visitor.Visit(this);
}

public class LinkedListToVisitableAdapterFactory<T> :
    VisitableAdapterFactory<LinkedListNode<T>>
{
    public override VisitableAdapter<LinkedListNode<T>> Create(LinkedListNode<T> data) =>
        new LinkedListToVisitableAdapter<T>(data);
}