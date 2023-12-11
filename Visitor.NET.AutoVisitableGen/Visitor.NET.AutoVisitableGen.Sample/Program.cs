namespace Visitor.NET.AutoVisitableGen.Sample;

public class Program
{
    public static void Main(string[] _)
    {
        var node = new Operation(
            '+',
            new Number(1),
            new Parenthesis(
                new Operation(
                    '+',
                    new Number(2),
                    new Number(3))));

        Console.WriteLine(node.Accept<double>(new BinaryTreeEvaluator()));
    }
}