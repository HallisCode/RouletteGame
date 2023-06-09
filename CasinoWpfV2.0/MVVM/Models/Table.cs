using CasinoWpfV2._0.MVVM.ViewModels;
using CasinoWpfV2._0.XamlTools.Controllers;
using RouletteLib;
using System;


namespace CasinoWpfV2._0.MVVM.Models
{

	/// <summary>
	/// Реализует логику стола с рулеткой, за которым сидит 1 игрок.
	/// </summary>
	internal class TableModel : Model
	{
		private Table _table = new Table();

		private WheelModel wheel { get; set; }


		public PlayerModel player { get; private set; }

		public Bet? bet { get; private set; }

		public Bet? lastBetPlayed { get; private set; }


		public event EventHandler CompletedSpin
		{
			add { wheel.CompletedSpin += value; }
			remove { wheel.CompletedSpin += value; }
		}


		public TableModel(PlayerModel player, WheelModel wheel)
		{
			this.player = player;

			this.wheel = wheel;

			this.wheel.CompletedSpin += PayOut;
		}

		private void CheckAmountBet(decimal amount)
		{
			if (!player.CheckHasMoney(amount))
			{
				throw new Exception("Ставка превышает сумму имеющихся денег!");
			}

			if (amount <= 0)
			{
				throw new Exception("Ставка неккоректная!");
			}
		}

		public void MakeBet(TypeOutsideBet typeOutsideBet, decimal amount)
		{
			CheckAmountBet(amount);

			bet = _table.MakeBet(typeOutsideBet, amount);
		}

		public void MakeBet(TypeInsideBet typeInsideBet, decimal amount, int[] numbers)
		{
			CheckAmountBet(amount);

			bet = _table.MakeBet(typeInsideBet, amount, numbers);
		}

		public void MakeLastBet(decimal amount = 0)
		{
			if (lastBetPlayed is null) throw new Exception("Отсутствует предыдущая ставка!");

			amount = amount <= 0 ? lastBetPlayed.amount : amount;

			switch (lastBetPlayed)
			{
				case (OutsideBet):

					OutsideBet outsideBet = (OutsideBet)lastBetPlayed;

					bet = _table.MakeBet(outsideBet.type, amount);

					break;

				case (InsideBet):

					InsideBet insideBet = (InsideBet)lastBetPlayed;

					bet = _table.MakeBet(insideBet.type, amount, insideBet.numbers);

					break;
			}
		}

		/// <summary>
		/// Запускает вращение колеса.
		/// </summary>
		public void SpinWheel()
		{
			int winningCell = _table.Spin();

			wheel.Spin(winningCell);
		}

		/// <summary>
		/// Представляет делегат EventHandler. Произодит выплату игроку после вращения колеса.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="Exception">При отсутствии ставки на столе вызывает исключение.</exception>
		private void PayOut(object? sender, EventArgs e)
		{
			if (bet is null) throw new Exception("Невозможно произвести выплату при отсутствии ставки!");

			if (bet.status is BetStatus.Winning)
			{
				player.AddMoney(bet.winningAmount + bet.amount);
			}
			else if (bet.status is BetStatus.Loss)
			{
				player.TakeMoney(bet.amount);
			}

			lastBetPlayed = bet;

			bet = null;
		}
	}
}
