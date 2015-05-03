using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDF.PersianNumericConverter;
using System.Collections.Generic;

namespace PersianNumericConverterTests
{
    [TestClass]
    public class ConversionTest
    {
        [TestMethod]
        public void Convert_TestDefaultNumeralsConversion()
        {
            NumericParser parser = new NumericParser();
            
            decimal input = 0;
            string expected = null;
            string actual = null;

            foreach (var numeral in NumericDefinitions.TextNumerals)
            {
                input = numeral.Value;
                actual = parser.Convert(input);
                expected = numeral.Name;

                Assert.AreEqual(expected, actual, "Failed to convert {0}", input);
            }
        }
        [TestMethod]
        public void Convert_TestSimpleNumbers()
        {
            NumericParser parser = new NumericParser();

            Dictionary<string, decimal> cases = new Dictionary<string, decimal>();

            decimal input = 0;
            string expected = null;
            string actual = null;

            cases.Add("یک ریال", 1);
            cases.Add("ده ریال", 10);
            cases.Add("یکصد ریال", 100);
            cases.Add("یک هزار ریال", 1000);
            cases.Add("ده هزار ریال", 10 * NumericDefinitions.Thousand);
            cases.Add("یکصد هزار ریال", 100 * NumericDefinitions.Thousand);
            cases.Add("یک میلیون ریال", 1 * NumericDefinitions.Million);
            cases.Add("ده میلیون ریال", 10 * NumericDefinitions.Million);
            cases.Add("یکصد میلیون ریال", 100 * NumericDefinitions.Million);
            cases.Add("یک میلیارد ریال", 1 * NumericDefinitions.Milliard);
            cases.Add("ده میلیارد ریال", 10 * NumericDefinitions.Milliard);
            cases.Add("یکصد میلیارد ریال", 100 * NumericDefinitions.Milliard);

            foreach (var item in cases)
            {
                input = item.Value;
                actual = parser.Convert(input, NumericDefinitions.Rial);
                expected = item.Key;

                Assert.AreEqual(expected, actual, "Failed to convert {0}", input);
            }

            cases.Clear();

            cases.Add("یک تومان", 10);
            cases.Add("ده تومان", 100);
            cases.Add("یکصد تومان", 1000);
            cases.Add("یک هزار تومان", 10 * 1 * NumericDefinitions.Thousand);
            cases.Add("ده هزار تومان", 10 * 10 * NumericDefinitions.Thousand);
            cases.Add("یکصد هزار تومان", 10 * 100 * NumericDefinitions.Thousand);
            cases.Add("یک میلیون تومان", 10 * 1 * NumericDefinitions.Million);
            cases.Add("ده میلیون تومان", 10 * 10 * NumericDefinitions.Million);
            cases.Add("یکصد میلیون تومان", 10 * 100 * NumericDefinitions.Million);
            cases.Add("یک میلیارد تومان", 10 * 1 * NumericDefinitions.Milliard);
            cases.Add("ده میلیارد تومان", 10 * 10 * NumericDefinitions.Milliard);
            cases.Add("یکصد میلیارد تومان", 10 * 100 * NumericDefinitions.Milliard);

            foreach (var item in cases)
            {
                input = item.Value / 10;
                actual = parser.Convert(input, NumericDefinitions.Toman);
                expected = item.Key;

                Assert.AreEqual(expected, actual, "Failed to convert {0}", input);
            }
        }

        [TestMethod]
        public void Convert_TestComplexNumbers()
        {
            NumericParser parser = new NumericParser();

            var cases = TestCases.GetComplextNumbers();

            decimal input = 0;
            string expected = null;
            string actual = null;
            foreach (var item in cases)
            {
                input = item.Value;
                actual = parser.Convert(input);
                expected = item.Key;

                Assert.AreEqual(expected, actual, "Failed to convert {0}", input);
            }
        }
    }
}
