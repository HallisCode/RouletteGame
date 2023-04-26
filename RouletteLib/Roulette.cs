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

		public Bet? rate { get; private set; }

		public decimal minRate { get; private set; }

		public decimal maxRate { get; private set; }


		public Table(decimal minRate = decimal.MinValue, decimal maxRate = decimal.MaxValue)
		{
			this.minRate = minRate;

			this.maxRate = maxRate;
		}

		private void CheckAmountRate(decimal amount)
		{
			if (amount < minRate || amount > maxRate)
				throw new Exception("Ставка выходит за диапозон принимаемых ставок столом.");
		}

		public OutsideBet Bet(TypeOutsideBet typeOutsideBet, decimal amount)
		{
			CheckAmountRate(amount);

			rate = new OutsideBet(typeOutsideBet: typeOutsideBet, amount: amount);

			return (OutsideBet)rate;
		}

		public InsideBet Bet(TypeInsideBet typeInsideBet, decimal amount)
		{
			CheckAmountRate(amount);

			rate = new InsideBet(typeInsideBet: typeInsideBet, amount: amount);

			return (InsideBet)rate;
		}

		/// <summary>
		/// Получает выпавшее число рулетки и обновляет состояние сделанных ставок.
		/// </summary>
		public int Spin()
		{
			if (rate is null) throw new Exception("Отсутствует ставка.");

			int winningCell = roulette.Spin();

			bool win = false;

			decimal multiplier = 0m;

			switch (rate)
			{
				case (OutsideBet):

					OutsideBetHandler((OutsideBet)rate, winningCell, out win, out multiplier);
					break;

				case (InsideBet):

					InsideBetHandler((InsideBet)rate, winningCell, out win, out multiplier);
					break;
			}


			decimal winningMoney = 0m;

			if (win)
			{
				winningMoney = rate.amount * multiplier;

				rate.status = RateStatus.Winning;

				rate.winningAmount = winningMoney;
			}
			else
			{
				rate.status = RateStatus.Loss;

				rate.winningAmount = winningMoney;
			}

			lastRate = rate;

			rate = null;

			return winningCell;
		}

		private void OutsideBetHandler(OutsideBet rateExternal, int winningCell, out bool win, out decimal multiplier)
		{
			switch (rateExternal.type)
			{
				case (TypeOutsideBet.Red):

					win = Cells.redCells.Contains(winningCell);

					multiplier = 1m;

					break;

				case (TypeOutsideBet.Black):

					win = Cells.blackCells.Contains(winningCell);

					multiplier = 1m;

					break;

				case (TypeOutsideBet.Even):

					win = winningCell % 2 == 0;

					multiplier = 1m;

					break;

				case (TypeOutsideBet.Odd):

					win = winningCell % 2 != 0;

					multiplier = 1m;

					break;

				case (TypeOutsideBet.SmallFigures):

					win = winningCell > 0 && winningCell <= 18;

					multiplier = 1m;

					break;

				case (TypeOutsideBet.LargeFigures):

					win = winningCell > 18 && winningCell <= 36;

					multiplier = 1m;

					break;

				case (TypeOutsideBet.Dozen1):

					win = Cells.dozen1.Contains(winningCell);

					multiplier = 2m;

					break;

				case (TypeOutsideBet.Dozen2):

					win = Cells.dozen2.Contains(winningCell);

					multiplier = 2m;

					break;

				case (TypeOutsideBet.Dozen3):

					win = Cells.dozen3.Contains(winningCell);

					multiplier = 2m;

					break;

				case (TypeOutsideBet.Column1):

					win = Cells.column1.Contains(winningCell);

					multiplier = 2m;

					break;

				case (TypeOutsideBet.Column2):

					win = Cells.column2.Contains(winningCell);

					multiplier = 2m;

					break;

				case (TypeOutsideBet.Column3):

					win = Cells.column3.Contains(winningCell);

					multiplier = 2m;

					break;

				default:

					win = false;

					multiplier = 0m;

					break;
			}
		}

		private void InsideBetHandler(InsideBet rateDomestic, int winningCell, out bool win, out decimal multiplier)
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