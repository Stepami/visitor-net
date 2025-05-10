namespace Visitor.NET.AutoVisitableGen.Sample;

public interface IElement : IVisitable<IElement>;

[AutoVisitable<IElement>]
public partial class ElementA : IElement;

[AutoVisitable<IElement>]
public partial record ElementB : IElement;