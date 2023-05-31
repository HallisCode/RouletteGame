using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWpfV2._0.ViewModels
{
	internal abstract class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void Notify([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
