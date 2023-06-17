using CasinoWpfV2._0.MVVM.ViewModels;
using CasinoWpfV2._0.XamlTools.Controllers;
using RouletteLib;
using System;


namespace CasinoWpfV2._0.MVVM.Models
{

	/// <summary>
	/// Реализует логику стола с рулеткой, за которым сидит 1 игрок.
	/// </summary>
	public class TableModel : Model
	{
		private Table _table;

		public WheelModel wheel { get; set; }


		public PlayerModel player { get; private set; }

		public Bet? bet { get; private set; }

		public Bet? lastBetPlayed { get; private set; }


		public event EventHandler CompletedSpin
		{
			add { wheel.CompletedSpin += value; }
			remove { wheel.CompletedSpin += value; }
		}

		public static bool CheckValidityAmmountBet(params decimal[] ammountBets)
		{
			foreach(decimal ammount in ammountBets)
			{
				if (ammount <= 0 || ammount > decimal.MaxValue) return false;
			}

			return true;
		}


		public TableModel(PlayerModel player, WheelModel wheel, decimal minAmmountBet = 0m, decimal maxAmmountBet = 0m)
		{
			if (minAmmountBet == 0m)
			{
				minAmmountBet = 1m;
			}

			if (maxAmmountBet == 0)
			{
				maxAmmountBet = decimal.MaxValue;
			}

			if (!CheckValidityAmmountBet(minAmmountBet) || !CheckValidityAmmountBet(maxAmmountBet))
			{
				throw new Exception("Неккоректный диапозон ставок!");
			}


			_table = new Table(minAmmountBet, maxAmmountBet);

			this.player = player;

			this.wheel = wheel;

			this.wheel.CompletedSpin += (obj, e) => PayOut();
		}

		private void CheckAmountBet(decimal amount)
		{
			if (!CheckValidityAmmountBet(amount))
			{
				throw new Exception("Неккоректный диапозон ставок!");
			}

			if (!player.CheckHasMoney(amount))
			{
				throw new Exception("Ставка превышает сумму имеющихся денег!");
			}

			if (!CheckRangeAmount(amount))
			{
				throw new Exception("Ставка выходит за диапозон ставок");
			}
		}

		public bool CheckRangeAmount(decimal amount)
		{
			if (amount < _table.minAmmountBet || amount > _table.maxAmmountBet)
			{
				return false;
			}

			return true;
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

		/// <summary>
		/// Запускает вращение колеса.
		/// </summary>
		public void SpinWheel()
		{
			if (bet is null) throw new Exception("Отсутствует ставка для вращения колеса!");

			int winningCell = _table.Spin();

			wheel.Spin(winningCell);
		}

		/// <summary>
		/// Произодит выплату игроку после вращения колеса.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="Exception">При отсутствии ставки на столе вызывает исключение.</exception>
		private void PayOut()
		{
			if (bet is null) return;

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
