using System.Text;
using Visitor.NET.Adapter;
using Visitor.NET.Tests.VisitableAdapters;

namespace Visitor.NET.Tests.Visitors;

public class LinkedListNodePrinter<T> : IVisitor<LinkedListToVisitableAdapter<T>>
{
    private readonly StringBuilder _sb = new();
    private readonly VisitableAdapterFactory<LinkedListNode<T>, LinkedListNodePrinter<T>> _factory;

    public LinkedListNodePrinter(VisitableAdapterFactory<LinkedListNode<T>, LinkedListNodePrinter<T>> factory)
    {
        _factory = factory;
    }

    public Unit Visit(LinkedListToVisitableAdapter<T> visitable)
    {
        var node = visitable.Data;
        _sb.Append(node.Data);
        if (node.HasNext())
        {
            var next = _factory.Create(node.Next);
            _sb.Append("->");
            next.Accept(this);
        }

        return default;
    }

    public override string ToString() => _sb.ToString();
}