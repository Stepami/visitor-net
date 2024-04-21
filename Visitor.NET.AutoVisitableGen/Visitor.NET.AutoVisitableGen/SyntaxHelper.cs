using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Visitor.NET.AutoVisitableGen;

internal static class SyntaxHelper
{
    /// <summary>
    /// return list of types that contains type: <paramref name="type"/>.
    /// when the provided type is not nested empty list will be return.
    /// 
    /// the order of element in list is top to bottom, deeper nested types is last. 
    /// </summary>
    /// <param name="type">type symbol</param>
    /// <param name="semanticModel"></param>
    /// <returns>list of <see cref="TypeDeclarationSyntax"/> that containing the provided type</returns>
    internal static List<ContainingTypeInfo> GetContainingTypes(TypeDeclarationSyntax type, SemanticModel semanticModel)
    {
        List<ContainingTypeInfo> containingTypes = new();

        SyntaxNode? currentType = type.Parent;
        while (currentType is TypeDeclarationSyntax containingType)
        {
            if (semanticModel.GetDeclaredSymbol(containingType) is not INamedTypeSymbol declaredSymbol) 
                return new List<ContainingTypeInfo>();

            ContainingTypeInfo containingTypeInfo = new(
                declaredSymbol.DeclaredAccessibility.ToString().ToLower(),
                containingType.Keyword.Text,
                containingType.Identifier.Text
            );
            containingTypes.Add(containingTypeInfo);
            currentType = currentType.Parent;
        }

        // change to top to bottom order
        containingTypes.Reverse();

        return containingTypes;
    }
}

internal record ContainingTypeInfo(string Accessibility, string Keyword, string Name);