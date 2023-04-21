using CasinoWpf.Commands;
using CasinoWpf.Models;
using RouletteLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace CasinoWpf.ViewModels
{
	internal class CasinoViewModel : ViewModel
	{
		public Player player { get; private set; }

		public Table table { get; private set; }


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
			if (_wheel is null || table.rate is null || player.balance is 0) return false;

			return true;
		}

		public CasinoViewModel(Player player)
		{
			this.player = player;

			commandSpinWheel = new Command(SpinWheel, CheckAvailabilitywheel);

			table = new Table();
		}
	}
}
