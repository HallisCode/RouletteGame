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
	public partial class GameWindow : Window
	{
		private CasinoViewModel casinoViewModel;

		public GameWindow(PlayerModel player, decimal minAmmountBet, decimal maxAmmountBet)
		{
			InitializeComponent();

			System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");


			WheelModel wheel = new WheelModel(Wheel);

			TableModel table = new TableModel(player, wheel, minAmmountBet, maxAmmountBet);


			casinoViewModel = new CasinoViewModel(table);

			DataContext = casinoViewModel;
		}

		private void cellbet_click(object sender, RoutedEventArgs e)
		{
			CellBet cellBet = (CellBet)e.Source;


			casinoViewModel.AcceptCellBet(cellBet);

			if (casinoViewModel.chip is null) CreateChip();

			MoveChip(cellBet);
		}

		private void MoveChip(CellBet cellBet)
		{
			Point pointMoveTo = cellBet.TranslatePoint(new Point(0, 0), FieldChip);

			pointMoveTo.X += cellBet.RenderSize.Width / 2;
			pointMoveTo.Y += cellBet.RenderSize.Height / 2;

			casinoViewModel.chip?.MoveTo(pointMoveTo);
		}

		private ChipModel CreateChip()
		{
			Point spawnPoint = betAmount.TranslatePoint(new Point(0, 0), FieldChip);

			spawnPoint.X += betAmount.RenderSize.Width / 2;
			spawnPoint.Y += betAmount.RenderSize.Height / 2;

			return casinoViewModel.chip = new ChipModel(30d, 30d, FieldChip, spawnPoint);
		}
	}
}
