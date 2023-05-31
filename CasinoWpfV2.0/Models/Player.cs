using System;


namespace CasinoWpfV2._0.Models
{
	internal class Player : Model
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

		public Player(string name, string lastName, decimal balance)
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
	}
}