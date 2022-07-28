namespace Visitor.NET.Lib.Core
{
    public interface IVisitable{}
    
    public interface IVisitable<in TVisitor> : IVisitable
        where TVisitor : IVisitor
    {
        void Accept(TVisitor visitor);
    }
    
    public interface IVisitable<in TVisitor, out T> : IVisitable
        where TVisitor : IVisitor
    {
        T Accept(TVisitor visitor);
    }
}