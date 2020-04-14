using Microsoft.CodeAnalysis.CSharp;

namespace CodeGenerator
{
    public class TestMemberModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public SyntaxKind Modificator { get; set; }
    }
}
