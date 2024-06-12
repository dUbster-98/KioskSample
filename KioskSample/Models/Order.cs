using Prism.Common;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSample.Models
{
    public class Order : BindableBase
    {
        public Guid OrderId { get; set; }

        private int _totalQuantity;
        public int TotalQuantity 
        { 
            get { return _totalQuantity;} 
            set { SetProperty(ref _totalQuantity, value); }
        }

        private int _totalAmount;
        public int TotalAmount
        {
            get { return _totalAmount;}
            set { SetProperty(ref _totalAmount, value); }
        }

        public IList<OrderDetail> Items { get; set; } = new ObservableCollection<OrderDetail>();
        
        public void UpdateProperties()
        {
            TotalQuantity = Items.Sum(x => x.Quantity);
            TotalAmount = Items.Sum(x => x.Amount);
        }

    }
}
