using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CasinoWpf.Models
{
	public abstract class Model : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void Notify([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
