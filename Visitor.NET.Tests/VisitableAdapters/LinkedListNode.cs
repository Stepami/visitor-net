namespace Visitor.NET.Tests.VisitableAdapters;

public record LinkedListNode<T>(T Data, LinkedListNode<T> Next)
{
    public bool HasNext() => Next != null;
}