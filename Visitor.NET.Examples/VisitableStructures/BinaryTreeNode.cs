namespace Visitor.NET.Examples.VisitableStructures;

public abstract record BinaryTreeNode : IVisitable<BinaryTreeNode>
{
    public abstract TReturn Accept<TReturn>(
        IVisitor<BinaryTreeNode, TReturn> visitor);
}

public record Operation(
    char Symbol,
    BinaryTreeNode Left,
    BinaryTreeNode Right) : BinaryTreeNode, IVisitable<Operation>
{
    public override TReturn Accept<TReturn>(
        IVisitor<BinaryTreeNode, TReturn> visitor) =>
        Accept(visitor);

    public TReturn Accept<TReturn>(
        IVisitor<Operation, TReturn> visitor) =>
        visitor.Visit(this);
}

public record Number(double Value) : BinaryTreeNode, IVisitable<Number>
{
    public override TReturn Accept<TReturn>(
        IVisitor<BinaryTreeNode, TReturn> visitor) =>
        Accept(visitor);

    public TReturn Accept<TReturn>(
        IVisitor<Number, TReturn> visitor) =>
        visitor.Visit(this);
}

public record Parenthesis(BinaryTreeNode Node) : BinaryTreeNode, IVisitable<Parenthesis>
{
    public override TReturn Accept<TReturn>(
        IVisitor<BinaryTreeNode, TReturn> visitor) =>
        Accept(visitor);

    public TReturn Accept<TReturn>(
        IVisitor<Parenthesis, TReturn> visitor) =>
        visitor.Visit(this);
}