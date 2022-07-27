using System;
using NUnit.Framework;
using Rhino.Mocks;
using Visitor.NET.Lib;
using Visitor.NET.Tests.VisitableStructures;

namespace Visitor.NET.Tests;

[TestFixture(Category = "Unit", TestOf = typeof(IVisitor<>))]
public class VisitorTests
{
    [Test]
    public void VisitorVisitDoesNotThrow()
    {
        IVisitable<Node> linkedList = new Node(1,
            new Node(2,
                new Node(0, null)
            )
        );
        var visitor = MockRepository.GenerateMock<IVisitor<Node>>();
        
        visitor.Stub(x => x.Visit(Arg<Node>.Is.Anything))
            .Do((Action<Node>)(_ => { }));

        Assert.DoesNotThrow(() => linkedList.Accept(visitor));
    }
}