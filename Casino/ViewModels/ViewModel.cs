using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CasinoWpf.ViewModels
{
	internal class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;


		protected void Notify([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
