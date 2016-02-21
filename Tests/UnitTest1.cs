using AwaitAnything;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TimeConverter_Test()
        {
            var t = TimeConverter.Convert("2m");

            Assert.AreEqual(TimeSpan.FromMinutes(2), t);
        }

        [TestMethod]
        // dont work correctly
        public void DelayTestTime_Should_Pass()
        {
            var sw = new Stopwatch();

            sw.Start();
            Task.Delay(TimeConverter.Convert("2,3m")).Wait();
            sw.Stop();

            Assert.AreEqual(sw.Elapsed.Minutes, 2);
        }

        [TestMethod]
        public Task DelayTestTime_Should_Throw_Exception()
        {
            return Task.Delay(TimeConverter.Convert("5se"));
        }
    }
}