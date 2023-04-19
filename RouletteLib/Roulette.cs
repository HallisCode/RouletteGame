using System;
using System.Linq;
using System.Security.Cryptography;

namespace RouletteLib
{

	/// <summary>
	/// Описывает игровой стол рулетки.
	/// </summary>
	public class Table
	{
		public Roulette roulette { get; private set; } = new Roulette();

		public Rate? rate { get; private set; }

		public decimal minRate { get; private set; }

		public decimal maxRate { get; private set; }


		public Table(decimal minRate, decimal maxRate)
		{
			this.minRate = minRate;

			this.maxRate = maxRate;
		}

		private void CheckAmountRate(decimal amount)
		{
			if (amount < minRate || amount > maxRate)
				throw new Exception("Ставка превышает диапозон принимаемых ставок стола.");
		}

		public void CreateRate(ExternalRate foreignRate, decimal amount)
		{
			CheckAmountRate(amount);

			rate = new RateExternal(foreignRate: foreignRate, amount: amount);
		}

		public void CreateRate(DomesticRate domesticRate, decimal amount)
		{
			CheckAmountRate(amount);

			rate = new RateDomestic(domesticRate: domesticRate, amount: amount);
		}

		/// <summary>
		/// Получает выпавшее число рулетки и обновляет состояние сделанных ставок.
		/// </summary>
		public int Spin()
		{
			if (rate is null) return -1;


			int winningCell = roulette.Spin();

			bool win = false;

			decimal multiplier = 0m;

			switch (rate)
			{
				case (RateExternal):

					ExternalRateHandler((RateExternal)rate, winningCell, out win, out multiplier);
					break;

				case (RateDomestic):

					DomesticRateHandler((RateDomestic)rate, winningCell, out win, out multiplier);
					break;
			}


			decimal winningMoney = 0m;

			if (win)
			{
				winningMoney = rate.amount * multiplier + rate.amount;

				rate.status = RateStatus.Winning;

				rate.winningAmount = winningMoney;
			}
			else
			{
				rate.status = RateStatus.Loss;

				rate.winningAmount = winningMoney;
			}


			rate = null;

			return winningCell;
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


		public int Spin()
		{
			return RandomNumberGenerator.GetInt32(cells.Min(), cells.Max() + 1);
		}
	}
}