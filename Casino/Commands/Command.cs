using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CasinoWpf.Commands
{
	internal class Command : ICommand
	{
		private Action execute;
		private Func<bool> canExecute;


		public event EventHandler? CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public Command(Action execute, Func<bool> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public bool CanExecute(object? parameter)
		{
			return this.canExecute();
		}

		public void Execute(object? parameter)
		{
			this.execute();
		}
	}
}
