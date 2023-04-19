# RouletteGame
![C#11.0](https://img.shields.io/badge/CSharp-11.0-blueviolet) ![.NET-7.0](https://img.shields.io/badge/.NET-7.0-blueviolet)

Standard roulette game of chance.

Useful links :
1. Read more about roulette: https://fan-casino.ru/roulette/pravila-igry.html.
2. Wikipedia : https://en.wikipedia.org/wiki/Roulette.
3. Random : https://stackoverflow.com/questions/2706500/how-do-i-generate-a-random-integer-in-c

## Contains
1. RouletteLib - the roulette library itself.
2. Casino - graphic implementation of roulette on wpf.
3. ConsoleCasino - a small roulette with reduced functionality on the console.

## RouletteLib : MVP Example

```
#using RoulutteLib

RouletteManager rouletteManager = new RouletteManager();

RouletteUser user = new RouletteUser("Name", "LastName", 10000);


Rate rate = user.CreateRate(ExternalRate.Red, 1000);

rouletteManager.AcceptBet(rate);


rouletteManager.Spin();


Console.WriteLine(rate.status + " " + rate.winningAmount + " " + user.money);
```

## Requirements
- *.NET 7.0*
