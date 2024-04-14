using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Visitor.NET.AutoVisitableGen;

[Generator]
public class AutoVisitableIncrementalSourceGenerator : IIncrementalGenerator
{
    private const string AttributeSourceCode = @"// <auto-generated/>

namespace Visitor.NET;
                   
[System.AttributeUsage(System.AttributeTargets.Class)]
public class AutoVisitableAttribute<T> : System.Attribute
    where T : IVisitable<T>
{
}
";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "AutoVisitableAttribute.g.cs",
            SourceText.From(AttributeSourceCode, Encoding.UTF8)));

        var provider = context.SyntaxProvider
            .ForAttributeWithMetadataName("Visitor.NET.AutoVisitableAttribute`1",
                static (s, _) => IsSyntaxTargetForGeneration(s),
                static (ctx, _) => GetTypeDeclarationForSourceGen(ctx))
            .Where(t => t is not null)
            .Select((x, _) => x!);

        context.RegisterImplementationSourceOutput(context.CompilationProvider.Combine(provider.Collect()),
            (ctx, t) => GenerateCode(ctx, t.Left, t.Right));
    }


    private static VisitableInfo? GetTypeDeclarationForSourceGen(
        GeneratorAttributeSyntaxContext context)
    {
        var typeDeclarationSyntax = (TypeDeclarationSyntax)context.TargetNode;

        var baseType = GetBaseType(context, typeDeclarationSyntax);
        if (baseType is null)
        {
            return null;
        }

        var kind = GetTypeKind(typeDeclarationSyntax);
        if (kind is null)
        {
            return null;
        }

        var visitableName = typeDeclarationSyntax.Identifier.Text;

        return new VisitableInfo(
            kind.Value,
            baseType,
            visitableName,
            typeDeclarationSyntax);
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node) =>
        node is TypeDeclarationSyntax candidate &&
        candidate.Modifiers.Any(SyntaxKind.PublicKeyword) &&
        candidate.Modifiers.Any(SyntaxKind.PartialKeyword) &&
        !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);

    private static TypeKind? GetTypeKind(TypeDeclarationSyntax typeDeclarationSyntax)
    {
        return typeDeclarationSyntax switch
        {
            ClassDeclarationSyntax => TypeKind.Class,
            RecordDeclarationSyntax => TypeKind.Record,
            _ => null
        };
    }

    private static string? GetBaseType(GeneratorAttributeSyntaxContext context,
        TypeDeclarationSyntax typeDeclarationSyntax)
    {
        var attribute = typeDeclarationSyntax.AttributeLists
            .SelectMany(attributeListSyntax => attributeListSyntax.Attributes)
            .FirstOrDefault(attributeSyntax =>
            {
                if (ModelExtensions.GetSymbolInfo(
                        context.SemanticModel,
                        attributeSyntax).Symbol is not IMethodSymbol)
                    return false;

                if (attributeSyntax.Name is not GenericNameSyntax genericAttribute)
                    return false;

                var attributeName = genericAttribute.Identifier.Text;

                return attributeName is "AutoVisitable" or "AutoVisitableAttribute";
            });

        var baseType = (attribute?.Name as GenericNameSyntax)?
            .TypeArgumentList.Arguments
            .FirstOrDefault()?.ToString();
        return baseType;
    }

    private static void GenerateCode(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<VisitableInfo> visitableInfos)
    {
        foreach (var (typeKind, baseTypeName, visitableTypeName, typeDeclarationSyntax) in visitableInfos)
        {
            var semanticModel = compilation.GetSemanticModel(typeDeclarationSyntax.SyntaxTree);

            if (ModelExtensions.GetDeclaredSymbol(
                    semanticModel,
                    typeDeclarationSyntax) is not INamedTypeSymbol classSymbol)
                continue;

            var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

            var code = $@"// <auto-generated/>

using Visitor.NET;

namespace {namespaceName};

public partial {typeKind.ToString().ToLower()} {visitableTypeName} :
    IVisitable<{visitableTypeName}>
{{
    public override TReturn Accept<TReturn>(
        IVisitor<{baseTypeName}, TReturn> visitor) =>
        Accept(visitor);

    public TReturn Accept<TReturn>(
        IVisitor<{visitableTypeName}, TReturn> visitor) =>
        visitor.Visit(this);
}}
";

            context.AddSource(
                $"{visitableTypeName}.g.cs",
                SourceText.From(code, Encoding.UTF8));
        }
    }
}

internal record VisitableInfo(
    TypeKind Kind,
    string BaseTypeName,
    string VisitableTypeName,
    TypeDeclarationSyntax TypeDeclarationSyntax);

internal enum TypeKind
{
    Class,
    Record
}