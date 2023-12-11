namespace Visitor.NET.AutoVisitableGen.Sample;
/*
public abstract record BinaryTreeNode : IVisitable<BinaryTreeNode>
{
    public abstract TReturn Accept<TReturn>(
        IVisitor<BinaryTreeNode, TReturn> visitor);
}

[AutoVisitable<BinaryTreeNode>]
public partial record Operation(
    char Symbol,
    BinaryTreeNode Left,
    BinaryTreeNode Right)// : BinaryTreeNode
{
}

[AutoVisitable<BinaryTreeNode>]
public partial record Number(double Value)// : BinaryTreeNode
{
}

[AutoVisitable<BinaryTreeNode>]
public partial record Parenthesis(BinaryTreeNode Node)// : BinaryTreeNode
{
}
*/