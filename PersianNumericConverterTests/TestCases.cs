using RDF.PersianNumericConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersianNumericConverterTests
{
    public class TestCases
    {
        public static Dictionary<string, decimal> GetComplextNumbers()
        {
            Dictionary<string, decimal> cases = new Dictionary<string, decimal>();

            cases.Add("بیست هزار و بیست و یک", 20021);
            cases.Add("یکصد و بیست", 120);
            cases.Add("یکصد و بیست و سه", 123);
            cases.Add("یکصد هزار و بیست", 100020);
            cases.Add("یکصد و بیست و سه هزار و بیست", 123020);
            cases.Add("یک میلیون و سیصد هزار", 1300000);
            cases.Add("یک میلیون و چهارصد و نود و سه", 1000493);
            cases.Add("یک میلیون و دویست و پنجاه و دو هزار و هفتصد و شصت و نه", 1252769);
            cases.Add("یک میلیارد و بیست", 1000000020);
            cases.Add("یک میلیارد و بیست هزار", 1000020000);
            cases.Add("یک میلیارد و پانصد میلیون و سیصد و چهل و چهار هزار و هفتصد و نود و یک", 1500344791);
            cases.Add("یک هزار و سه", 1003);
            cases.Add("نود", 90);
            cases.Add("یک میلیون و چهل و دو", 1000042);
            cases.Add("سیصد و بیست میلیون و چهارصد هزار و سه", 320400003);
            cases.Add("هفتصد و دوازده هزار و سی و دو", 712032);
            cases.Add("پانصد هزار و سیصد و چهار", 500304);
            cases.Add("یک میلیون و دویست و سی و دو هزار", 1232000);
            cases.Add("یک میلیون و دویست و سی و دو", 1000232);
            cases.Add("یک میلیون و دویست هزار", 1200000);
            cases.Add("یک میلیون و سیصد", 1000300);
            cases.Add("یک میلیارد و بیست و دو میلیون و شصت و سه هزار و سیصد و چهل و دو", 1022063342);
            cases.Add("سه هزار میلیارد", 3000000000000);
            cases.Add("هشتصد و شصت و هفت میلیارد و نهصد و یک میلیون", 867901000000);
            cases.Add("یک میلیارد و دویست و سی و چهار میلیون و پانصد و شصت و هفت هزار و هشتصد و نود", 1234567890);
            cases.Add("دویست هزار میلیارد", 200000000000000);

            return cases;
        }

        public static Dictionary<string, decimal> GetConvertedNumbers(decimal start, decimal end)
        {
            NumericParser parser = new NumericParser();
            Dictionary<string, decimal> cases = new Dictionary<string, decimal>();

            for (decimal i = start; i <= end; i++)
            {
                cases.Add(parser.Convert(i), i);
            }

            return cases;
        }

        public static Dictionary<string, decimal> GetRandomConvertedNumbers(int start, int end, int randomCount)
        {
            NumericParser parser = new NumericParser();
            Dictionary<string, decimal> cases = new Dictionary<string, decimal>();

            decimal value;
            Random random = new Random();
            for (int i = 0; i < randomCount; i++)
            {
                value = random.Next(start, end);
                string text = parser.Convert(value);
                if (!cases.ContainsKey(text))
                    cases.Add(text, value);
            }

            return cases;
        }
    }
}
