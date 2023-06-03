namespace CasinoWpf.Models
{
	public class Player : Model
	{
		public string name { get; private set; }
		public string lastName { get; private set; }

		public decimal _balance;

		public decimal balance
		{
			get { return _balance; }
			internal set
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
	}
}
