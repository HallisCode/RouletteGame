using System;


namespace RouletteLib
{
	public enum RateStatus
	{
		Waiting,
		Winning,
		Loss
	}

	public enum DomesticRate
	{
		StraightUp,
		Split,
		Street,
		SixLine,
		Corner,
		FirstFour
	}

	public enum ExternalRate
	{
		Red, Black,
		Even, Odd,
		SmallFigures, LargeFigures,
		Dozen1, Dozen2, Dozen3,
		Column1, Column2, Column3,
	}


	/// <summary>
	/// Описывает абстрактную ставку в рулетке.
	/// </summary>
	public abstract class Rate
	{
		public Player owner { get; protected set; }

		public decimal rate { get; protected set; }

		public RateStatus status { get; internal set; } = RateStatus.Waiting;

		public decimal winningAmount { get; internal set; }


		protected Rate(Player owner, decimal money)
		{
			if (owner is null) throw new Exception("Пользователь не может быть null.");

			if (money <= 0) throw new Exception("Неккоректная сумма ставки");

			if (owner.money < money) throw new Exception("Недостаточно средств у пользователя.");

			this.owner = owner;

			this.owner.money -= money;

			this.rate = money;
		}
	}

	/// <summary>
	/// Описывает внешнюю ставку в рулетке.
	/// </summary>
	public class RateExternal : Rate
	{
		public ExternalRate type { get; private set; }

		internal RateExternal(Player owner, ExternalRate foreignRate, decimal money)
			: base(owner, money)
		{
			this.type = foreignRate;
		}
	}

	/// <summary>
	/// Описывает внутреннюю ставку в рулетке.
	/// </summary>
	public class RateDomestic : Rate
	{
		public DomesticRate type { get; private set; }


		internal RateDomestic(Player owner, DomesticRate domesticRate, decimal money)
			: base(owner, money)
		{
			this.type = domesticRate;
		}
	}
}
