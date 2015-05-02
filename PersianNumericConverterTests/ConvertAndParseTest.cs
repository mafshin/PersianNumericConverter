using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersianNumericConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersianNumericConverterTests
{
    [TestClass]
    public class ConvertAndParseTest
    {
        [TestMethod]
        public void ConvertAndParse_TestNumberRanges()
        {
            NumericParser parser = new NumericParser();

            string input = null;
            decimal expected = 0;
            decimal actual = 0;

            //0, 1000000 Passed
            //var cases = TestCases.GetConvertedNumbers(0, 1000000);
            var cases = TestCases.GetRandomConvertedNumbers(1000000, 2000000000, 1000000);

            foreach (var item in cases)
            {
                input = item.Key;
                actual = parser.Parse(input);
                expected = item.Value;

                Assert.AreEqual(expected, actual, "Failed to convert {0}", input);
            }
        }
    }
}
