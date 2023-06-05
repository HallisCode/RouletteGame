using CasinoWpfV2._0.Commands;
using CasinoWpfV2._0.MVVM.Models;
using RouletteLib;
using System.Diagnostics;

namespace CasinoWpfV2._0.MVVM.ViewModels
{
	internal class CasinoViewModel : ViewModel
	{
		private TableModel _table;


		private RelayCommand _SpinWheelCommand;
		public RelayCommand SpinWheelCommand
		{
			get { return _SpinWheelCommand; }
		}


		private PlayerModel _player;
		public PlayerModel player
		{
			get { return _player; }
			set
			{
				_player = value;

				Notify();
			}
		}


		private decimal _betAmount;
		public decimal betAmount
		{
			get { return _betAmount; }
			set
			{
				_betAmount = value;

				Notify();
			}
		}


		public CasinoViewModel(TableModel tableModel)
		{
			_table = tableModel;

			_player = _table.player;

			_SpinWheelCommand = new RelayCommand(execute: (obj) => SpinWheel(), obj => CheckAvalibilitySpin());
		}

		public void MakeBet(TypeOutsideBet typeOutsideBet)
		{
			_table.MakeBet(typeOutsideBet, _betAmount);
		}

		public void MakeBet(TypeInsideBet typeInsideBet, int[] numbers)
		{
			_table.MakeBet(typeInsideBet, _betAmount, numbers);
		}

		public void SpinWheel()
		{
			_table.SpinWheel();
		}

		private bool CheckAvalibilitySpin()
		{
			if (_player.balance <= 0 || !_player.CheckMoney(_betAmount) || _table.bet is null ||
				_table.isSpinning is true)
			{
				return false;
			}

			return true;
		}
	}
}
