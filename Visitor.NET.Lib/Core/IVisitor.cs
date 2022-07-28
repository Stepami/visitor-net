namespace Visitor.NET.Lib.Core
{
    public interface IVisitor{}
    
    public interface IVisitor<in TVisitable, out T> : IVisitor
        where TVisitable : IVisitable
    {
        T Visit(TVisitable visitable);
    }
}