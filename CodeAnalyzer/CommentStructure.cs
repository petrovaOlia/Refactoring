namespace CodeAnalyzer
{
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Структура комментария.
    /// </summary>
    public class CommentStructure
    {
        /// <summary>
        /// Лист комментарий.
        /// </summary>
        public SyntaxNode XmlElement;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="xmlElement">Лист комментарий.</param>
        public CommentStructure(SyntaxNode xmlElement)
        {
            XmlElement = xmlElement;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="xmlElement">Лист комментарий.</param>
        /// <param name="autoText">Текст автокомментария.</param>
        /// <param name="text">Текст комментария.</param>
        public CommentStructure(SyntaxNode xmlElement, string autoText, string text)
        {
            XmlElement = xmlElement;
            AutoText = autoText;
            Text = text;

            IsAuto = text == autoText;
        }

        /// <summary>
        /// Текст автокомментария.
        /// </summary>
        public string AutoText { get; set; }

        /// <summary>
        /// Комментарий автоматический?
        /// </summary>
        public bool IsAuto { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        public string Text { get; set; }
    }
}