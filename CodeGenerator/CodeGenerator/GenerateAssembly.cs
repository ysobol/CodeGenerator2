using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
namespace CodeGenerator
{
    public static class GenerateAssembly
    {
        public static void Run(string path, string assemblyName)
        {
            string code = ClassGenerator.GetClass();

            var assembly = CSharpCompilation
                .Create(assemblyName).WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location))
                .AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(code));

            var compilationResult = assembly.Emit(path, assemblyName);

            if (!compilationResult.Success)
            {
                compilationResult.Diagnostics.ToList().ForEach(issue =>
                    Console.WriteLine(
                        $"ID: {issue.Id}, Message: {issue.GetMessage()}, Location: { issue.Location.GetLineSpan()}, Severity: { issue.Severity}")
                );
            }
        }
    }
}