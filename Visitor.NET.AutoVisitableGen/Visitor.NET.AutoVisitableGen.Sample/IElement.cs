namespace Visitor.NET.AutoVisitableGen.Sample;

public interface IElement : IVisitable<IElement>;

public abstract class ElementABase;

[AutoVisitable<IElement>]
public partial class ElementA : ElementABase, IElement;

[AutoVisitable<IElement>]
public partial record ElementB : IElement;

public abstract class ElementCBase : IElement
{
    public abstract TReturn Accept<TReturn>(IVisitor<IElement, TReturn> visitor);
}

[AutoVisitable<IElement>]
public partial class ElementC : ElementCBase;