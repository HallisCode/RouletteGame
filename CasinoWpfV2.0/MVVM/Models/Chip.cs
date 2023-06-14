using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using CasinoWpfV2._0.XamlTools.Controllers;
using System.Windows.Media.Animation;
using System;

namespace CasinoWpfV2._0.MVVM.Models
{
	public class ChipModel
	{
		private Image _image;

		private static readonly ImageSource _imageSource;


		public double cacheX { get; private set; }

		public double cacheY { get; private set; }


		private DoubleAnimation _pointAnimationX;
		private DoubleAnimation _pointAnimationY;

		private const double _secondsAnimation = 0.75d;


		static ChipModel()
		{
			ResourceDictionary ResourceDictionary = new ResourceDictionary();

			ResourceDictionary.Source = new System.Uri("/Design/Icons/Chip.xaml", System.UriKind.Relative);

			_imageSource = (DrawingImage)ResourceDictionary["chip"];

		}


		public ChipModel(double height, double width, Canvas owner, System.Windows.Point point)
		{

			_image = new Image();

			_image.Source = _imageSource;

			_image.Height = height;
			_image.Width = width;

			owner.Children.Add(_image);

			cacheX = point.X - _image.Width / 2;
			cacheY = point.Y - _image.Height / 2;

			_image.SetValue(Canvas.LeftProperty, cacheX);
			_image.SetValue(Canvas.TopProperty, cacheY);


			QuarticEase fluidAnimation = new QuarticEase()
			{
				EasingMode = EasingMode.EaseIn
			};

			_pointAnimationX = new DoubleAnimation()
			{
				Duration = TimeSpan.FromSeconds(_secondsAnimation),
				EasingFunction = fluidAnimation
			};

			_pointAnimationY = new DoubleAnimation()
			{
				Duration = TimeSpan.FromSeconds(_secondsAnimation),
				EasingFunction = fluidAnimation
			};

			_image.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
		}

		public void MoveTo(System.Windows.Point point)
		{
			System.Windows.Point moveTo = new System.Windows.Point();

			moveTo.X = point.X - _image.Width / 2;
			moveTo.Y = point.Y - _image.Height / 2;

			_pointAnimationX.From = cacheX;
			_pointAnimationX.To = moveTo.X;

			_pointAnimationY.From = cacheY;
			_pointAnimationY.To = moveTo.Y;

			cacheX = moveTo.X;
			cacheY = moveTo.Y;

			_image.BeginAnimation(Canvas.LeftProperty, _pointAnimationX);
			_image.BeginAnimation(Canvas.TopProperty, _pointAnimationY);
		}
	}
}
