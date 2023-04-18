using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace Casino
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();

			Wheel.RenderTransform = new RotateTransform();

		}

		private void Spin_Click(object sender, RoutedEventArgs e)
		{
			QuarticEase fluidAnimation = new QuarticEase();

			fluidAnimation.EasingMode = EasingMode.EaseInOut;


			RotateTransform rotateTransform = (RotateTransform)Wheel.RenderTransform;


			DoubleAnimation rotateAnimation = new DoubleAnimation();


			double currentAngel = rotateTransform.Angle % 360;

			rotateTransform.Angle = currentAngel;

			Random random = new Random();

			rotateAnimation.From = currentAngel;
			rotateAnimation.To = currentAngel + 360d * 6d + random.Next(1, 38) * 360d / 37d;
			rotateAnimation.Duration = TimeSpan.FromSeconds(8);
			rotateAnimation.EasingFunction = fluidAnimation;



			rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);




		}
	}
}
