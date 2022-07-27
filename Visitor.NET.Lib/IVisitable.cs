namespace Visitor.NET.Lib
{
    public interface IVisitable<TVisitable>
        where TVisitable : IVisitable<TVisitable>
    {
        void Accept(IVisitor<TVisitable> visitor) => visitor.Visit(this);
    }
}