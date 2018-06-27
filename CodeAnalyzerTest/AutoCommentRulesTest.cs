using System;
using CodeAnalyzer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeAnalyzerTest
{
    [TestClass]
    public class AutoCommentRulesTest
    {
        [TestMethod]
        public void GenerateAutoCommentText()
        {
            var name = "CapacityOfPeat1B";

            var comment = AutoCommentRules.GenerateAutoCommentText(name);

            Assert.IsTrue(comment.Contains("capacity of peat 1 b"));
        }
    }
}
