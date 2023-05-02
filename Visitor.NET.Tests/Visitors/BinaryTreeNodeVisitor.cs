using System.Text;
using Visitor.NET.Tests.VisitableStructures;

namespace Visitor.NET.Tests.Visitors;

public class BinaryTreeNodeVisitor : 
    IVisitor<Operation>, 
    IVisitor<Number>,
    IVisitor<Parenthesis>
{
    private readonly StringBuilder _sb = new ();
    
    public Unit Visit(Operation visitable)
    {
        visitable.Left.Accept(this);
        _sb.Append(visitable.Symbol);
        visitable.Right.Accept(this);
        return default;
    }

    public Unit Visit(Number visitable)
    {
        _sb.Append(visitable.Value);
        return default;
    }

    public Unit Visit(Parenthesis visitable)
    {
        _sb.Append('(');
        visitable.Node.Accept(this);
        _sb.Append(')');
        return default;
    }

    public override string ToString() => _sb.ToString();
}