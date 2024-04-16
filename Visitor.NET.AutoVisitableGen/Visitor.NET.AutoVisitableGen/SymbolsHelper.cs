﻿using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Visitor.NET.AutoVisitableGen;

public static class SymbolsHelper
{
    /// <summary>
    /// return list of types that contains type: <paramref name="type"/>.
    /// when the provided type is not nested empty list will be return.
    ///
    /// the order of element in list is top to bottom, deeper nested types is last. 
    /// </summary>
    /// <param name="type">type symbol</param>
    /// <returns>list of <see cref="INamedTypeSymbol"/> that containing the provided type</returns>
    public static List<INamedTypeSymbol> GetContainingTypes(INamedTypeSymbol type)
    {
        List<INamedTypeSymbol> containingTypes = new();

        INamedTypeSymbol? currentType = type.ContainingType;
        while (currentType != null)
        {
            containingTypes.Add(currentType);
            currentType = currentType.ContainingType;
        }
            
        // change to top to bottom order
        containingTypes.Reverse();
        
        return containingTypes;
    }
}