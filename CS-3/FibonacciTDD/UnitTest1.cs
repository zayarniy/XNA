using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//0 1 1 2 3 5
namespace FibonacciTDD
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Dictionary<int, int> cases = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 },{ 2, 1 },{ 3, 2 },{ 4, 5 } };
            foreach (var _case in cases)
                Assert.AreEqual(_case.Value, fib(_case.Key));
            
        }

        int fib(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            return fib(n - 1) + fib(n - 2);
        }
    }
}
