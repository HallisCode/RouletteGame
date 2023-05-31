using RouletteLib;
using System;


namespace CasinoWpfV2._0.Models
{

	/// <summary>
	/// Реализует логику рулетки с 37 ячейками.
	/// </summary>
	internal class Table : Model
	{
		private RouletteLib.Table _table = new RouletteLib.Table();

		private Bet? bet;


		public Wheel wheel { get; private set; }

		public Player player { get; private set; }


		private decimal _betAmmount;

		public decimal betAmmount
		{
			get { return _betAmmount; }
			set
			{
				_betAmmount = value;

				Notify();
			}
		}


		public Table(Player player, Wheel wheel)
		{
			this.player = player;

			this.wheel = wheel;
			this.wheel.CompletedRotate += PayOut;
		}


		public void MakeBet(TypeOutsideBet typeOutsideBet, decimal amount)
		{
			_table.MakeBet(typeOutsideBet, amount);
		}

		public void MakeBet(TypeInsideBet typeInsideBet, decimal amount, int[] numbers)
		{
			_table.MakeBet(typeInsideBet, amount, numbers);
		}


		public void SpinWheel()
		{
			int winningCell = _table.Spin();

			wheel.Spin(winningCell);
		}

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
		}

	}
}
