using CasinoWpfV2._0.Commands;
using CasinoWpfV2._0.MVVM.Models;
using CasinoWpfV2._0.XamlTools.Controllers;
using RouletteLib;
using System;
using System.Windows;
using Application = System.Windows.Application;

namespace CasinoWpfV2._0.MVVM.ViewModels
{
	internal class CasinoViewModel : ViewModel
	{
		private TableModel _table;

		private CellBet? _cellBet;


		public PlayerModel player { get; private set; }

		public ChipModel? chip { get; set; }


		private RelayCommand _SpinWheelCommand;
		public RelayCommand SpinWheelCommand
		{
			get { return _SpinWheelCommand; }
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

			player = _table.player;

			_SpinWheelCommand = new RelayCommand(execute: (obj) => SpinWheel(), obj => CheckAvalibilitySpin());

			_table.CompletedSpin += (obj, sender) => ShowSpinTotal();
		}

		public void AcceptCellBet(CellBet cellBet)
		{
			_cellBet = cellBet;
		}

		private void MakeBet()
		{
			if (_cellBet is null) return;

			switch (_cellBet.type)
			{
				case (TypeOutsideBet):

					_table.MakeBet((TypeOutsideBet)_cellBet.type, _betAmount);

					break;

				case (TypeInsideBet):

					_table.MakeBet((TypeInsideBet)_cellBet.type, _betAmount, _cellBet.GetNumbersArray());

					break;
			}
		}

		private void SpinWheel()
		{
			MakeBet();

			_table.SpinWheel();

			Application.Current.MainWindow.IsEnabled = false;
		}


		private bool CheckAvalibilitySpin()
		{
			if (_cellBet is null) return false;

			if (!_table.CheckRangeAmount(_betAmount)) return false;

			if (!player.CheckHasMoney(_betAmount)) return false;

			return true;
		}

		private void ShowSpinTotal()
		{
			if (_table.lastBetPlayed is null) throw new Exception("Отсутствует последняя сыгранная ставка!");

			string message = "";

			switch (_table.lastBetPlayed.status)
			{


				case (BetStatus.Winning):

					message = $"Поздравляю, Вы выйгриали!\nСумма вашего выигрыша : {_table.lastBetPlayed.winningAmount:N3}$ + {_table.lastBetPlayed.amount:N3}$";

					break;

				case (BetStatus.Loss):

					message = $"К сожалению, Вы проиграли!\nСумма вашего проигрыша: {_table.lastBetPlayed.amount:N3}$";

					break;
			}

			MessageBox.Show(message, "Итог", MessageBoxButton.OK, MessageBoxImage.Information);

			Application.Current.MainWindow.IsEnabled = true;
		}
	}
}
