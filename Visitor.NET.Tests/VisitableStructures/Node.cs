using Visitor.NET.Lib;

namespace Visitor.NET.Tests.VisitableStructures;

public record Node(int Value, Node? Next)
    : IVisitable<Node>;