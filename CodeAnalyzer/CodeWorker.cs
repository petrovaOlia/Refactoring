using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalyzer
{
    public static class CodeWorker
    {
        public static string GetTextWithoutAutoComments(string text)
        {
            var tree = CSharpSyntaxTree.ParseText(text);
            var root = tree.GetRootAsync().Result;

            // Список xml-комментариев
            var commentsSyntaxTrivia = root.DescendantTrivia()
                .OfType<SyntaxTrivia>()
                .Where(x => x.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia));

            List<SyntaxNode> nodesForDelete = new List<SyntaxNode>();

            foreach (var comment in commentsSyntaxTrivia)
            {
                var analyzer = new AutoCommentAnalyzer(comment);

                for (int i = 1; i < analyzer.Comments.Count; i++)
                {
                    if (analyzer.Comments[i].XmlElement.Kind() is SyntaxKind.XmlText)
                        continue;

                    if (analyzer.Comments[i].IsAuto)
                    {
                        nodesForDelete.Add(analyzer.Comments[i - 1].XmlElement);
                        nodesForDelete.Add(analyzer.Comments[i].XmlElement);
                    }
                }

                foreach (var commentForDelete in analyzer.Comments)
                    if (commentForDelete.IsAuto)
                    //root.RemoveNode(commentForDelete.XmlElement, SyntaxRemoveOptions.KeepNoTrivia);
                    {
                        nodesForDelete.Add(commentForDelete.XmlElement);
                    }
            }

            if (nodesForDelete.Count == 0)
                return String.Empty;

            //File.WriteAllText(tree.FilePath, root.RemoveNodes(nodesForDelete.AsEnumerable(), SyntaxRemoveOptions.KeepNoTrivia).ToFullString());

            return root.RemoveNodes(nodesForDelete.AsEnumerable(), SyntaxRemoveOptions.KeepNoTrivia).ToFullString();
        }
    }
}
