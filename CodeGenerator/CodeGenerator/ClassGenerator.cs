using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGenerator
{
    public class ClassGenerator
    {
        public static string GetClass()
        {
            return new ClassBuilder()
                .SetNamespace("CodeGenerationSample")
                .SetUsings(new List<string>() { "System" })
                .SetClassName("Order", SyntaxKind.PublicKeyword, new[] { "BaseEntity<Order>", "IHaveIdentity" })
                .SetFields(new List<FieldModel>()
                {
                    new FieldModel(){Name = "canceled", Modificator = SyntaxKind.PrivateKeyword, Type = "bool"}
                })
                .SetProperties(new List<PropertyModel>()
                {
                    new PropertyModel(){Name = "Quantity", Type = "int", Modificator = SyntaxKind.PublicKeyword}
                })
                .SetMethods(new List<MethodModel>()
                {
                    new MethodModel()
                    {
                        Name = "MarkAsCanceled",
                        Type = "void",
                        Modificator = SyntaxKind.PublicKeyword,
                        Body = "canceled = true;"
                    }
                })
                .Build()
                .ToString();
        }
    }
}

