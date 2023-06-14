using RouletteLib;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace CasinoWpfV2._0.MVVM.Models
{
	/// <summary>
	/// Реализует логику рулетки с 37 ячейками.
	/// </summary>
	public class WheelModel
	{
		private Image _wheel;

		private RotateTransform _rotateTransform;

		private DoubleAnimation _rotateAnimation;


		private const double _secondsAnimation = 8d;

		private const int _additionalSpins = 5;


		private int _lastIndexCell;

		private readonly int[] _cells = Cells.all.Reverse().ToArray();

		private bool _isFirstSpin = true;

		public event EventHandler CompletedSpin
		{
			add
			{
				_rotateAnimation.Completed += value;
			}
			remove
			{
				_rotateAnimation.Completed -= value;
			}
		}


		public WheelModel(Image wheel)
		{
			_wheel = wheel;

			QuarticEase fluidAnimation = new QuarticEase()
			{
				EasingMode = EasingMode.EaseInOut
			};

			_rotateAnimation = new DoubleAnimation()
			{
				Duration = TimeSpan.FromSeconds(_secondsAnimation),
				EasingFunction = fluidAnimation
			};

			_rotateTransform = new RotateTransform();

			_wheel.RenderTransform = _rotateTransform;
			_wheel.RenderTransformOrigin = new Point(0.5, 0.5);
		}

		/// <summary>
		/// Запускает анимацию вращения рулетки.
		/// </summary>
		/// <param name="winningCell"></param>
		public void Spin(int winningCell)
		{
			int currentIndexCell = Array.IndexOf(_cells, winningCell);

			int rotateOn = 0;

			if (currentIndexCell - _lastIndexCell > 0)
			{
				int[] betweenElements = _cells.Skip(_lastIndexCell).Take(currentIndexCell - _lastIndexCell).ToArray();

				rotateOn = betweenElements.Count();
			}
			else if (currentIndexCell - _lastIndexCell < 0)
			{
				rotateOn += _cells.Length - _lastIndexCell;

				int[] betweenElements = _cells.Take(currentIndexCell).ToArray();

				rotateOn += betweenElements.Count();
			}

			if (_isFirstSpin)
			{
				_isFirstSpin = false;

				rotateOn += 1;
			}

			_rotateAnimation.From = _rotateTransform.Angle;
			_rotateAnimation.To = _rotateTransform.Angle + rotateOn * (360d / _cells.Length) + 360d * _additionalSpins;

			_rotateTransform.BeginAnimation(RotateTransform.AngleProperty, _rotateAnimation);

			_lastIndexCell = currentIndexCell;
		}
	}
}
