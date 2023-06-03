﻿using CasinoWpf.Models;
using CasinoWpf.Utils;
using CasinoWpf.ViewModels;
using RouletteLib;
using System.Windows;

namespace CasinoWpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class CasinoWindow : Window
	{
		private CasinoViewModel casinoViewModel;


		public CasinoWindow(Player player)
		{
			InitializeComponent();

			casinoViewModel = new CasinoViewModel(player);

			DataContext = casinoViewModel;

			casinoViewModel.wheel = Wheel;
		}

		private void Table_SelectCell(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			TypeOutsideBet typeOutsideBet = CellOutsideBet.GetType((UIElement)e.Source);

			casinoViewModel.bet = casinoViewModel.table.MakeBet(typeOutsideBet, casinoViewModel.rate);
		}
	}
}
