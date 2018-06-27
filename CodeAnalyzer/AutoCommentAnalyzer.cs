using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalyzer
{
    public class AutoCommentAnalyzer
    {
        public AutoCommentAnalyzer(SyntaxTrivia comment)
        {
            XmlСomment = comment;
            CommentOwner = comment.Token.Parent;
            OwnerKind = CommentOwner.Kind();

            Comments = new List<CommentStructure>();

            AnalyzeByType();
        }

        public SyntaxKind OwnerKind;

        /// <summary>
        /// Владелец комментария.
        /// </summary>
        public SyntaxNode CommentOwner;

        /// <summary>
        /// Комментарий.
        /// </summary>
        public SyntaxTrivia XmlСomment;

        public List<CommentStructure> Comments;
        

        public void GetCommentsStructure(string ownerName)
        {
            var xmlList = XmlСomment.GetStructure().ChildNodes().ToList();
                //.OfType<XmlElementSyntax>().ToList();

            foreach (var xmlElement in xmlList)
            {
                if (xmlElement.Kind() is SyntaxKind.XmlText)
                {
                    Comments.Add(new CommentStructure(xmlElement));
                    continue;
                }

                var xmlElementList = (XmlElementSyntax)xmlElement;
                var typeXml = xmlElementList.StartTag.Name.ToString();

                switch (typeXml)
                {
                    case "summary":
                        Comments.Add(new CommentStructure(xmlElementList, typeXml, ownerName, GetCommentText(xmlElementList)));
                        break;
                    case "param":
                        Comments.Add(new CommentStructure(xmlElementList, typeXml, AutoCommentRules.GenerateStandartAutoComment(GetParameterName(xmlElementList.StartTag)), GetCommentText(xmlElementList)));
                        break;
                    case "returns":
                        Comments.Add(new CommentStructure(xmlElementList, typeXml, string.Empty, GetCommentText(xmlElementList)));
                        break;
                }
            }
        }

        public string GetCommentText(SyntaxNode xmlElement)
        {
            return xmlElement.ChildNodes().OfType<XmlTextSyntax>().First()
                    .TextTokens.FirstOrDefault(x => x.IsKind(SyntaxKind.XmlTextLiteralToken)).Text;
        }
        public string GetParameterName(XmlElementStartTagSyntax startTag)
        {
            return startTag.Attributes.First().ChildNodes().OfType<IdentifierNameSyntax>().First().Identifier.Text;
        }

        public void AnalyzeClass(ClassDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }
        public void AnalyzeStruct(StructDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }
        public void AnalyzeInterface(InterfaceDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateInterfaceAutoComment(name));
        }
        public void AnalyzeConstructor(ConstructorDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateConstructorAutoComment(name));
        }
        public void AnalyzeField(FieldDeclarationSyntax declaration)
        {
            var name = declaration.Declaration.Variables.First().Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }
        public void AnalyzeProperty(PropertyDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GeneratePropertyAutoComment(name));
        }
        public void AnalyzeMethod(MethodDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }
        public void AnalyzeEnum(EnumDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }
        public void AnalyzeEnumMember(EnumMemberDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }
        public void AnalyzeDelegate(DelegateDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }
        public void AnalyzeEvent(EventFieldDeclarationSyntax declaration)
        {
            var name = declaration.Declaration.Variables.First().Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }

        public void AnalyzeByType()
        {
            switch (OwnerKind)
            {
                case SyntaxKind.ClassDeclaration:
                    AnalyzeClass((ClassDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.StructDeclaration:
                    AnalyzeStruct((StructDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.InterfaceDeclaration:
                    AnalyzeInterface((InterfaceDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.ConstructorDeclaration:
                    AnalyzeConstructor((ConstructorDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.FieldDeclaration:
                    AnalyzeField((FieldDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.PropertyDeclaration:
                    AnalyzeProperty((PropertyDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.MethodDeclaration:
                    AnalyzeMethod((MethodDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.EnumDeclaration:
                    AnalyzeEnum((EnumDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.EnumMemberDeclaration:
                    AnalyzeEnumMember((EnumMemberDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.DelegateDeclaration:
                    AnalyzeDelegate((DelegateDeclarationSyntax)CommentOwner);
                    break;
                case SyntaxKind.EventDeclaration:
                    AnalyzeEvent((EventFieldDeclarationSyntax)CommentOwner);
                    break;
                default:
                    break;
            }
        }
    }
}
