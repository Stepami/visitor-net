using System;

namespace Visitor.NET.Tests.VisitableStructures;

public abstract record BinaryTreeNode : IVisitable
{
    public abstract TReturn Accept<TReturn>(IVisitor<TReturn> visitor);
}

public record Operation(char Symbol, BinaryTreeNode Left, BinaryTreeNode Right) : BinaryTreeNode
{
    public override TReturn Accept<TReturn>(IVisitor<TReturn> visitor)
    {
        if (visitor is IVisitor<Operation, TReturn> concreteVisitor)
            return concreteVisitor.Visit(this);
        throw new NotSupportedException();
    }
}

public record Number(double Value) : BinaryTreeNode
{
    public override TReturn Accept<TReturn>(IVisitor<TReturn> visitor)
    {
        if (visitor is IVisitor<Number, TReturn> concreteVisitor)
            return concreteVisitor.Visit(this);
        throw new NotSupportedException();
    }
}

public record Parenthesis(BinaryTreeNode Node) : BinaryTreeNode
{
    public override TReturn Accept<TReturn>(IVisitor<TReturn> visitor)
    {
        if (visitor is IVisitor<Parenthesis, TReturn> concreteVisitor)
            return concreteVisitor.Visit(this);
        throw new NotSupportedException();
    }
}