using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersianNumericConverter
{

    public class NumericDefinitions
    {
        public static List<NumberScale> Scales = new List<NumberScale>();
        public static Currency Rial;
        public static Currency Toman;
        public static List<TextNumeral> TextNumerals = new List<TextNumeral>();


        public const decimal Billion = 1000000000000;
        public const decimal Milliard = 1000000000;
        public const decimal Million = 1000000;
        public const decimal Thousand = 1000;

        public struct NumberScale
        {
            public string Name;
            public decimal Value;
            public NumberScale(string name, decimal value)
            {
                Name = name;
                Value = value;
            }
        }

        public struct Currency
        {
            public List<string> Names;
            public decimal Magitude;

            public Currency(decimal magnitude, params string[] names)
            {
                this.Magitude = magnitude;
                this.Names = new List<string>(names);
            }
        }

        public struct TextNumeral
        {
            public string Name;
            public decimal Value;

            public TextNumeral(string name, decimal value)
            {
                this.Name = name;
                this.Value = value;
            }
        }

        static NumericDefinitions()
        {
            #region Scales
            Scales.Add(new NumberScale("هزار", 1000));
            Scales.Add(new NumberScale("میلیون", 1000000));
            Scales.Add(new NumberScale("میلیارد", 1000000000));
            Scales.Add(new NumberScale("هزار میلیارد", 1000000000000));
            #endregion

            #region Currencies
            Rial = new Currency(1, "ریال");
            Toman = new Currency(10, "تومان", "تومن");
            #endregion

            #region TextNumerals
            TextNumerals.AddRange(
                new TextNumeral[]
                {
                    new TextNumeral("صفر",0),
                    new TextNumeral("یک", 1),
                    new TextNumeral("دو", 2),
                    new TextNumeral("سه", 3),
                    new TextNumeral("چهار", 4),
                    new TextNumeral("پنج", 5),
                    new TextNumeral("شش", 6),
                    new TextNumeral("هفت", 7),
                    new TextNumeral("هشت", 8),
                    new TextNumeral("نه", 9),

                    new TextNumeral("یازده", 11),
                    new TextNumeral("دوازده", 12),
                    new TextNumeral("سیزده", 13),
                    new TextNumeral("چهارده", 14),
                    new TextNumeral("پانزده", 15),
                    new TextNumeral("شانزده", 16),
                    new TextNumeral("هفده", 17),
                    new TextNumeral("هجده", 18),
                    new TextNumeral("نوزده", 19),

                    new TextNumeral("ده", 10),
                    new TextNumeral("بیست", 20),
                    new TextNumeral("سی", 30),
                    new TextNumeral("چهل", 40),
                    new TextNumeral("پنجاه", 50),
                    new TextNumeral("شصت", 60),
                    new TextNumeral("هفتاد", 70),
                    new TextNumeral("هشتاد", 80),
                    new TextNumeral("نود", 90),

                    new TextNumeral("یکصد", 100),
                    new TextNumeral("دویست", 200),
                    new TextNumeral("سیصد", 300),
                    new TextNumeral("چهارصد", 400),
                    new TextNumeral("پانصد", 500),
                    new TextNumeral("ششصد", 600),
                    new TextNumeral("هفتصد", 700),
                    new TextNumeral("هشتصد", 800),
                    new TextNumeral("نهصد", 900)
                });
            #endregion
        }
    }
    public class NumericParser
    {
        public NumericDefinitions.Currency DefaultCurrency { get; set; }
        public NumericParser()
        {
            DefaultCurrency = NumericDefinitions.Rial;
        }
        public decimal Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentOutOfRangeException();

            decimal result = 0;

            // if it's a number
            if (decimal.TryParse(input, out result))
                return result;

            //input = input.Trim(" و".ToCharArray());

            var match = Regex.Match(input, @"^(\d+) و (\d+)$");
            if (match.Success && match.Groups.Count == 3)
            {
                decimal millionsNumber;
                if (!decimal.TryParse(match.Groups[1].Value, out millionsNumber))
                    return 0;

                decimal thousandsNumber;
                if (!decimal.TryParse(match.Groups[2].Value, out thousandsNumber))
                    return 0;

                result = millionsNumber * 1000000 + thousandsNumber * 1000;

                // Assuming Toman as defaults
                result *= 10;

                return result;
            }

            NumericDefinitions.Currency currency;
            if (NumericDefinitions.Rial.Names.Any(x => input.Contains(x)))
            {
                currency = NumericDefinitions.Rial;
            }
            else if (NumericDefinitions.Toman.Names.Any(x => input.Contains(x)))
            {
                currency = NumericDefinitions.Toman;
            }
            else
            {
                currency = DefaultCurrency;
            }

            //Clear currency values from input string
            currency.Names.ForEach(x => input = input.Replace(x, " "));

            result = ParsePure(input);

            result *= currency.Magitude;

            return result;
        }

        private decimal ParsePure(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            decimal result = 0;

            foreach (var scale in NumericDefinitions.Scales.OrderByDescending(x => x.Value))
            {
                var innerParts = input.Split(new string[] { scale.Name }, StringSplitOptions.RemoveEmptyEntries);
                if (innerParts.Length > 0)
                {
                    if (input.Contains(scale.Name) && !string.IsNullOrWhiteSpace(innerParts[0]))
                    {
                        result += ParsePure(innerParts[0]) * scale.Value;
                        input = input.Substring(innerParts[0].Length);
                        var index = input.IndexOf(scale.Name);
                        input = input.Remove(index, scale.Name.Length);
                    }

                    if (innerParts.Length > 1)
                    {
                        result += ParsePure(innerParts[1]);
                        input = input.Replace(innerParts[1], " ");
                    }
                }
            }


            string[] parts = input.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == ",")
                    continue;

                var index = NumericDefinitions.TextNumerals.FindIndex(x => x.Name == parts[i]);

                if (index != -1)
                    result += NumericDefinitions.TextNumerals[index].Value;
            }

            return result;
        }

        public string Convert(decimal input, bool addCurrencySpecifier = false)
        {
            var text = Convert(input);

            if (addCurrencySpecifier)
                text = string.Format("{0} {1}", text,
                    DefaultCurrency.Names.First());

            return text;
        }
        public string Convert(decimal input, NumericDefinitions.Currency currency)
        {
            DefaultCurrency = currency;
            return Convert(input, true);
        }
        private string Convert(decimal input)
        {
            var result = GetNumeralAndScaleText(input);

            if (!string.IsNullOrEmpty(result))
                return result;

            foreach (var scale in NumericDefinitions.Scales.OrderByDescending(x => x.Value))
            {
                if (input == scale.Value)
                {
                    return string.Format("{0} {1}", "یک", scale.Name);
                }
                else if (input > scale.Value)
                {
                    decimal rem = Decimal.Remainder(input, scale.Value);
                    decimal quotient = Decimal.Floor(input / scale.Value);
                    string quotientValue = Convert(quotient);

                    return string.Format("{0} {1}", quotientValue, scale.Name) +
                        (rem > 0.0M ? string.Format(" و {0}", Convert(rem)) : string.Empty);
                }
            }

            foreach (var textNumeral in NumericDefinitions.TextNumerals.OrderByDescending(x => x.Value))
            {
                if (input == textNumeral.Value)
                {
                    return textNumeral.Name;
                }
                else if (input > textNumeral.Value)
                {
                    decimal rem = Decimal.Remainder(input, textNumeral.Value);
                    decimal quotient = Decimal.Round(input / textNumeral.Value);
                    string quotientValue = Convert(quotient);

                    return textNumeral.Name +
                        (rem > 0.0M ? string.Format(" و {0}", Convert(rem)) : string.Empty);
                }
            }

            return GetNumeralAndScaleText(input);
        }

        private string GetScaleText(decimal input)
        {
            var scale =
                NumericDefinitions.Scales
                .FirstOrDefault(x => x.Value == input);

            if (!scale.Equals(default(NumericDefinitions.NumberScale)))
                return scale.Name;

            return string.Empty;
        }
        private string GetNumeralAndScaleText(decimal input)
        {
            var scale =
                NumericDefinitions.Scales
                .FirstOrDefault(x => x.Value == input);

            if (!scale.Equals(default(NumericDefinitions.NumberScale)))
                return String.Format("یک {0}", scale.Name);

            var numeral =
                NumericDefinitions.TextNumerals
                .FirstOrDefault(x => x.Value == input);

            if (!numeral.Equals(default(NumericDefinitions.TextNumeral)))
                return numeral.Name;

            return String.Empty;
        }
        private string GetNumeralText(decimal input)
        {
            var numeral =
                NumericDefinitions.TextNumerals
                .FirstOrDefault(x => x.Value == input);

            if (!numeral.Equals(default(NumericDefinitions.TextNumeral)))
                return numeral.Name;

            return String.Empty;
        }
    }
}
