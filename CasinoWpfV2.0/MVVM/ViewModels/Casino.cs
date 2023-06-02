using CasinoWpfV2._0.MVVM.Models;
using RouletteLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoWpfV2._0.MVVM.ViewModels
{
    internal class CasinoViewModel : ViewModel
    {
        private TableModel _table;


        public PlayerModel player;


        private decimal _betAmount;

        public decimal betAmount
        {
            get { return _betAmount; }
            set
            {
                _betAmount = value;

                Notify();
            }
        }

        public void MakeBet(TypeOutsideBet typeOutsideBet)
        {
            _table.MakeBet(typeOutsideBet, _betAmount);
        }

        public void MakeBet(TypeInsideBet typeInsideBet, int[] numbers)
        {
            _table.MakeBet(typeInsideBet, _betAmount, numbers);
        }

        public void SpinWheel()
        {
            _table.SpinWheel();
        }

        public CasinoViewModel(TableModel tableModel)
        {
            _table = tableModel;

            player = _table.player;
        }
    }
}
