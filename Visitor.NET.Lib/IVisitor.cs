namespace Visitor.NET.Lib
{
    public interface IVisitor<TVisitable>
        where TVisitable : IVisitable<TVisitable>
    {
        void Visit(IVisitable<TVisitable> visitable);
    }
}