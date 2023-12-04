using NUnit.Framework;
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
        var visitor = new BinaryTreeNodeVisitor();
        _visitable.Accept(visitor);
        Assert.AreEqual("1+(2+3)", visitor.ToString());
    }

    [Test]
    public void EvaluatorComputesCorrectly()
    {
        var visitor = new BinaryTreeEvaluator();
        Assert.AreEqual(6, _visitable.Accept(visitor));
    }
}