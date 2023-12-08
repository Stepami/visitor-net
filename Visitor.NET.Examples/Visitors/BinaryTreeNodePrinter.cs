using System.Globalization;
using Visitor.NET.Examples.VisitableStructures;

namespace Visitor.NET.Examples.Visitors;

public class BinaryTreeNodePrinter : VisitorBase<BinaryTreeNode, string>,
    IVisitor<Operation, string>,
    IVisitor<Number, string>,
    IVisitor<Parenthesis, string>
{
    public string Visit(Operation visitable)
    {
        var leftStr = visitable.Left.Accept(This);
        var rightStr = visitable.Right.Accept(This);
        return $"{leftStr}{visitable.Symbol}{rightStr}";
    }

    public string Visit(Number visitable) =>
        visitable.Value.ToString(CultureInfo.InvariantCulture);

    public string Visit(Parenthesis visitable) =>
        $"({visitable.Node.Accept(This)})";
}