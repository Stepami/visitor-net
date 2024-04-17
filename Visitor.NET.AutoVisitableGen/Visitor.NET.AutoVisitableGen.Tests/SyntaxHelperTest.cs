using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace Visitor.NET.AutoVisitableGen.Tests;

public class SyntaxHelperTest
{
    [Fact]
    public void GetContainingTypes_OnNoContainingTypes_ReturnEmptyList()
    {
        var inputCode = CSharpSyntaxTree.ParseText(@"
public class NestedType {}
");
        CSharpCompilation compilation = CSharpCompilation.Create("test", new[] {inputCode}, Array.Empty<MetadataReference>());

        INamedTypeSymbol nestedType = (INamedTypeSymbol) compilation.GetSymbolsWithName("NestedType").Single();

        var nestedTypeSyntaxNode = (ClassDeclarationSyntax) nestedType.DeclaringSyntaxReferences.Single().GetSyntax();

        SemanticModel semanticModel = compilation.GetSemanticModel(nestedTypeSyntaxNode.SyntaxTree);
        List<ContainingTypeInfo> actualContainingTypes = SyntaxHelper.GetContainingTypes(nestedTypeSyntaxNode, semanticModel);
        
        Assert.Empty(actualContainingTypes);
    }
    
    [Fact]
    public void GetContainingTypes_OnMultipleContainingTypes_ReturnByOrderTopToBottom()
    {
        var inputCode = CSharpSyntaxTree.ParseText(@"
public class ContainingTypeA
{
    public class ContainingTypeB
    {
        public class ContainingTypeC
        {
            public class NestedType {}
        }
    }
}    
");
        var expectedContainingTypes = new List<ContainingTypeInfo>() {
            new("public", "class", "ContainingTypeA"),
            new("public", "class", "ContainingTypeB"),
            new("public", "class", "ContainingTypeC"),
        };
        
        CSharpCompilation compilation = CSharpCompilation.Create("test", new[] {inputCode}, Array.Empty<MetadataReference>());

        INamedTypeSymbol nestedType = (INamedTypeSymbol) compilation.GetSymbolsWithName("NestedType").Single();

        var nestedTypeSyntaxNode = (ClassDeclarationSyntax) nestedType.DeclaringSyntaxReferences.Single().GetSyntax();

        SemanticModel semanticModel = compilation.GetSemanticModel(nestedTypeSyntaxNode.SyntaxTree);
        List<ContainingTypeInfo> actualContainingTypes = SyntaxHelper.GetContainingTypes(nestedTypeSyntaxNode, semanticModel);
        
        Assert.Equal(expectedContainingTypes, actualContainingTypes);
    }
}