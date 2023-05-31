using RouletteLib;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace CasinoWpfV2._0.Models
{
	internal class Wheel
	{
		private Image _wheel;

		private RotateTransform rotateTransform;

		private DoubleAnimation rotateAnimation;

		private const int secondsAnimation = 8;

		private const int additionalSpins = 5;


		private int lastIndexCell;

		private int[] cells = Cells.all.Reverse().ToArray();

		private bool isFirstSpin;

		public event EventHandler? CompletedRotate;


		public Wheel(Image wheel)
		{
			_wheel = wheel;

			QuarticEase fluidAnimation = new QuarticEase()
			{
				EasingMode = EasingMode.EaseInOut
			};

			rotateAnimation = new DoubleAnimation()
			{
				Duration = TimeSpan.FromSeconds(secondsAnimation),
				EasingFunction = fluidAnimation
			};


			rotateTransform = (RotateTransform)_wheel.RenderTransform;
			rotateAnimation.Completed += CompletedRotate;
		}


		public void Spin(int winningCell)
		{
			int currentIndexCell = Array.IndexOf(cells, winningCell);

			int rotateOn = 0;

			if (currentIndexCell - lastIndexCell > 0)
			{
				int[] betweenElements = cells.Skip(lastIndexCell).Take(currentIndexCell - lastIndexCell).ToArray();

				rotateOn = betweenElements.Count();
			}
			else if (currentIndexCell - lastIndexCell < 0)
			{
				rotateOn += cells.Length - lastIndexCell;

				int[] betweenElements = cells.Take(currentIndexCell).ToArray();

				rotateOn += betweenElements.Count();
			}

			if (isFirstSpin)
			{
				isFirstSpin = false;

				rotateOn += 1;
			}

			rotateAnimation.From = rotateTransform.Angle;
			rotateAnimation.To = rotateTransform.Angle + rotateOn * (360d / cells.Length) + 360d * additionalSpins;

			rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

			lastIndexCell = currentIndexCell;
		}
	}
}
