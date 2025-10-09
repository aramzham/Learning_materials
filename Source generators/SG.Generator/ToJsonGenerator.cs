using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SG.Generator
{
    [Generator]
    public class ToJsonGenerator : IIncrementalGenerator
    {
        private const string HelloWorld = """
                                          namespace SG.Generator
                                          {
                                              public static class HelloWorld
                                              {
                                                  public static void SayHello() => Console.WriteLine("Hello World!");
                                              }
                                          }
                                          """;

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(ctx => ctx.AddSource("HelloWorld.g.cs", SourceText.From(HelloWorld, Encoding.UTF8)));
        }
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