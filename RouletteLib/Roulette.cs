using System;
using System.Linq;


namespace RouletteLib
{

	/// <summary>
	/// Описывает менеджер рулетки, который управляет всеми ее процессами.
	/// </summary>
	public class Table
	{
		public Roulette roulette { get; private set; } = new Roulette();

		public Rate? rate { get; private set; }


		public void CreateRate(Player owner, ExternalRate foreignRate, decimal money)
		{
			rate = new RateExternal(owner: owner, foreignRate: foreignRate, money: money);
		}

		public void CreateRate(Player owner, DomesticRate domesticRate, decimal money)
		{
			rate = new RateDomestic(owner: owner, domesticRate: domesticRate, money: money);
		}

		/// <summary>
		/// Запускает прокрутку рулетки с последующими выплатами победителям.
		/// </summary>
		public void Spin()
		{
			if (rate is null) return;


			int winningCell = roulette.Spin();

			bool win;

			decimal multiplier;

			switch (rate)
			{
				case (RateExternal):

					ExternalRateHandler((RateExternal)rate, winningCell, out win, out multiplier);
					break;

				case (RateDomestic):

					DomesticRateHandler((RateDomestic)rate, winningCell, out win, out multiplier);
					break;

				default:

					win = false;

					multiplier = 0m;

					break;
			}

			decimal winningMoney = 0m;

			if (win)
			{
				winningMoney = rate.rate * multiplier + rate.rate;

				rate.status = RateStatus.Winning;

				rate.winningAmount = winningMoney;

				rate.owner.money += winningMoney;
			}
			else
			{
				rate.status = RateStatus.Loss;

				rate.winningAmount = winningMoney;
			}

			rate = null;
		}

		protected void ExternalRateHandler(RateExternal rateExternal, int winningCell, out bool win, out decimal multiplier)
		{
			switch (rateExternal.type)
			{
				case (ExternalRate.Red):

					win = Cells.redCells.Contains(winningCell);

					multiplier = 1m;

					break;

				case (ExternalRate.Black):

					win = Cells.blackCells.Contains(winningCell);

					multiplier = 1m;

					break;

				case (ExternalRate.Even):

					win = winningCell % 2 == 0;

					multiplier = 1m;

					break;

				case (ExternalRate.Odd):

					win = winningCell % 2 != 0;

					multiplier = 1m;

					break;

				case (ExternalRate.SmallFigures):

					win = winningCell > 0 && winningCell <= 18;

					multiplier = 1m;

					break;

				case (ExternalRate.LargeFigures):

					win = winningCell > 18 && winningCell <= 36;

					multiplier = 1m;

					break;

				case (ExternalRate.Dozen1):

					win = Cells.dozen1.Contains(winningCell);

					multiplier = 2m;

					break;

				case (ExternalRate.Dozen2):

					win = Cells.dozen2.Contains(winningCell);

					multiplier = 2m;

					break;

				case (ExternalRate.Dozen3):

					win = Cells.dozen3.Contains(winningCell);

					multiplier = 2m;

					break;

				case (ExternalRate.Column1):

					win = Cells.column1.Contains(winningCell);

					multiplier = 2m;

					break;

				case (ExternalRate.Column2):

					win = Cells.column2.Contains(winningCell);

					multiplier = 2m;

					break;

				case (ExternalRate.Column3):

					win = Cells.column3.Contains(winningCell);

					multiplier = 2m;

					break;

				default:

					win = false;

					multiplier = 0m;

					break;
			}
		}

		protected void DomesticRateHandler(RateDomestic rateDomestic, int winningCell, out bool win, out decimal multiplier)
		{
			win = false;

			multiplier = 0m;
		}
	}

	/// <summary>
	/// Описывает логику поведения самой рулетки.
	/// </summary>
	public class Roulette
	{
		public int[] cells { get; private set; } = Cells.allCells;

		private Random _random = new Random();


		public int Spin()
		{
			return _random.Next(cells.Min(), cells.Max() + 1);
		}
	}
}