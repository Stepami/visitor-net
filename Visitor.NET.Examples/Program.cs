using System;
using Visitor.NET;
using Visitor.NET.Adapter;
using Visitor.NET.Examples.VisitableAdapters;
using Visitor.NET.Examples.VisitableStructures;
using Visitor.NET.Examples.Visitors;

BinaryTreeNode visitable = new Operation('+',
    new Number(1),
    new Parenthesis(
        new Operation('+',
            new Number(2),
            new Number(3)
        )
    )
);

IVisitor<BinaryTreeNode, string> binaryTreeNodePrinter = new BinaryTreeNodePrinter();
Console.WriteLine(visitable.Accept(binaryTreeNodePrinter));
IVisitor<BinaryTreeNode, double> binaryTreeNodeEvaluator = new BinaryTreeEvaluator();
Console.WriteLine(visitable.Accept(binaryTreeNodeEvaluator));

var linkedList = new LinkedListNode<int>(3,
    new LinkedListNode<int>(1,
        new LinkedListNode<int>(7, null)
    )
);

VisitableAdapterFactory<LinkedListNode<int>> factory =
    new LinkedListToVisitableAdapterFactory<int>();

var rootVisitable = factory.Create(linkedList);
IVisitor<VisitableAdapter<LinkedListNode<int>>, VisitUnit> linkedListNodePrinter =
    new LinkedListNodePrinter<int>(factory);

rootVisitable.Accept(linkedListNodePrinter);
Console.WriteLine(linkedListNodePrinter);