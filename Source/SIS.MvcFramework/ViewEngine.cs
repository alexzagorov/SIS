using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework
{
    public class ViewEngine : IViewEngine
    {
        public string GetHtml(string templateHtml, object moedel)
        {
            var methiodCode = PrepapreCSharpCode(templateHtml);
            var code = $@"using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using SIS.MvcFramework;

namespace AppViewNamespace
{{
    public class AppViewCode : IView
    {{
        public string GetHtml(object model)
        {{
            var html = new StringBuilder();
            {methiodCode}
            return html.ToString();
        }}
    }}
}}";

            IView view = GetInstanceFromCode(code, moedel);
            string html = view.GetHtml(moedel);
            return html;
        }

        private IView GetInstanceFromCode(string code, object model)
        {
            var compilation = CSharpCompilation.Create("AppViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(model.GetType().Assembly.Location));
            var libraries = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();
            foreach (var library in libraries)
            {
                compilation = compilation.AddReferences(MetadataReference.CreateFromFile(Assembly.Load(library).Location));
            }

            compilation.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(code));

            compilation.Emit("a.dll");

            return null;
        }

        private string PrepapreCSharpCode(string templateHtml)
        {
            return string.Empty;
        }
    }
}
