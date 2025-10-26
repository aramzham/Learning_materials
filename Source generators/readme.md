Code generator is just a code that produces code, the goal is to remove the repetitive parts of your tasks

target netstandard2.0 for all Roslyn analyzers

you'll need CodeAnalysis.CSharp and .Analyzers nuget packages

when you see <IncludeAssets> and <PrivateAssets>all</PrivateAssets> in your .csproj, it means that you've added analyzer package and not a lib, the analyzer classes won't show up when you use the project and you won't the analyzer at runtime

we don't want the generator to be at the output folder, it's only useful at compile time

have a look at this website
https://github.com/dotnet/roslyn/blob/main/src/RoslynAnalyzers/Microsoft.CodeAnalysis.Analyzers/Core/AnalyzerBannedSymbols.txt

```xml
<Folder Include="Generated\**"/>
        => include all the subfolders as well
```

debugging analyzers is different because the debugger is concerned about the runtime and the analyzers are run at compile time

the syntax tree represents what your code SAYS

the semantic model represents what your code MEANS, it's close to reflection

Roslyn is .net's compiler as a service

the method .RegisterPostInitializationOutput() is called once when the generator is first starting up
for files generated dynamically we need to call .RegisterSourceOutput

context.AddSource's hintName parameter becomes the file name

id - unique id for the nuget.org
version - stick to major.minor.build version convention
title - the name that will appear in nuget.org
copyright - for copyright you can use either a LICENSE file or just type MIT or other license keyword
project url - url of GitHub repo
license url - url to the license file
repository url - the clone url (.git url)
repository type - git
tags - terms that users will look for your package, space delimited list
release notes - changes made specifically in this project

you can use .txt, .json, .xml files to generate classes from, you can very neatly describe what you want to have in an xml file and then generate it using a generator

mark your .txt file as AdditionFile in your IDE so you can access it through context.AdditionalTextsProvider