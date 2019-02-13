using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherBot;

namespace WeatherBotTests
{
    [TestClass]
    public class WeatherBotLogicTests
    {
        [TestMethod]
        public void getSearchTextTest()
        {
            Assert.AreEqual("Warszawa", BotLogic.getSearchText("Warszawa?"));
            Assert.AreEqual("London", BotLogic.getSearchText("Check London!"));
            Assert.AreEqual("Atlanta", BotLogic.getSearchText("USA, Atlanta"));
            Assert.AreEqual("Hamburg", BotLogic.getSearchText("Was im Hamburg?"));
            Assert.AreEqual("Chicago", BotLogic.getSearchText("Now Chicago."));
        }
        [TestMethod]
        public void getWeatherDataTest()
        {
            Assert.AreEqual("something went wrong", BotLogic.getWeatherData(null, null));
            Assert.AreNotEqual("something went wrong", BotLogic.getWeatherData("Warsaw", new WeatherDataSource("CAwJoMmH2A76kfWZGUOq2U4SAQR4t72v")));
        }
    }
}
