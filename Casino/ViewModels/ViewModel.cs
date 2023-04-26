using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CasinoWpf.ViewModels
{
	internal abstract class ViewModel : INotifyPropertyChanged, IDataErrorInfo
	{
		public virtual string this[string columnName] => throw new System.NotImplementedException();

		public string Error => throw new System.NotImplementedException();



		public event PropertyChangedEventHandler? PropertyChanged;


		protected void Notify([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
