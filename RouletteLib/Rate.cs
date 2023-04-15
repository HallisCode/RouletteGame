﻿using System;


namespace RouletteLib
{
	public enum RateStatus
	{
		Waiting,
		Winning,
		Loss
	}


	/// <summary>
	/// Описывает абстрактную ставку в рулетке.
	/// </summary>
	public abstract class Rate
	{
		public RouletteUser owner { get; protected set; }

		public decimal rate { get; protected set; }

		public RateStatus status { get; internal set; } = RateStatus.Waiting;

		public decimal winningAmount { get; internal set; }


		protected Rate(RouletteUser ownerRate, decimal rate)
		{
			if (ownerRate is null) throw new Exception("Пользователь не может быть null.");

			if (rate <= 0) throw new Exception("Неккоректная сумма ставки");

			if (ownerRate.money < rate) throw new Exception("Недостаточно средств у пользователя.");

			this.owner = ownerRate;

			this.owner.money -= rate;

			this.rate = rate;
		}
	}

	/// <summary>
	/// Описывает внешнюю ставку в рулетке.
	/// </summary>
	public class RateExternal : Rate
	{
		public ExternalRate typeRate { get; private set; }

		internal RateExternal(RouletteUser ownerRate, ExternalRate foreignRate, decimal rate)
			: base(ownerRate, rate)
		{
			this.typeRate = foreignRate;
		}
	}

	/// <summary>
	/// Описывает внутреннюю ставку в рулетке.
	/// </summary>
	public class RateDomestic : Rate
	{
		public DomesticRate typeRate { get; private set; }

		internal RateDomestic(RouletteUser ownerRate, DomesticRate domesticRate, decimal rate)
			: base(ownerRate, rate)
		{
			this.typeRate = domesticRate;
		}
	}
}