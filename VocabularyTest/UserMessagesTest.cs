using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Vocabulary.Messages;

namespace VocabularyTest
{
    [TestClass]
    public class UserMessagesTest
    {
        static int token = 5;

        [TestMethod]
        public void CreateUserTokenMessage()
        {
            UserTokenMessage tokenMsg = new UserTokenMessage();
            Assert.AreEqual(tokenMsg.msg_id, 257); //Must be 257
            Assert.AreEqual(tokenMsg.token, 0);
        }

        [TestMethod]
        public void CreateUserSettingsMessage()
        {
            UserSettingsMessage UserGetSettings = new UserSettingsMessage(token);
            Assert.AreEqual(UserGetSettings.token, token);
            Assert.AreEqual(UserGetSettings.msg_id, 3); //MUST be 3
        }

        static string Param = "video_resolution";
        static string Value = "1920x1080 60P 16:9";

        [TestMethod]
        public void CreateUserGetParamValuesMessage()
        {
            UserGetParamValuesMessage GetParam = new UserGetParamValuesMessage(token, Param);
            Assert.AreEqual(GetParam.token, token);
            Assert.AreEqual(GetParam.param, Param);
            Assert.AreEqual(GetParam.msg_id, 9); //MUST be 9
        }

        [TestMethod]
        public void CreateUserSetParamMessage()
        {
            UserSetParamMessage SetParam = new UserSetParamMessage(token, Param, Value);
            Assert.AreEqual(SetParam.token, token);
            Assert.AreEqual(SetParam.type, Param);
            Assert.AreEqual(SetParam.param, Value);
            Assert.AreEqual(SetParam.msg_id, 2); //MUST be 2
        }
    }
}
