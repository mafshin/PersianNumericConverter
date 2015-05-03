using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDF.PersianNumericConverter;
using System.Collections.Generic;

namespace PersianNumericConverterTests
{
    [TestClass]
    public class ParseTest
    {
        [TestMethod]
        public void Parse_TestSimpleNumbers()
        {
            NumericParser parser = new NumericParser();

            string input = "صفر";
            decimal expected = 0;

            decimal actual = parser.Parse(input);
            Assert.AreEqual(expected, actual, "Failed to parse {0}", input);

            foreach (var numeral in NumericDefinitions.TextNumerals)
            {
                input = numeral.Name;
                actual = parser.Parse(input);
                expected = numeral.Value;
                Assert.AreEqual(expected, actual, "Failed to parse {0}", input);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Parse_NullStringShouldThrowArgumentOutOfRange()
        {
            NumericParser parser = new NumericParser();

            string input = "";

            parser.Parse(input);
        }

        [TestMethod]
        public void Parse_TestNumberWithScalesConversion()
        {
            NumericParser parser = new NumericParser();

            Dictionary<string, decimal> cases = new Dictionary<string, decimal>();

            string input = null;
            decimal expected = 0;
            decimal actual = 0;

            cases.Add("یک ریال", 1);
            cases.Add("یک تومان", 10);
            cases.Add("یک هزار ریال", 1000);
            cases.Add("یک هزار تومان", 10000);
            cases.Add("یک میلیون ریال", 1000000);
            cases.Add("یک میلیون تومان", 10000000);
            cases.Add("یک میلیارد ریال", 1000000000);
            cases.Add("یک میلیارد تومان", 10000000000);
            
            foreach (var item in cases)
            {
                input = item.Key;
                actual = parser.Parse(input);
                expected = item.Value;
                Assert.AreEqual(expected, actual, "Failed to parse {0}", input);
            }
        }

        [TestMethod]
        public void Parse_CompoundNumbersConversion()
        {
            NumericParser parser = new NumericParser();

            Dictionary<string, decimal> cases = new Dictionary<string, decimal>();

            string input = null;
            decimal expected = 0;
            decimal actual = 0;

            cases.Add("بیست و یک", 21);
            cases.Add("بیست و دو", 22);
            cases.Add("بیست و یک ریال", 21);
            cases.Add("بیست و یک تومان", 210);
            cases.Add("یکصد و یک", 101);
            cases.Add("یکصد و ده", 110);
            cases.Add("یکصد و یازده", 111);
            cases.Add("یکصد و بیست و یک", 121);
            cases.Add("یکصد و بیست و یک تومان", 1210);

            foreach (var item in cases)
            {
                input = item.Key;
                actual = parser.Parse(input);
                expected = item.Value;
                Assert.AreEqual(expected, actual, "Failed to parse {0}", input);
            }
        }

        [TestMethod]
        public void Parse_TestComplexNumbers()
        {
            NumericParser parser = new NumericParser();

            var cases = TestCases.GetComplextNumbers();

            string input = null;
            decimal expected = 0;
            decimal actual = 0;

            foreach (var item in cases)
            {
                input = item.Key;
                actual = parser.Parse(input);
                expected = item.Value;
                Assert.AreEqual(expected, actual, "Failed to parse {0}", input);
            }
        }
    }
}
