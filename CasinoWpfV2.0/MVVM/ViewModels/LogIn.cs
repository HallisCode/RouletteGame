using CasinoWpfV2._0.Commands;
using CasinoWpfV2._0.MVVM.Models;
using CasinoWpfV2._0.MVVM.Views;
using System;
using System.Windows;

namespace CasinoWpfV2._0.MVVM.ViewModels
{
	internal class LogInPlayerViewModel : ViewModel
	{
		private string? _lastName;
		public string? lastName
		{
			get { return _lastName; }
			set
			{
				_lastName = value;

				Notify();
			}
		}

		private string? _name;
		public string? name
		{
			get { return _name; }
			set
			{
				_name = value;

				Notify();
			}
		}

		private decimal _balance;
		public decimal balance
		{
			get { return _balance; }
			set
			{
				_balance = value;

				Notify();
			}
		}

		public PlayerModel CreateUser()
		{
			if (!CheckPlayerDataValidation())
			{
				throw new Exception("Неккоректные данные пользователя!");
			}

			return new PlayerModel(_name, _lastName, _balance);
		}

		public bool CheckPlayerDataValidation()
		{
			if (!PlayerModel.CheckFullNameValidation(_name, _lastName)) return false;

			if (!TableModel.CheckValidityAmmountBet(_balance)) return false;

			return true;
		}
	}


	internal class LogInTableViewModel : ViewModel
	{
		private decimal _minAmmountBet;
		public decimal minAmmountBet
		{
			get { return _minAmmountBet; }
			set
			{
				_minAmmountBet = value;

				Notify();
			}
		}

		private decimal _maxAmmountBet;
		public decimal maxAmmountBet
		{
			get { return _maxAmmountBet; }
			set
			{
				_maxAmmountBet = value;

				Notify();
			}
		}

		public bool CheckTableDataValidation()
		{
			if (!CheckRangeBet(minAmmountBet, maxAmmountBet)) return false;

			return true;
		}

		private bool CheckRangeBet(params decimal[] ammountBets)
		{
			foreach (decimal ammount in ammountBets)
			{
				if (ammount < 0 || ammount > decimal.MaxValue) return false;
			}

			return true;
		}

	}


	internal class LogInViewModel : ViewModel
	{
		public LogInPlayerViewModel logInPlayerViewModel { get; private set; }

		public LogInTableViewModel logInTableViewModel { get; private set; }

		private RelayCommand _LogIngCommand;
		public RelayCommand LogIngCommand
		{
			get { return _LogIngCommand; }
		}

		public LogInViewModel()
		{
			_LogIngCommand = new RelayCommand(execute: (obj) => LogIn(), (obj) => CheckAvalibilityLogIn());

			logInPlayerViewModel = new LogInPlayerViewModel();

			logInTableViewModel = new LogInTableViewModel();
		}

		private void LogIn()
		{
			PlayerModel player = logInPlayerViewModel.CreateUser();

			decimal minAmmountBet = logInTableViewModel.minAmmountBet;

			decimal maxAmmountBet = logInTableViewModel.maxAmmountBet;


			GameWindow gameWindow = new GameWindow(player, minAmmountBet, maxAmmountBet);

			gameWindow.Show();


			Application.Current.Windows[0].Close();

			Application.Current.MainWindow = gameWindow;
		}

		private bool CheckAvalibilityLogIn()
		{
			if (!logInPlayerViewModel.CheckPlayerDataValidation() ||
				!logInTableViewModel.CheckTableDataValidation())
			{
				return false;
			}

			return true;
		}
	}
}
