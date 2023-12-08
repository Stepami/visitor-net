using System.Text;
using Visitor.NET.Adapter;
using Visitor.NET.Examples.VisitableAdapters;

namespace Visitor.NET.Examples.Visitors;

public class LinkedListNodePrinter<T> : VisitorNoReturnBase<VisitableAdapter<LinkedListNode<T>>>,
    IVisitor<LinkedListToVisitableAdapter<T>>
{
    private readonly StringBuilder _sb = new();
    private readonly VisitableAdapterFactory<LinkedListNode<T>> _factory;

    public LinkedListNodePrinter(VisitableAdapterFactory<LinkedListNode<T>> factory) =>
        _factory = factory;

    public VisitUnit Visit(LinkedListToVisitableAdapter<T> visitable)
    {
        var node = visitable.Data;
        _sb.Append(node.Data);
        if (node.HasNext())
        {
            var next = _factory.Create(node.Next);
            _sb.Append("->");
            next.Accept(This);
        }

        return default;
    }

    public override string ToString() => _sb.ToString();
}