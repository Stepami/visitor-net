using NUnit.Framework;
using Visitor.NET.Lib.Core;
using Visitor.NET.Tests.VisitableStructures;
using Visitor.NET.Tests.Visitors;

namespace Visitor.NET.Tests;

[TestFixture(Category = "Unit", TestOf = typeof(IVisitor<,>))]
public class VisitorTests
{
    private IVisitable _visitable;

    [SetUp]
    public void SetUp()
    {
        _visitable = new Operation('+',
            new Number(1),
            new Parenthesis(
                new Operation('+',
                    new Number(2),
                    new Number(3)
                )
            )
        );
    }
    
    [Test]
    public void VisitorVisitsCorrectly()
    {
        var binaryTree = (IVisitable<BinaryTreeNodeVisitor>)_visitable;
        var visitor = new BinaryTreeNodeVisitor();
        binaryTree.Accept(visitor);
        Assert.AreEqual("1+(2+3)", visitor.ToString());
    }

    [Test]
    public void EvaluatorComputesCorrectly()
    {
        var binaryTree = (IVisitable<BinaryTreeEvaluator, double>)_visitable;
        var visitor = new BinaryTreeEvaluator();
        Assert.AreEqual(6, binaryTree.Accept(visitor));
    }
}