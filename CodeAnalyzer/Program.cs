using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;

namespace CodeAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText("..//..//..//AutoComments.txt");
            var textWithoutComments = CodeWorker.GetTextWithoutAutoComments(text);
            File.WriteAllText("..//..//..//WithoutAutoComments.txt", textWithoutComments);
        }
    }
}
