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
        var tree = CSharpSyntaxTree.ParseText(@"
public class NestedType {}
");
        CSharpCompilation compilation = CSharpCompilation.Create("test", new[] {tree}, Array.Empty<MetadataReference>());

        INamedTypeSymbol nestedType = (INamedTypeSymbol) compilation.GetSymbolsWithName("NestedType").Single();

        var nestedTypeSyntaxNode = (ClassDeclarationSyntax) nestedType.DeclaringSyntaxReferences.Single().GetSyntax();

        List<TypeDeclarationSyntax> containingTypes = SyntaxHelper.GetContainingTypes(nestedTypeSyntaxNode);
        
        Assert.Empty(containingTypes);
    }
    
    [Fact]
    public void GetContainingTypes_OnMultipleContainingTypes_ReturnByOrderTopToBottom()
    {
        var tree = CSharpSyntaxTree.ParseText(@"
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
        CSharpCompilation compilation = CSharpCompilation.Create("test", new[] {tree}, Array.Empty<MetadataReference>());

        INamedTypeSymbol nestedType = (INamedTypeSymbol) compilation.GetSymbolsWithName("NestedType").Single();

        var nestedTypeSyntaxNode = (ClassDeclarationSyntax) nestedType.DeclaringSyntaxReferences.Single().GetSyntax();

        List<TypeDeclarationSyntax> containingTypes = SyntaxHelper.GetContainingTypes(nestedTypeSyntaxNode);
        
        Assert.Equal("ContainingTypeA", containingTypes[0].Identifier.Text);
        Assert.Equal("ContainingTypeB", containingTypes[1].Identifier.Text);
        Assert.Equal("ContainingTypeC", containingTypes[2].Identifier.Text);
        Assert.Equal(3, containingTypes.Count);
    }
}