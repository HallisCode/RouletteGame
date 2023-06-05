using CasinoWpfV2._0.XamlTools.Controllers;
using CasinoWpfV2._0.MVVM.Models;
using CasinoWpfV2._0.MVVM.ViewModels;
using RouletteLib;
using System.Windows;

namespace CasinoWpfV2._0.MVVM.Views
{
	/// <summary>
	/// Interaction logic for Game.xaml
	/// </summary>
	public partial class Game : Window
	{
		public Game()
		{
			InitializeComponent();


			PlayerModel player = new PlayerModel("Andrew", "Atamanov", 500000m);

			WheelModel wheel = new WheelModel(Wheel);

			TableModel table = new TableModel(player, wheel);

			DataContext = new CasinoViewModel(table);
		}

		private void CellBet_click(object sender, RoutedEventArgs e)
		{
			CellBet cellBet = (CellBet)e.Source;

			CasinoViewModel casinoViewModel = (CasinoViewModel)DataContext;

			switch (cellBet.type)
			{
				case (TypeOutsideBet):

					casinoViewModel.MakeBet((TypeOutsideBet)cellBet.type);

					break;

				case (TypeInsideBet):

					casinoViewModel.MakeBet((TypeInsideBet)cellBet.type, cellBet.GetNumbersArray());

					break;
			}
		}
	}
}
