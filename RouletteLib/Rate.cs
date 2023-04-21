using System;


namespace RouletteLib
{
	public enum RateStatus
	{
		Waiting,
		Winning,
		Loss
	}

	public enum TypeInsideBet
	{
		StraightUp,
		Split,
		Street,
		SixLine,
		Corner,
		FirstFour
	}

	public enum TypeOutsideBet
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
	public abstract class Bet
	{
		public decimal amount { get; protected set; }

		public RateStatus status { get; internal set; } = RateStatus.Waiting;

		public decimal winningAmount { get; internal set; }


		protected Bet(decimal amount)
		{
			this.amount = amount;
		}
	}

	/// <summary>
	/// Описывает внешнюю ставку в рулетке.
	/// </summary>
	public class OutsideBet : Bet
	{
		public TypeOutsideBet type { get; private set; }


		internal OutsideBet(TypeOutsideBet typeOutsideBet, decimal amount)
			: base(amount)
		{
			this.type = typeOutsideBet;
		}
	}

	/// <summary>
	/// Описывает внутреннюю ставку в рулетке.
	/// </summary>
	public class InsideBet : Bet
	{
		public TypeInsideBet type { get; private set; }


		internal InsideBet(TypeInsideBet typeInsideBet, decimal amount)
			: base(amount)
		{
			this.type = typeInsideBet;
		}
	}
}
