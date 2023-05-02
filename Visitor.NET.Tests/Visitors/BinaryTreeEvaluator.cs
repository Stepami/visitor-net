using System;
using Visitor.NET.Tests.VisitableStructures;

namespace Visitor.NET.Tests.Visitors;

public class BinaryTreeEvaluator :
    IVisitor<Operation, double>,
    IVisitor<Number, double>,
    IVisitor<Parenthesis, double>
{
    public double Visit(Operation visitable) =>
        visitable.Symbol switch
        {
            '+' => visitable.Left.Accept(this) + visitable.Right.Accept(this),
            _ => throw new NotImplementedException()
        };

    public double Visit(Number visitable) => visitable.Value;

    public double Visit(Parenthesis visitable) => visitable.Node.Accept(this);
}