using Visitor.NET.Lib.Core;
using Visitor.NET.Tests.Visitors;

namespace Visitor.NET.Tests.VisitableStructures;

public abstract record BinaryTreeNode : IVisitable<BinaryTreeNodeVisitor>, IVisitable<BinaryTreeEvaluator, double>
{
    public abstract void Accept(BinaryTreeNodeVisitor visitor);
    
    public abstract double Accept(BinaryTreeEvaluator visitor);
}

public record Operation(char Symbol, BinaryTreeNode Left, BinaryTreeNode Right) : BinaryTreeNode
{
    public override void Accept(BinaryTreeNodeVisitor visitor) =>
        visitor.Visit(this);
    
    public override double Accept(BinaryTreeEvaluator visitor) =>
        visitor.Visit(this);
}

public record Number(double Value) : BinaryTreeNode
{
    public override void Accept(BinaryTreeNodeVisitor visitor) =>
        visitor.Visit(this);
    
    public override double Accept(BinaryTreeEvaluator visitor) =>
        visitor.Visit(this);
}

public record Parenthesis(BinaryTreeNode Node) : BinaryTreeNode
{
    public override void Accept(BinaryTreeNodeVisitor visitor) =>
        visitor.Visit(this);
    
    public override double Accept(BinaryTreeEvaluator visitor) =>
        visitor.Visit(this);
}