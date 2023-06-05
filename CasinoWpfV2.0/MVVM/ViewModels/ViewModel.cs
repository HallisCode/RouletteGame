using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CasinoWpfV2._0.MVVM.ViewModels
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
