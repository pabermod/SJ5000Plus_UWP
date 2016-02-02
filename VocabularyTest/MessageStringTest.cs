using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Vocabulary;
using Vocabulary.Messages;

namespace VocabularyTest
{
    [TestClass]
    public class MessageStringTest
    {

        [TestMethod]
        public void GetTokenString()
        {
            UserTokenMessage tokenMsg = new UserTokenMessage();
            System.Diagnostics.Debug.WriteLine(tokenMsg);
        }
    }
}
