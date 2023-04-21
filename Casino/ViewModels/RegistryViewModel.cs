using CasinoWpf.Commands;
using CasinoWpf.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CasinoWpf.ViewModels
{
	class RegistryViewModel : ViewModel
	{
		private string _name;

		public string name
		{
			get { return _name; }
			set
			{
				_name = value;

				Notify();
			}
		}


		private string _lastName;

		public string lastName
		{
			get { return _lastName; }
			set
			{
				_lastName = value;

				Notify();
			}
		}


		private decimal _balance;

		public decimal balance
		{
			get
			{
				return _balance;
			}

			set
			{
				_balance = value;

				Notify();
			}
		}

		private Command _commandLogin;

		public Command commandLogin
		{
			get { return _commandLogin; }
			private set { _commandLogin = value; }
		}

		public void Login()
		{
			Player player = new Player(_name, _lastName, _balance);

			CasinoWindow casinoWindow = new CasinoWindow(player);

			Application.Current.MainWindow.Close();

			casinoWindow.Show();
		}

		public RegistryViewModel()
		{
			_commandLogin = new Command(Login, () => true);
		}

	}
}
