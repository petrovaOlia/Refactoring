namespace CodeAnalyzer
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Анализатор кода на предмет содержания автокомментариев.
    /// </summary>
    public class AutoCommentAnalyzer
    {
        /// <summary>
        /// Владелец комментария.
        /// </summary>
        public SyntaxNode CommentOwner;

        /// <summary>
        /// Список элементов комментария.
        /// </summary>
        public List<CommentStructure> Comments;

        /// <summary>
        /// Тип владельца комментария.
        /// </summary>
        public SyntaxKind OwnerKind;

        /// <summary>
        /// Комментарий.
        /// </summary>
        public SyntaxTrivia XmlСomment;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="comment">Комментарий.</param>
        public AutoCommentAnalyzer(SyntaxTrivia comment)
        {
            XmlСomment = comment;
            CommentOwner = comment.Token.Parent;
            OwnerKind = CommentOwner.Kind();

            Comments = new List<CommentStructure>();

            AnalyzeByType();
        }

        /// <summary>
        /// Анализ в зависимости от типа владельца комментария.
        /// </summary>
        public void AnalyzeByType()
        {
            switch (OwnerKind)
            {
                case SyntaxKind.ClassDeclaration:
                    AnalyzeClass((ClassDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.StructDeclaration:
                    AnalyzeStruct((StructDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.InterfaceDeclaration:
                    AnalyzeInterface((InterfaceDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.ConstructorDeclaration:
                    AnalyzeConstructor((ConstructorDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.FieldDeclaration:
                    AnalyzeField((FieldDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.PropertyDeclaration:
                    AnalyzeProperty((PropertyDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.MethodDeclaration:
                    AnalyzeMethod((MethodDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.EnumDeclaration:
                    AnalyzeEnum((EnumDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.EnumMemberDeclaration:
                    AnalyzeEnumMember((EnumMemberDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.DelegateDeclaration:
                    AnalyzeDelegate((DelegateDeclarationSyntax) CommentOwner);
                    break;
                case SyntaxKind.EventDeclaration:
                    AnalyzeEvent((EventFieldDeclarationSyntax) CommentOwner);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Анализ комментария класса.
        /// </summary>
        /// <param name="declaration">Лист объявления класса.</param>
        public void AnalyzeClass(ClassDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария конструктора.
        /// </summary>
        /// <param name="declaration">Лист объявления конструктора.</param>
        public void AnalyzeConstructor(ConstructorDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateConstructorAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария делегата.
        /// </summary>
        /// <param name="declaration">Лист объявления делегата.</param>
        public void AnalyzeDelegate(DelegateDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария перечисления.
        /// </summary>
        /// <param name="declaration">Лист объявления перечисления.</param>
        public void AnalyzeEnum(EnumDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария элемента перечисления.
        /// </summary>
        /// <param name="declaration">Лист объявления элемента перечисления.</param>
        public void AnalyzeEnumMember(EnumMemberDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария события.
        /// </summary>
        /// <param name="declaration">Лист объявления события.</param>
        public void AnalyzeEvent(EventFieldDeclarationSyntax declaration)
        {
            var name = declaration.Declaration.Variables.First().Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария поля.
        /// </summary>
        /// <param name="declaration">Лист объявления поля.</param>
        public void AnalyzeField(FieldDeclarationSyntax declaration)
        {
            var name = declaration.Declaration.Variables.First().Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария интерфейса.
        /// </summary>
        /// <param name="declaration">Лист объявления интерфейса.</param>
        public void AnalyzeInterface(InterfaceDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateInterfaceAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария метода.
        /// </summary>
        /// <param name="declaration">Лист объявления метода.</param>
        public void AnalyzeMethod(MethodDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария свойства.
        /// </summary>
        /// <param name="declaration">Лист объявления свойства.</param>
        public void AnalyzeProperty(PropertyDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GeneratePropertyAutoComment(name));
        }

        /// <summary>
        /// Анализ комментария структуры.
        /// </summary>
        /// <param name="declaration">Лист объявления структуры.</param>
        public void AnalyzeStruct(StructDeclarationSyntax declaration)
        {
            var name = declaration.Identifier.Text;
            GetCommentsStructure(AutoCommentRules.GenerateStandartAutoComment(name));
        }

        /// <summary>
        /// Возвращает структуру комментария.
        /// </summary>
        /// <param name="ownerName">Имя владельца комментария.</param>
        public void GetCommentsStructure(string ownerName)
        {
            var xmlList = XmlСomment.GetStructure().ChildNodes().ToList();

            foreach (var xmlElement in xmlList)
            {
                if (xmlElement.Kind() is SyntaxKind.XmlText)
                {
                    Comments.Add(new CommentStructure(xmlElement));
                    continue;
                }

                var xmlElementList = (XmlElementSyntax) xmlElement;
                var typeXml = xmlElementList.StartTag.Name.ToString();

                switch (typeXml)
                {
                    case "summary":
                        Comments.Add(new CommentStructure(xmlElementList, ownerName, GetCommentText(xmlElementList)));
                        break;
                    case "param":
                        Comments.Add(new CommentStructure(xmlElementList,
                            AutoCommentRules.GenerateStandartAutoComment(GetParameterName(xmlElementList.StartTag)),
                            GetCommentText(xmlElementList)));
                        break;
                    case "returns":
                        Comments.Add(new CommentStructure(xmlElementList, AutoCommentRules.GenerateReturnAutoComment(),
                            GetCommentText(xmlElementList)));
                        break;
                }
            }
        }

        /// <summary>
        /// Возвращает текст комментария.
        /// </summary>
        /// <param name="xmlElement">Лист комментария.</param>
        public string GetCommentText(SyntaxNode xmlElement)
        {
            return xmlElement.ChildNodes().OfType<XmlTextSyntax>().First()
                .TextTokens.FirstOrDefault(x => x.IsKind(SyntaxKind.XmlTextLiteralToken)).Text;
        }

        /// <summary>
        /// Возвращает имя параметра. 
        /// </summary>
        /// <param name="startTag">Лист стртого тега param</param>
        public string GetParameterName(XmlElementStartTagSyntax startTag)
        {
            return startTag.Attributes.First().ChildNodes().OfType<IdentifierNameSyntax>().First().Identifier.Text;
        }
    }
}