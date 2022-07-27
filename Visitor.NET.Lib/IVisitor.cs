namespace Visitor.NET.Lib
{
    public interface IVisitor{}

    public interface IVisitor<in TVisitable> : IVisitor
        where TVisitable : IVisitable
    {
        void Visit(TVisitable visitable);
    }
    
    public interface IVisitor<in TVisitable, out T> : IVisitor
        where TVisitable : IVisitable
    {
        T Visit(TVisitable visitable);
    }
}