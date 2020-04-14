using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGenerator
{
    public class ClassModel
    {
        public NamespaceDeclarationSyntax Namespace { get; set; }

        public ClassDeclarationSyntax Class { get; set; }

        public List<BaseFieldDeclarationSyntax> Fields { get; set; }

        public List<PropertyDeclarationSyntax> Properties { get; set; }

        public List<MethodDeclarationSyntax> Methods { get; set; }

        public override string ToString() => Namespace.NormalizeWhitespace().ToFullString();

        public ClassModel()
        {
            Fields=new List<BaseFieldDeclarationSyntax>();
            Properties=new List<PropertyDeclarationSyntax>();
            Methods=new List<MethodDeclarationSyntax>();
        }
    }
}
