using CasinoWpf.Commands;
using CasinoWpf.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace CasinoWpf.ViewModels
{
	class RegistryViewModel : ViewModel
	{
		public UIElement form;


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

		public bool CheckForm()
		{
			foreach (object child in LogicalTreeHelper.GetChildren(form))
			{
				TextBox? element = child as TextBox;

				if (element == null) continue;

				if (Validation.GetHasError(element))
				{
					return false;
				}
			}

			return true;
		}


		public string[] errors;

		public override string? this[string propName]
		{
			get
			{
				string? error = null;

				switch (propName)
				{

					case (nameof(name)):

						if (String.IsNullOrWhiteSpace(name) || String.IsNullOrEmpty(name))
						{
							error = "Неккорктное имя пользователя";
						}
						else if (name.Length > 12)
						{
							error = "Слишком длинное имя пользователя";
						}

						break;

					case (nameof(lastName)):

						if (String.IsNullOrWhiteSpace(lastName) || String.IsNullOrWhiteSpace(lastName))
						{
							error = "Неккорктное имя пользователя";
						}
						else if (name.Length > 12)
						{
							error = "Слишком длинное имя пользователя";
						}

						break;

					case (nameof(balance)):

						if (balance < decimal.MinValue || balance > decimal.MaxValue)
						{
							error = "Слишком большое/маленькое число";
						}

						break;
				}

				return error;
			}
		}



		public RegistryViewModel()
		{
			_commandLogin = new Command(Login, CheckForm);
		}

	}
}
