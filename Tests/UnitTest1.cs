using AwaitAnything;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TimeConverter_Test()
        {
            var t = TimeConverter.Convert("2.35m");
        }

        [TestMethod]
        public void DelayTestTime()
        {
            //await "5s";
            Task.Delay(TimeConverter.Convert("5s")).Wait();
        }
    }
}