using System.Collections.Generic;

namespace Visitor.NET.AutoVisitableGen;

internal record VisitableInfo(
    TypeKind Kind,
    string BaseTypeName,
    bool IsBaseInterface,
    bool IsParentInterface,
    string TypeName,
    string? NamespaceName,
    string? AccessModifier,
    List<ContainingTypeInfo> ContainingTypes);

internal enum TypeKind
{
    Class,
    Record
}