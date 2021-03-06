﻿namespace CodeAnalyzer
{
    /// <summary>
    /// Правила генерации автокомментариев.
    /// </summary>
    public static class AutoCommentRules
    {
        /// <summary>
        /// Возвращает текст автокомментария.
        /// </summary>
        /// <param name="name"> Имя владельца комментария.</param>
        public static string GenerateAutoCommentText(string name)
        {
            var autoCommentText = $"{name[0]}";
            for (var i = 1; i < name.Length; i++)
            {
                if (char.IsUpper(name, i) || char.IsDigit(name, i))
                    autoCommentText += $" {name[i]}";
                else
                    autoCommentText += $"{name[i]}";
            }

            return autoCommentText.ToLower();
        }

        /// <summary>
        /// Возвращает текст атвокомментария summary для конструктора.
        /// </summary>
        /// <param name="name">Имя класса.</param>
        public static string GenerateConstructorAutoComment(string name)
        {
            return "Initializes a new instance of the";
        }

        /// <summary>
        /// Возвращает текст атвокомментария summary для интерфейсов.
        /// </summary>
        /// <param name="name">Имя операнда.</param>
        public static string GenerateInterfaceAutoComment(string name)
        {
            return $" The {name.Substring(1)} interface.";
        }

        /// <summary>
        /// Возвращает текст атвокомментария summary для свойств.
        /// </summary>
        /// <param name="name">Имя класса.</param>
        public static string GeneratePropertyAutoComment(string name)
        {
            return $"Gets or sets the {GenerateAutoCommentText(name)}.";
        }

        /// <summary>
        /// Возвращает текст атвокомментария returns.
        /// </summary>
        public static string GenerateReturnAutoComment()
        {
            return " The ";
        }

        /// <summary>
        /// Возвращает текст атвокомментария param и summary для классов, методов, структур, полей, перечислений, элементов перечислений и событий.
        /// </summary>
        /// <param name="name">Имя владельуа комментария.</param>
        public static string GenerateStandartAutoComment(string name)
        {
            return $" The {GenerateAutoCommentText(name)}.";
        }
    }
}