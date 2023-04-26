using CasinoWpf.Models;
using CasinoWpf.ViewModels;
using System.Windows;


namespace CasinoWpf.Views
{
	/// <summary>
	/// Interaction logic for Registry.xaml
	/// </summary>
	public partial class RegistryWindow : Window
	{
		public RegistryWindow()
		{
			InitializeComponent();

			RegistryViewModel registryViewModel = new RegistryViewModel();

			registryViewModel.form = Form;

			DataContext = registryViewModel;
		}
	}
}
