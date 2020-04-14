using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGenerator
{
    interface IClassBuilder<T>
    {
        T ClassModel { get; set; }
        IClassBuilder<T> SetUsings(IEnumerable<string> usingNames);
        IClassBuilder<T> SetNamespace(string namespaceName);
        IClassBuilder<T> SetClassName(string className, SyntaxKind kind, params string[] baseTypes);
        IClassBuilder<T> SetFields(List<FieldModel> fieldsData);
        IClassBuilder<T> SetProperties(List<PropertyModel> propertiesData);
        IClassBuilder<T> SetMethods(List<MethodModel> methodsData);
        T Build();
    }
}
