using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGenerator
{
    class ClassBuilder: IClassBuilder<ClassModel>
    {
        public ClassModel ClassModel { get; set; }

        public ClassBuilder()
        {
            ClassModel = new ClassModel();
        }

        public IClassBuilder<ClassModel> SetUsings(IEnumerable<string> usingNames)
        {
            ClassModel.Namespace = ClassModel.Namespace.AddUsings(
                usingNames.Select(uName=> SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(uName)))
                    .ToArray());

            return this;
        }

        public IClassBuilder<ClassModel> SetNamespace(string namespaceName)
        {
            ClassModel.Namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(namespaceName))
                .NormalizeWhitespace();

            return this;
        }

        public IClassBuilder<ClassModel> SetClassName(string className, SyntaxKind kind, params string[] baseTypes)
        {
            ClassModel.Class = SyntaxFactory.ClassDeclaration(className)
                .AddModifiers(SyntaxFactory.Token(kind));

            foreach (var type in baseTypes)
            {
                ClassModel.Class = ClassModel.Class.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(type)));
            }

            return this;
        }

        public IClassBuilder<ClassModel> SetFields(List<FieldModel> fieldsData) 
        {
            fieldsData.ForEach(field => ClassModel.Fields.Add(SyntaxFactory.FieldDeclaration(SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.ParseTypeName(field.Type))
                    .AddVariables(SyntaxFactory.VariableDeclarator(field.Name)))
                .AddModifiers(SyntaxFactory.Token(field.Modificator))));

            return this;
        }

        public IClassBuilder<ClassModel> SetProperties(List<PropertyModel> propertiesData)
        {
            propertiesData.ForEach(prop =>
                ClassModel.Properties.Add(
                    SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(prop.Type), prop.Name)
                        .AddModifiers(SyntaxFactory.Token(prop.Modificator))
                        .AddAccessorListAccessors(
                            SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                            SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                ));

            return this;
        }

        public IClassBuilder<ClassModel> SetMethods(List<MethodModel> methodsData)
        {
            methodsData.ForEach(method =>
                ClassModel.Methods.Add(SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(method.Type), method.Name)
                    .AddModifiers(SyntaxFactory.Token(method.Modificator))
                    .WithBody(SyntaxFactory.Block(SyntaxFactory.ParseStatement(method.Body)))
                ));
            
            return this;
        }

        public ClassModel Build()
        {
            ClassModel.Class = ClassModel.Class.AddMembers(
                ClassModel.Fields
                    .Union<MemberDeclarationSyntax>(ClassModel.Properties)
                    .Union(ClassModel.Methods).ToArray()
            );

            ClassModel.Namespace = ClassModel.Namespace.AddMembers(ClassModel.Class);

            return ClassModel;
        }
    }
}
