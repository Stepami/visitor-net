![logo](visitor-net-logo.jpg)

Status:

![stars](https://img.shields.io/github/stars/stepami/visitor-net?style=flat-square&cacheSeconds=604800)
[![NuGet](https://img.shields.io/nuget/dt/Visitor.NET.svg)](https://www.nuget.org/packages/Visitor.NET/)

# Visitor.NET

First-ever acyclic generic extensible typesafe implementation of Visitor pattern for .NET **without any usage of dynamic cast**.

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
public class BinaryTreeEvaluator : VisitorBase<BinaryTreeNode, double>,
    IVisitor<Operation, double>,
    IVisitor<Number, double>,
    IVisitor<Parenthesis, double>
{
    public double Visit(Operation visitable) =>
        visitable.Symbol switch
        {
            '+' => visitable.Left.Accept(This) + visitable.Right.Accept(This),
            _ => throw new NotImplementedException()
        };

    public double Visit(Number visitable) => visitable.Value;

    public double Visit(Parenthesis visitable) => visitable.Node.Accept(This);
}
```

But then we have to tell structures we visit that they are visitable.

It is done through [`IVisitable<>`](Visitor.NET/IVisitable.cs) interface implementation:

```csharp
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
```

Basically, if you have access to source code of structure you want "visit", it's better to always have implementation:

```csharp
return visitor.Visit(this);
```

In case you would make `Visit` implementation procedure (i.e. have no returning value), use [`VisitUnit` type](https://en.wikipedia.org/wiki/Unit_type) as return type.

So, your method would look like this:

```csharp
public class SomeVisitor : IVisitor<Some>
{
    public VisitUnit Visit(Some visitable)
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
    VisitableAdapter<LinkedListNode<T>>,
    IVisitable<LinkedListToVisitableAdapter<T>>
{
    public LinkedListToVisitableAdapter(LinkedListNode<T> data) :
        base(data)
    {
    }

    public override TReturn Accept<TReturn>(
        IVisitor<VisitableAdapter<LinkedListNode<T>>, TReturn> visitor) =>
        Accept(visitor);

    public TReturn Accept<TReturn>(
        IVisitor<LinkedListToVisitableAdapter<T>, TReturn> visitor) =>
        visitor.Visit(this);
}
```

This adapter can be instantiated with [`VisitableAdapterFactory<>`](Visitor.NET/Adapter/VisitableAdapterFactory.cs) implementation:

```csharp
public class LinkedListToVisitableAdapterFactory<T> :
    VisitableAdapterFactory<LinkedListNode<T>>
{
    public override LinkedListToVisitableAdapter<T> Create(LinkedListNode<T> data) =>
        new(data);
}
```

Bringing it all together:

```csharp
public class LinkedListNodePrinter<T> : VisitorNoReturnBase<VisitableAdapter<LinkedListNode<T>>>,
    IVisitor<LinkedListToVisitableAdapter<T>>
{
    private readonly StringBuilder _sb = new();
    private readonly VisitableAdapterFactory<LinkedListNode<T>> _factory;

    public LinkedListNodePrinter(VisitableAdapterFactory<LinkedListNode<T>> factory) =>
        _factory = factory;

    public VisitUnit Visit(LinkedListToVisitableAdapter<T> visitable)
    {
        var node = visitable.Data;
        _sb.Append(node.Data);
        if (node.HasNext())
        {
            var next = _factory.Create(node.Next);
            _sb.Append("->");
            next.Accept(This);
        }

        return default;
    }

    public override string ToString() => _sb.ToString();
}
```

# Visitor.NET.AutoVisitableGen

If you do not want implement visitable manually, you can do it automatically with incremental source generator.

Install package : [https://www.nuget.org/packages/Visitor.NET.AutoVisitableGen](https://www.nuget.org/packages/Visitor.NET.AutoVisitableGen).

Then, rewrite the nodes type declarations like this:

```csharp
public abstract record BinaryTreeNode : IVisitable<BinaryTreeNode>
{
    public abstract TReturn Accept<TReturn>(
        IVisitor<BinaryTreeNode, TReturn> visitor);
}

[AutoVisitable<BinaryTreeNode>]
public partial record Operation(
    char Symbol,
    BinaryTreeNode Left,
    BinaryTreeNode Right) : BinaryTreeNode;

[AutoVisitable<BinaryTreeNode>]
public partial record Number(double Value) : BinaryTreeNode;

[AutoVisitable<BinaryTreeNode>]
public partial record Parenthesis(BinaryTreeNode Node) : BinaryTreeNode;
```