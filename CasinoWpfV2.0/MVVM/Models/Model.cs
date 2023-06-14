using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CasinoWpfV2._0.MVVM.Models
{
	public class Model : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void Notify([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
