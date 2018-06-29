using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalyzer
{
    public class CommentStructure
    {
        public CommentStructure(SyntaxNode xmlElement)
        {
            XmlElement = xmlElement;
        }

        public CommentStructure(SyntaxNode xmlElement, string autoText, string text)
        {
            XmlElement = xmlElement;
            AutoText = autoText;
            Text = text;

            IsAuto = text == autoText;
        }

        public SyntaxNode XmlElement;
        public string Text;
        public string AutoText;
        public bool IsAuto;


    }
}
