using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SumConsole;

namespace SumExampleForUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int sum;
            sum = Program.Sum(2, 2);
            Assert.AreEqual(4, sum);
        }

        [TestMethod]
        public void TestMethod2()
        {
            int sum;
            sum = Program.Sum(0, 0);
            Assert.AreEqual(0, sum);
        }


    }
}
