# RouletteGame
![C#11.0](https://img.shields.io/badge/CSharp-11.0-blueviolet) ![.NET-7.0](https://img.shields.io/badge/.NET-7.0-blueviolet)

Standard roulette game of chance.

Useful links :
1. Read more about roulette: https://fan-casino.ru/roulette/pravila-igry.html.
2. Wikipedia : https://en.wikipedia.org/wiki/Roulette.
3. Randomness : https://stackoverflow.com/questions/2706500/how-do-i-generate-a-random-integer-in-c

## Contains
1. RouletteLib - the roulette library itself.
2. Casino - graphic implementation of roulette on wpf.
3. ConsoleCasino - a small roulette with reduced functionality on the console.

## RouletteLib : MVP Example

```
using RoulutteLib

Table table = new Table();

Rate rate = table.Bet(TypeExternalRate.Red, 1000);


table.Spin();


Console.WriteLine(rate.status + " " + rate.winningAmount);
```

## Requirements
- *.NET 7.0*
