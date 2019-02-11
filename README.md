# Persian Numeric Converter
A library for converting numbers to equivalent Farsi text and vice versa

The library is pretty simple it has a `Parse` method for parsing a persian numeral text into equivalent number and `Convert` method for converting any number less than 10^15 to equivalent text representation. 


## Parse a string to number
```decimal number = parser.Parse("چهل و سه");```


## Convert a number to string
`string text = parser.Convert(43);`

## Test
There's also a Test project in the solution, with different test cases. Since the parsing and converting algorithms are not exactly opposite of each other we can test them against each other. The first 1 million numbers and 1 million random numbers up to 2 billion are tested in this way.

