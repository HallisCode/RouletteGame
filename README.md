# RouletteGame
![C#11.0](https://img.shields.io/badge/CSharp-11.0-blueviolet) ![.NET-7.0](https://img.shields.io/badge/.NET-7.0-blueviolet)

Standard roulette game of chance.

Useful links :
1. Read more about roulette: https://fan-casino.ru/roulette/pravila-igry.html.
2. Wikipedia : https://en.wikipedia.org/wiki/Roulette.
3. Randomness : https://stackoverflow.com/questions/2706500/how-do-i-generate-a-random-integer-in-c

## Navigation
1. <a name="Contains">Contains</a> 
2. <a name="RouletteLibMVPExampl">RouletteLib : MVP Exampl</a> 
3. <a name="Requirements">Requirements</a> 

## <a name="Contains">Contains</a> 
1. RouletteLib - the roulette library itself.
2. Casino - graphic implementation of roulette on wpf.

## RouletteLib : MVP Example (#RouletteLib : MVP Example)

```
// Connecting the library
using RoulutteLib

// Create a gambling table with a range of accepted bets
Table table = new Table(10000m, 100000m);

// When you create a bet, it is accepted by the gambling table by default
Rate rate = table.MakeBet(TypeExternalRate.Red, 1000);

// Spinning roulette on the gambling table
int winningCell = table.Spin();

// After spinning the roulette wheel the status of bets is updated
Console.WriteLine(rate.status + " " + rate.winningAmount);
```

## Requirements (#Requirements)
- [*.NET 7.0*](https://dotnet.microsoft.com/en-us/download/dotnet/7.0#:~:text=x86-,.NET%20Desktop%20Runtime%207.0.5,-The%20.NET%20Desktop)
