using CodeAnalyzer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalyzerTest
{
    [TestClass]
    public class CodeWorkerTest
    {
        [TestMethod]
        public void GetTextWithoutAutoComments()
        {
            var textWithAutoComment = @"    /// <summary>
    /// The my class.
    /// </summary>
    class MyClass{ }";

            var textWithoutAutoComment = @"   
    class MyClass{ }";

            var result = CodeWorker.GetTextWithoutAutoComments(textWithAutoComment);

            Assert.AreEqual(textWithoutAutoComment, result);
        }
    }
}
