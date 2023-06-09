using CasinoWpfV2._0.XamlTools.Controllers;
using CasinoWpfV2._0.MVVM.Models;
using CasinoWpfV2._0.MVVM.ViewModels;
using System.Windows;
using System.Globalization;

namespace CasinoWpfV2._0.MVVM.Views
{
	/// <summary>
	/// Interaction logic for Game.xaml
	/// </summary>
	public partial class Game : Window
	{
		private CasinoViewModel casinoViewModel;

		public Game()
		{
			InitializeComponent();

			System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

			PlayerModel player = new PlayerModel("Andrew", "Atamanov", 500000m);

			WheelModel wheel = new WheelModel(Wheel);

			TableModel table = new TableModel(player, wheel);

			casinoViewModel = new CasinoViewModel(table);

			DataContext = casinoViewModel;
		}

		private void cellbet_click(object sender, RoutedEventArgs e)
		{
			CellBet cellBet = (CellBet)e.Source;


			casinoViewModel.AcceptCellBet(cellBet);

			MoveChip(cellBet);
		}

		private void MoveChip(CellBet cellBet)
		{
			if (casinoViewModel.chip is null)
			{
				Point spawnPoint = betAmount.TranslatePoint(new Point(0, 0), FieldChip);

				spawnPoint.X += betAmount.RenderSize.Width / 2;
				spawnPoint.Y += betAmount.RenderSize.Height / 2;

				casinoViewModel.chip ??= new ChipModel(30d, 30d, FieldChip, spawnPoint);
			}

			Point pointMoveTo = cellBet.TranslatePoint(new Point(0, 0), FieldChip);

			pointMoveTo.X += cellBet.RenderSize.Width / 2;
			pointMoveTo.Y += cellBet.RenderSize.Height / 2;

			casinoViewModel.chip.MoveTo(pointMoveTo);
		}
	}
}
