using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CasinoWpfV2._0.Models
{

	/// <summary>
	/// Реализует логику рулетки с 37 ячейками.
	/// </summary>
	internal class Wheel : Model
	{
		private Image _wheel;
		public Image wheel
		{
			get { return _wheel; }
			set
			{
				_wheel = value;

				Notify();
			}
		}




	}
}
