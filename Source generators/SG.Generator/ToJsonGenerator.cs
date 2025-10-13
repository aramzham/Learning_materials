using System;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SG.Generator
{
    [Generator]
    public class ToJsonGenerator : IIncrementalGenerator
    {
        private const string HelloWorld = """
                                          using System;

                                          namespace SG.Generator;

                                          [AttributeUsage(AttributeTargets.Class)]
                                          public class ToJsonSerializerAttribute : Attribute;
                                          """;

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // #if DEBUG
            // if (!System.Diagnostics.Debugger.IsAttached)
            // {
            //     System.Diagnostics.Debugger.Launch();
            // }
            // #endif

            context.RegisterPostInitializationOutput(ctx =>
                ctx.AddSource("ToJsonSerializerAttribute.g.cs", SourceText.From(HelloWorld, Encoding.UTF8)));

            var provider = context.SyntaxProvider.CreateSyntaxProvider(Predicate, Transform);
            
            context.RegisterSourceOutput(provider, (productionContext, info) =>
            {
                throw new NotImplementedException();
            });
        }

        private ClassInfo? Transform(GeneratorSyntaxContext arg1, CancellationToken ct)
        {
            var classDeclarationSyntax = (ClassDeclarationSyntax)arg1.Node;

            foreach (var attributeList in classDeclarationSyntax.AttributeLists)
            {
                foreach (var attributeSyntax in attributeList.Attributes)
                {
                    if (ct.IsCancellationRequested)
                        return null;
                    
                    var attributeName = attributeSyntax.Name.ToString();
                    if (attributeName != "ToJsonSerializer" && attributeName != "ToJsonSerializerAttribute") continue;
                    
                    var attributeSymbolInfo = arg1.SemanticModel.GetSymbolInfo(attributeSyntax, ct);
                    
                    if (attributeSymbolInfo.Symbol is not IMethodSymbol methodSymbol) continue;
                    
                    var attributeSymbol = methodSymbol.ContainingType;
                    if (attributeSymbol.Name != "ToJsonSerializerAttribute" &&
                        attributeSymbol.Name != "ToJsonSerializer") 
                        continue;
                    
                    var classSymbol = arg1.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax, ct);
                    
                    var classInfo = new ClassInfo()
                    {
                        Namespace = classSymbol?.ContainingNamespace.ToString(),
                        Name = classSymbol?.Name
                    };

                    return classInfo;
                }
            }
            
            return new ClassInfo();
        }

        private static bool Predicate(SyntaxNode node, CancellationToken ct)
        {
            return node is ClassDeclarationSyntax { AttributeLists.Count: > 0 };
        }
    }

    public record struct ClassInfo
    {
        public string? Namespace { get; set; }
        public string? Name { get; set; }
    }
}

/*
T:System.Console; Analyzers should not be reading / writing to the console
T:System.Diagnostics.Process; Analyzers should not inspect or create processes
T:System.Diagnostics.ProcessStartInfo; Analyzers should not inspect or create processes
T:System.Environment; Analyzers should not read their settings directly from environment variables
P:System.Globalization.CultureInfo.CurrentCulture; Analyzers should use LocalizableResourceString for culture-dependent messages
P:System.Globalization.CultureInfo.CurrentUICulture; Analyzers should use LocalizableResourceString for culture-dependent messages
T:System.IO.File; Do not do file IO in analyzers
T:System.IO.Directory; Do not do file IO in analyzers
M:System.IO.Path.GetTempPath; Do not do file IO in analyzers
T:System.Random; Analyzers should be deterministic
M:System.Reflection.Assembly.Load(System.Byte[]); Analyzers should only load their dependencies via standard runtime mechanisms
M:System.Reflection.Assembly.Load(System.String); Analyzers should only load their dependencies via standard runtime mechanisms
M:System.Reflection.Assembly.Load(System.Reflection.AssemblyName); Analyzers should only load their dependencies via standard runtime mechanisms
M:System.Reflection.Assembly.Load(System.Byte[],System.Byte[]); Analyzers should only load their dependencies via standard runtime mechanisms
T:Microsoft.CodeAnalysis.GeneratorInitializationContext; Non-incremental source generators should not be used, implement IIncrementalGenerator instead
T:Microsoft.CodeAnalysis.GeneratorExecutionContext; Non-incremental source generators should not be used, implement IIncrementalGenerator instead
 */