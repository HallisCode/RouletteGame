using CasinoWpf.Commands;
using CasinoWpf.Models;
using RouletteLib;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CasinoWpf.ViewModels
{
	internal class CasinoViewModel : INotifyPropertyChanged
	{
		private Player _player;

		private Table table;

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


		private decimal _rate;

		public decimal rate
		{
			get
			{
				return _rate;
			}

			set
			{
				_rate = value;

				Notify();
			}
		}


		private Image _wheel;
		public Image wheel
		{
			get { return _wheel; }
			set
			{
				_wheel = value;

				_wheel.RenderTransform = new RotateTransform();
				_wheel.RenderTransformOrigin = new Point(0.5, 0.5);
			}
		}


		private Command _commandSpinWheel;

		public Command commandSpinWheel
		{
			get { return _commandSpinWheel; }
			private set { _commandSpinWheel = value; }
		}


		public void SpinWheel()
		{
			int winningCell = table.Spin();

			QuarticEase fluidAnimation = new QuarticEase();
			fluidAnimation.EasingMode = EasingMode.EaseInOut;

			RotateTransform rotateTransform = (RotateTransform)_wheel.RenderTransform;

			DoubleAnimation rotateAnimation = new DoubleAnimation();

			double currentAngel = rotateTransform.Angle % 360;
			rotateTransform.Angle = currentAngel;

			rotateAnimation.From = currentAngel;
			rotateAnimation.To = currentAngel + 360d * 6d + winningCell * 360d / table.roulette.cells.Length;
			rotateAnimation.Duration = TimeSpan.FromSeconds(8);
			rotateAnimation.EasingFunction = fluidAnimation;


			rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
		}

		public bool CheckAvailabilitywheel()
		{
			if (_wheel is null || table.rate is null || _balance is 0) return false;

			return true;
		}

		public CasinoViewModel(Player player)
		{
			this._player = player;

			commandSpinWheel = new Command(SpinWheel, CheckAvailabilitywheel);

			table = new Table();
		}


		public event PropertyChangedEventHandler PropertyChanged;


		private void Notify([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
