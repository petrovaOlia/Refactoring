namespace CodeAnalyzer
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Работник с кодом.
    /// </summary>
    public static class CodeWorker
    {
        /// <summary>
        /// Возвращает текст кода без автокомментариев.
        /// </summary>
        /// <param name="text">Текст кода.</param>
        public static string GetTextWithoutAutoComments(string text)
        {
            var tree = CSharpSyntaxTree.ParseText(text);
            var root = tree.GetRootAsync().Result;

            // Список xml-комментариев
            var commentsSyntaxTrivia = root.DescendantTrivia()
                .OfType<SyntaxTrivia>()
                .Where(x => x.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia));

            var nodesForDelete = new List<SyntaxNode>();

            foreach (var comment in commentsSyntaxTrivia)
            {
                var analyzer = new AutoCommentAnalyzer(comment);

                for (var i = 1; i < analyzer.Comments.Count; i++)
                {
                    if (analyzer.Comments[i].XmlElement.Kind() is SyntaxKind.XmlText)
                        continue;

                    if (analyzer.Comments[i].IsAuto)
                    {
                        nodesForDelete.Add(analyzer.Comments[i - 1].XmlElement);
                        nodesForDelete.Add(analyzer.Comments[i].XmlElement);
                    }
                }
            }

            if (nodesForDelete.Count == 0)
                return string.Empty;

            return root.RemoveNodes(nodesForDelete.AsEnumerable(), SyntaxRemoveOptions.KeepNoTrivia).ToFullString();
        }
    }
}