using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void TestDatabase()
        {
            string[] sequel = IMDbWebApp.Database.GetSequel(1);
            Assert.AreEqual(2, sequel[0]);
        }
    }
}
