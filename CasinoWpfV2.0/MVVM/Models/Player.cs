﻿using System;


namespace CasinoWpfV2._0.MVVM.Models
{
	/// <summary>
	/// Реализует логику игрока.
	/// </summary>
	internal class PlayerModel : Model
	{
		public string name { get; private set; }

		public string lastName { get; private set; }


		private decimal _balance;

		public decimal balance
		{
			get { return _balance; }
			private set
			{
				_balance = value;

				Notify();
			}
		}

		public PlayerModel(string name, string lastName, decimal balance)
		{
			this.name = name;

			this.lastName = lastName;

			this.balance = balance;
		}

		public void AddMoney(decimal money)
		{
			if (money < 0) throw new Exception("Неккоректная сумма денег!");

			balance += money;
		}

		public void TakeMoney(decimal money)
		{
			if (money < 0) throw new Exception("Неккоректная сумма денег!");
			else if (money > balance) throw new Exception("Попытка взять больше денег чем имеется!");

			balance -= money;
		}

		public bool CheckHasMoney(decimal money)
		{
			if (_balance < money) return false;

			return true;
		}
	}
}