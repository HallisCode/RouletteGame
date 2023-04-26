using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RouletteLib;

namespace CasinoWpf.Utils
{
	class CellOutsideBet : DependencyObject
	{
		public static readonly DependencyProperty TypeProperty =
			DependencyProperty.RegisterAttached("Type", typeof(TypeOutsideBet), typeof(CellOutsideBet));

		public static void SetType(UIElement element, int value)
		{
			element.SetValue(CellOutsideBet.TypeProperty, value);
		}

		public static TypeOutsideBet GetType(UIElement element)
		{
			return (TypeOutsideBet)element.GetValue(CellOutsideBet.TypeProperty);
		}
	}



}
