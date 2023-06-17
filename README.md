# RouletteGame
![C#11.0](https://img.shields.io/badge/CSharp-11.0-blueviolet) ![.NET-7.0](https://img.shields.io/badge/.NET-7.0-blueviolet)

Standard roulette game of chance.

Useful links :
1. Read more about roulette: https://fan-casino.ru/roulette/pravila-igry.html.
2. Wikipedia : https://en.wikipedia.org/wiki/Roulette.
3. Randomness : https://stackoverflow.com/questions/2706500/how-do-i-generate-a-random-integer-in-c

## Navigation
1. [Overview](#Overview)
2. [Contains](#Contains)
3. [RouletteLib : MVP Exampl](#roulettelib-mvp-exampl)
4. [Requirements](#Requirements)

## <a name ="Overview">Overview</a>
https://github.com/HallisCode/RouletteGame/assets/94699197/5255f76c-f5c1-423e-ba0b-edd7a8a53f26

## <a name ="RouletteLibMVPExample">Contains</a>
1. RouletteLib - the roulette library itself.
2. Casino - graphic implementation of roulette on wpf.

## <a name ="RouletteLibMVPExample">RouletteLib: MVP Exampl</a>

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

## <a name ="Requirements">Requirements</a>
- [*.NET 7.0*](https://dotnet.microsoft.com/en-us/download/dotnet/7.0#:~:text=x86-,.NET%20Desktop%20Runtime%207.0.7,-The%20.NET%20Desktop)
