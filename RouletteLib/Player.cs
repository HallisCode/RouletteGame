

namespace RouletteLib
{
	/// <summary>
	/// Описыает пользователя рулетки.
	/// </summary>
	public class Player
	{
		public string firstName { get; private set; }
		public string lastName { get; private set; }

		public decimal money { get; internal set; }


		public Player(string firstName, string lastName, decimal money)
		{
			this.firstName = firstName;

			this.lastName = lastName;

			this.money = money;
		}
	}
}
