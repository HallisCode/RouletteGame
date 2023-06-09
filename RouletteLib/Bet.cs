using System;


namespace RouletteLib
{
	public enum BetStatus
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

		public BetStatus status { get; internal set; } = BetStatus.Waiting;

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

		public int[] numbers { get; private set; }


		internal InsideBet(TypeInsideBet typeInsideBet, decimal amount, int[] numbers)
			: base(amount)
		{
			this.type = typeInsideBet;

			int sizeOfArray = 0;

			switch (typeInsideBet)
			{
				case (TypeInsideBet.StraightUp):

					sizeOfArray = 1;

					break;

				case (TypeInsideBet.Split):

					sizeOfArray = 2;

					break;

				case (TypeInsideBet.Street):

					sizeOfArray = 3;

					break;

				case (TypeInsideBet.SixLine):

					sizeOfArray = 6;

					break;

				case (TypeInsideBet.Corner):

					sizeOfArray = 4;

					break;

				case (TypeInsideBet.FirstFour):

					sizeOfArray = 4;

					break;
			}

			if (sizeOfArray != numbers.Length)
				throw new Exception("Количество ставок не соответствует типу ставки!");

			this.numbers = numbers;
		}
	}
}
