using System;
using System.Collections.Generic;
using RouletteLib;

namespace Test
{
	internal class Program
	{
		public static Dictionary<string, ExternalRate> ratesNumber { get; private set; } = new()
		{
			{"1-1", ExternalRate.Red },
			{"1-2", ExternalRate.Black },

			{"2-1", ExternalRate.Even },
			{"2-2", ExternalRate.Odd },

			{"3-1", ExternalRate.SmallFigures },
			{"3-2", ExternalRate.LargeFigures },

			{"4-1", ExternalRate.Dozen1 },
			{"4-2", ExternalRate.Dozen2 },
			{"4-3", ExternalRate.Dozen3 },

			{"5-1", ExternalRate.Column1 },
			{"5-2", ExternalRate.Column2 },
			{"5-3", ExternalRate.Column3 },
		};


		static void Main(string[] args)
		{
			RouletteManager rouletteManager = new RouletteManager();

			RouletteUser user = new RouletteUser("Andrew", "Atamanov", 10000m);
			

			while (true)
			{
				Console.Write("Ваша ставка $");

				decimal rateInput = decimal.Parse(Console.ReadLine());

				Console.Write("""
				Типы ставок :
				1-1. Red 1-2. Black
				2-1. Even 2-2. Odd
				3-1. SmallFigures 3-2. LargeFigures
				4-1. Dozen1 4-2. Dozen2 4-3. Dozen3
				5-1. Column1 5-2. Column2 5-3. Column3
				Ваша ставка : 
				""");

				string typeRate = Console.ReadLine();

				Rate rate = user.CreateRate(ratesNumber[typeRate], rateInput);

				rouletteManager.AcceptTheBet(rate);

				rouletteManager.Spin();

				Console.WriteLine($"\nСтатус ставки : {rate.status}\n" +
					$"Сумма выигрыша : ${rate.winningAmount}\n" +
					$"Ваш баланс : ${user.money}\n");

			}
		}
	}
}