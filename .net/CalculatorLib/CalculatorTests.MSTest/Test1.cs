using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLib;
using System;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator calculator;

        [TestInitialize]
        public void Setup()
        {
            calculator = new Calculator();
        }

        [TestMethod]
        public void TestAddition()
        {
            Assert.AreEqual(8, calculator.Add(3, 5));
        }

        [TestMethod]
        public void TestSubtraction()
        {
            Assert.AreEqual(2, calculator.Subtract(5, 3));
        }

        [TestMethod]
        public void TestMultiplication()
        {
            Assert.AreEqual(15, calculator.Multiply(3, 5));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivisionByZero()
        {
            calculator.Divide(5, 0);
        }

        [TestMethod]
        public void TestDivision()
        {
            Assert.AreEqual(2, calculator.Divide(10, 5));
        }

        [TestMethod]
        public void TestAddZero()
        {
            Assert.AreEqual(7, calculator.Add(7, 0));
        }

        [TestMethod]
        public void TestSubtractZero()
        {
            Assert.AreEqual(7, calculator.Subtract(7, 0));
        }
    }
}
