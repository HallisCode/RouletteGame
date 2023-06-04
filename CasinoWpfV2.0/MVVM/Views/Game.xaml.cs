using CasinoWpfV2._0.MVVM.Models;
using CasinoWpfV2._0.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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


			PlayerModel player = new PlayerModel("Andrew", "Atamanov", 5000m);

			WheelModel wheel = new WheelModel(Wheel);

			TableModel table = new TableModel(player, wheel);

			table.MakeBet(RouletteLib.TypeInsideBet.Split, 500m, new int[] { 3, 2 });

			DataContext = new CasinoViewModel(table);
		}
	}
}
