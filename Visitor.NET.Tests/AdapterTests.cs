using NUnit.Framework;
using Visitor.NET.Adapter;
using Visitor.NET.Tests.VisitableAdapters;
using Visitor.NET.Tests.Visitors;

namespace Visitor.NET.Tests;

[TestFixture(Category = "Unit", TestOf = typeof(VisitableAdapter<>))]
public class AdapterTests
{
    [Test]
    public void AdapterVisitsCorrectly()
    {
        var linkedList = new LinkedListNode<int>(3,
            new LinkedListNode<int>(1,
                new LinkedListNode<int>(7, null)
            )
        );

        VisitableAdapterFactory<LinkedListNode<int>> factory =
            new LinkedListToVisitableAdapterFactory<int>();

        IVisitable rootVisitable = factory.Create(linkedList);
        var visitor = new LinkedListNodePrinter<int>(factory);
        
        rootVisitable.Accept(visitor);
        
        Assert.AreEqual("3->1->7", visitor.ToString());
    }
}