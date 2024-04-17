using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Visitor.NET.AutoVisitableGen;

public static class SyntaxHelper
{
    /// <summary>
    /// return list of types that contains type: <paramref name="type"/>.
    /// when the provided type is not nested empty list will be return.
    ///
    /// the order of element in list is top to bottom, deeper nested types is last. 
    /// </summary>
    /// <param name="type">type symbol</param>
    /// <returns>list of <see cref="TypeDeclarationSyntax"/> that containing the provided type</returns>
    public static List<TypeDeclarationSyntax> GetContainingTypes(TypeDeclarationSyntax type)
    {
        List<TypeDeclarationSyntax> containingTypes = new();

        SyntaxNode? currentType = type.Parent;
        while (currentType is TypeDeclarationSyntax containingType)
        {
            containingTypes.Add(containingType);
            currentType = currentType.Parent;
        }
            
        // change to top to bottom order
        containingTypes.Reverse();
        
        return containingTypes;
    }
}