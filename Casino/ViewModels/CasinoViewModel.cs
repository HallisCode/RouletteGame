using CasinoWpf.Commands;
using CasinoWpf.Models;
using RouletteLib;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace CasinoWpf.ViewModels
{
	internal class CasinoViewModel : ViewModel
	{
		private int lastIndexCell = 0;

		private int[] allCells = Cells.all.Reverse().ToArray();

		private bool isFirstSpin = true;

		public Player player { get; private set; }

		public Table table { get; private set; }

		public Bet? bet;


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
			// В процессе написания

			int winningCell = table.Spin();

			int currentIndexCell = Array.IndexOf(allCells, winningCell);

			int rotateOn = 0;

			if (currentIndexCell - lastIndexCell > 0)
			{
				int[] betweenElements = allCells.Skip(lastIndexCell).Take(currentIndexCell - lastIndexCell).ToArray();

				rotateOn = betweenElements.Count();
			}
			else if (currentIndexCell - lastIndexCell < 0)
			{
				rotateOn += allCells.Length - lastIndexCell;

				int[] betweenElements = allCells.Take(currentIndexCell).ToArray();

				rotateOn += betweenElements.Count();
			}

			if (isFirstSpin)
			{
				isFirstSpin = false;

				rotateOn += 1;
			}


			QuarticEase fluidAnimation = new QuarticEase();
			fluidAnimation.EasingMode = EasingMode.EaseInOut;

			RotateTransform rotateTransform = (RotateTransform)_wheel.RenderTransform;

			DoubleAnimation rotateAnimation = new DoubleAnimation();

			rotateAnimation.From = rotateTransform.Angle;
			rotateAnimation.To = rotateTransform.Angle + rotateOn * (360d / allCells.Length) + 360d * 5;
			rotateAnimation.Duration = TimeSpan.FromSeconds(8);
			rotateAnimation.EasingFunction = fluidAnimation;
			rotateAnimation.Completed += new EventHandler(Paymenter);


			rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

			lastIndexCell = currentIndexCell;
		}

		public void Paymenter(object? sender, EventArgs e)
		{
			if (bet.status == RateStatus.Winning)
			{
				player.balance += bet.winningAmount;
			}
			else
			{
				player.balance -= bet.amount;
			}
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
