

namespace RouletteLib
{
	/// <summary>
	/// Описыает пользователя рулетки.
	/// </summary>
	public class RouletteUser
	{
		public string firstName { get; private set; }
		public string lastName { get; private set; }

		public decimal money { get; internal set; }


		public RouletteUser(string firstName, string lastName, decimal money)
		{
			this.firstName = firstName;

			this.lastName = lastName;

			this.money = money;
		}

		public RateExternal CreateRate(ExternalRate foreignRate, decimal rate)
		{
			return new RateExternal(ownerRate: this, foreignRate: foreignRate, rate: rate);
		}

		public RateDomestic CreateRate(DomesticRate domesticRate, decimal rate)
		{
			return new RateDomestic(ownerRate: this, domesticRate: domesticRate, rate: rate);
		}
	}
}
