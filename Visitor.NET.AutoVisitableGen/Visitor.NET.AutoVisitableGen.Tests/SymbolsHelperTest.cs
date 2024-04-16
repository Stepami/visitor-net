using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Visitor.NET.AutoVisitableGen.Tests;

public class SymbolsHelperTest
{
    [Fact]
    public void GetContainingTypes_OnNoContainingTypes_ReturnEmptyList()
    {
        var tree = CSharpSyntaxTree.ParseText(@"
public class NestedType {}
");
        CSharpCompilation compilation = CSharpCompilation.Create("test", new[] {tree}, Array.Empty<MetadataReference>());

        INamedTypeSymbol nestedType = (INamedTypeSymbol) compilation.GetSymbolsWithName("NestedType").Single();

        List<INamedTypeSymbol> containingTypes = SymbolsHelper.GetContainingTypes(nestedType);
        
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
        public class NestedType {}
    }
}    
");
        CSharpCompilation compilation = CSharpCompilation.Create("test", new[] {tree}, Array.Empty<MetadataReference>());

        INamedTypeSymbol nestedType = (INamedTypeSymbol) compilation.GetSymbolsWithName("NestedType").Single();

        List<INamedTypeSymbol> containingTypes = SymbolsHelper.GetContainingTypes(nestedType);
        
        Assert.Equal("ContainingTypeA", containingTypes[0].Name);
        Assert.Equal("ContainingTypeB", containingTypes[1].Name);
        Assert.Equal(2, containingTypes.Count);
    }
}