namespace Visitor.NET.AutoVisitableGen.Sample;

public class BinaryTreeEvaluator : VisitorBase<BinaryTreeNode, double>,
    IVisitor<Operation, double>,
    IVisitor<Number, double>,
    IVisitor<Parenthesis, double>
{
    public double Visit(Operation visitable) =>
        visitable.Symbol switch
        {
            '+' => visitable.Left.Accept(This) + visitable.Right.Accept(This),
            _ => throw new ArgumentOutOfRangeException(nameof(visitable.Symbol))
        };

    public double Visit(Number visitable) => visitable.Value;

    public double Visit(Parenthesis visitable) => visitable.Node.Accept(This);
}