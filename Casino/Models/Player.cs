using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWpf.Models
{
	public class Player : Model
	{
		public string name { get; private set; }
		public string lastName { get; private set; }

		public decimal _balance;

		public decimal balance
		{
			get { return _balance; }
			internal set
			{
				_balance = value;

				Notify();
			}
		}

		public Player(string name, string lastName, decimal balance)
		{
			this.name = name;

			this.lastName = lastName;

			this.balance = balance;
		}
	}
}
