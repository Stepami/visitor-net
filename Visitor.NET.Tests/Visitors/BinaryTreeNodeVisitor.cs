using System.Text;
using Visitor.NET.Lib;
using Visitor.NET.Tests.VisitableStructures;

namespace Visitor.NET.Tests.Visitors;

public class BinaryTreeNodeVisitor : 
    IVisitor<Operation>, 
    IVisitor<Number>,
    IVisitor<Parenthesis>
{
    private readonly StringBuilder _sb = new ();
    
    public void Visit(Operation visitable)
    {
        visitable.Left.Accept(this);
        _sb.Append(visitable.Symbol);
        visitable.Right.Accept(this);
    }

    public void Visit(Number visitable) => _sb.Append(visitable.Value);

    public void Visit(Parenthesis visitable)
    {
        _sb.Append('(');
        visitable.Node.Accept(this);
        _sb.Append(')');
    }

    public override string ToString() => _sb.ToString();
}