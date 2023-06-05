using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CasinoWpfV2._0.XamlTools.Controllers
{
	/// <summary>
	/// Interaction logic for CellBet.xaml
	/// </summary>
	public partial class CellBet : UserControl
	{
		public object type { get; set; }

		public string numbers { get; set; }

		public event RoutedEventHandler? click;


		public CellBet()
		{
			InitializeComponent();
		}

		public int[] GetNumbersArray()
		{
			if (String.IsNullOrEmpty(numbers)) return Array.Empty<int>();

			return numbers.Split(',').Select(symbol => int.Parse(symbol)).ToArray();
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			click?.Invoke(this, e);
		}
	}
}
