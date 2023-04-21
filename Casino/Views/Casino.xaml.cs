using CasinoWpf.Models;
using CasinoWpf.ViewModels;
using System.Windows;


namespace CasinoWpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class CasinoWindow : Window
	{

		public CasinoWindow(Player player)
		{
			InitializeComponent();

			CasinoViewModel casinoViewModel = new CasinoViewModel(player);

			DataContext = casinoViewModel;

			casinoViewModel.wheel = Wheel;
		}
	}
}
