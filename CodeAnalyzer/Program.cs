namespace CodeAnalyzer
{
    using System.IO;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var text = File.ReadAllText("..//..//..//AutoComments.txt");
            var textWithoutComments = CodeWorker.GetTextWithoutAutoComments(text);
            File.WriteAllText("..//..//..//WithoutAutoComments.txt", textWithoutComments);
        }
    }
}