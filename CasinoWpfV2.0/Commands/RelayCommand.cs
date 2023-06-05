using System;
using System.Windows.Input;

namespace CasinoWpfV2._0.Commands
{
	internal class RelayCommand : ICommand
	{
		public event EventHandler? CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}


		private Action<object?> execute;

		private Func<object?, bool>? canExecute;


		public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
		{
			this.execute = execute;

			this.canExecute = canExecute;
		}

		public bool CanExecute(object? parameter)
		{
			return canExecute == null || canExecute(parameter);
		}

		public void Execute(object? parameter)
		{
			execute(parameter);
		}
	}
}
