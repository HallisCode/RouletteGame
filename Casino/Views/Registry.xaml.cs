using CasinoWpf.Models;
using System.Windows;


namespace CasinoWpf.Views
{
	/// <summary>
	/// Interaction logic for Registry.xaml
	/// </summary>
	public partial class Registry : Window
	{
		public Registry()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			CasinoWindow casinoWindow = new CasinoWindow(new Player());

			casinoWindow.Show();

			this.Close();
        }
    }
}
