using CasinoWpfV2._0.Commands;
using CasinoWpfV2._0.MVVM.Models;
using CasinoWpfV2._0.XamlTools.Controllers;
using RouletteLib;
using System;
using System.Windows;

namespace CasinoWpfV2._0.MVVM.ViewModels
{
	internal class CasinoViewModel : ViewModel
	{
		private TableModel _table;

		public ChipModel? chip { get; set; }


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

			_table.CompletedSpin += (obj, sender) => ShowSpinTotal();
		}

		public bool MakeBet(CellBet cellBet)
		{
			if (_betAmount <= 0 || !_player.CheckHasMoney(_betAmount)) return false;

			switch (cellBet.type)
			{
				case (TypeOutsideBet):

					_table.MakeBet((TypeOutsideBet)cellBet.type, _betAmount);

					break;

				case (TypeInsideBet):

					_table.MakeBet((TypeInsideBet)cellBet.type, _betAmount, cellBet.GetNumbersArray());

					break;
			}

			return true;
		}

		private void SpinWheel()
		{
			if (_table.bet is null) _table.MakeLastBet(_betAmount);

			_table.SpinWheel();

			Application.Current.MainWindow.IsEnabled = false;
		}

		private bool CheckAvalibilitySpin()
		{
			if (_table.bet is null & _table.lastBetPlayed is null || !_player.CheckHasMoney(_betAmount))
			{
				return false;
			}

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
