using CasinoWpfV2._0.Models;
using RouletteLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWpfV2._0.ViewModels
{
	internal class CasinoViewModel : ViewModel
	{
		private TableModel _table;


		private decimal _betAmount;

		public decimal betAmount
		{
			get { return _betAmount; }
			set
			{
				_betAmount = value;

				_table.betAmmount = value;

				Notify();
			}
		}

		public void MakeBet(TypeOutsideBet typeOutsideBet, decimal amount)
		{
			_table.MakeBet(typeOutsideBet, amount);
		}

		public void MakeBet(TypeInsideBet typeInsideBet, decimal amount, int[] numbers)
		{
			_table.MakeBet(typeInsideBet, amount, numbers);
		}

		public void SpinWheel()
		{
			_table.SpinWheel();
		}

		public CasinoViewModel(TableModel tableModel)
		{
			_table = tableModel;
		}
	}
}
