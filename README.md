Status:

[![NuGet](https://img.shields.io/nuget/dt/Visitor.NET.svg)](https://www.nuget.org/packages/Visitor.NET/)

# Visitor.NET

With Visitor.NET you can develop typesafe acyclic visitors even if you do not have access to source code of visitable structures.

## Installation

### NuGet

Install package : [https://www.nuget.org/packages/Visitor.NET](https://www.nuget.org/packages/Visitor.NET).

### GitHub

- Clone locally this github repository
- Build the `Visitor.NET.sln` solution

## Projects using Visitor.NET

- [HydraScript](https://github.com/Stepami/extended-js-subset)

## Usage

### Basic Example

Let's say we have some expression-tree-like hierarchy, implementing basic arithmetics, like this:

```csharp
public abstract record BinaryTreeNode;

public record Operation(char Symbol, BinaryTreeNode Left, BinaryTreeNode Right) : BinaryTreeNode;

public record Number(double Value) : BinaryTreeNode;

public record Parenthesis(BinaryTreeNode Node) : BinaryTreeNode;
```
So we may want to traverse it in order to, for example, compute expression result.

First of all, we implement evaluator using [`IVisitor<,>`](Visitor.NET/IVisitor.cs) interface:

```csharp
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
```

But then we have to tell structures we visit that they are visitable.

It is done through [`IVisitable<,>`](Visitor.NET/IVisitable.cs) interface implementation:

```csharp
public abstract record BinaryTreeNode : 
    IVisitable<BinaryTreeEvaluator, double>
{
    public abstract double Accept(BinaryTreeEvaluator visitor);
}

public record Operation(char Symbol, BinaryTreeNode Left, BinaryTreeNode Right) : BinaryTreeNode
{
    public override double Accept(BinaryTreeEvaluator visitor) =>
        visitor.Visit(this);
}

public record Number(double Value) : BinaryTreeNode
{
    public override double Accept(BinaryTreeEvaluator visitor) =>
        visitor.Visit(this);
}

public record Parenthesis(BinaryTreeNode Node) : BinaryTreeNode
{
    public override double Accept(BinaryTreeEvaluator visitor) =>
        visitor.Visit(this);
}
```

Basically, if you have access to source code of structure you want "visit", it's better to always have implementation:

```csharp
return visitor.Visit(this);
```

In case you would make `Visit` implementation procedure (i.e. have no returning value), use [`Unit` type](https://en.wikipedia.org/wiki/Unit_type) as return type.

So, your method would look like this:

```csharp
public class SomeVisitor : IVisitor<Some>
{
    public Unit Visit(Some visitable)
    {
        //...
        return default;
    }
}
```
### Adapter Usage

Let's imagine you want to visit some structure defined outside of your project (library, dto, etc.):

```csharp
public record LinkedListNode<T>(T Data, LinkedListNode<T> Next)
{
    public bool HasNext() => Next != null;
}
```

So we may define wrapper around instance of this type which would [became visitable](Visitor.NET/Adapter/VisitableAdapter.cs):

```csharp
public class LinkedListToVisitableAdapter<T> : 
    VisitableAdapter<LinkedListNode<T>, LinkedListNodePrinter<T>>
{
    public LinkedListToVisitableAdapter(LinkedListNode<T> data) : base(data)
    {
    }

    public override Unit Accept(LinkedListNodePrinter<T> visitor) =>
        visitor.Visit(this);
}
```

This adapter can be instantiated with [`VisitableAdapterFactory<,,>`](Visitor.NET/Adapter/VisitableAdapterFactory.cs) implementation:

```csharp
public class LinkedListToVisitableAdapterFactory<T> :
    VisitableAdapterFactory<LinkedListNode<T>, LinkedListNodePrinter<T>>
{
    public override LinkedListToVisitableAdapter<T> Create(LinkedListNode<T> data) =>
        new(data);
}
```

Bringing it all together:

```csharp
public class LinkedListNodePrinter<T> : IVisitor<LinkedListToVisitableAdapter<T>>
{
    private readonly StringBuilder _sb = new();
    private readonly VisitableAdapterFactory<LinkedListNode<T>, LinkedListNodePrinter<T>> _factory;

    public LinkedListNodePrinter(VisitableAdapterFactory<LinkedListNode<T>, LinkedListNodePrinter<T>> factory)
    {
        _factory = factory;
    }

    public Unit Visit(LinkedListToVisitableAdapter<T> visitable)
    {
        var node = visitable.Data;
        _sb.Append(node.Data);
        if (node.HasNext())
        {
            var next = _factory.Create(node.Next);
            _sb.Append("->");
            next.Accept(this);
        }

        return default;
    }

    public override string ToString() => _sb.ToString();
}
```
