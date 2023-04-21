using System;


namespace RouletteLib
{
	public enum RateStatus
	{
		Waiting,
		Winning,
		Loss
	}

	public enum TypeDomesticRate
	{
		StraightUp,
		Split,
		Street,
		SixLine,
		Corner,
		FirstFour
	}

	public enum TypeExternalRate
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
		public decimal amount { get; protected set; }

		public RateStatus status { get; internal set; } = RateStatus.Waiting;

		public decimal winningAmount { get; internal set; }


		protected Rate(decimal amount)
		{
			this.amount = amount;
		}
	}

	/// <summary>
	/// Описывает внешнюю ставку в рулетке.
	/// </summary>
	public class RateExternal : Rate
	{
		public TypeExternalRate type { get; private set; }


		internal RateExternal(TypeExternalRate typeExternalRate, decimal amount)
			: base(amount)
		{
			this.type = typeExternalRate;
		}
	}

	/// <summary>
	/// Описывает внутреннюю ставку в рулетке.
	/// </summary>
	public class RateDomestic : Rate
	{
		public TypeDomesticRate type { get; private set; }


		internal RateDomestic(TypeDomesticRate typeDomesticRate, decimal amount)
			: base(amount)
		{
			this.type = typeDomesticRate;
		}
	}
}
