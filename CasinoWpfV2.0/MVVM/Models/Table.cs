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


		public TableModel(PlayerModel player, WheelModel wheel)
		{
			this.player = player;

			this.wheel = wheel;

			this.wheel.Completed += PayOut;
		}

		public void MakeBet(TypeOutsideBet typeOutsideBet, decimal amount)
		{
			bet = _table.MakeBet(typeOutsideBet, amount);
		}

		public void MakeBet(TypeInsideBet typeInsideBet, decimal amount, int[] numbers)
		{
			bet = _table.MakeBet(typeInsideBet, amount, numbers);
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

			if (bet.status is RateStatus.Winning)
			{
				player.AddMoney(bet.winningAmount + bet.amount);
			}
			else if (bet.status is RateStatus.Loss)
			{
				player.TakeMoney(bet.amount);
			}

			bet = null;
		}

	}
}
