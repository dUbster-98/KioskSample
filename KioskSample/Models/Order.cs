﻿using Prism.Common;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

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
            set { SetProperty(ref _totalAmount, value, () => RaisePropertyChanged(nameof(Change))); }
        }

        private int _receivedAmount;
        public int ReceivedAmount
        {
            get { return _receivedAmount; }
            set { SetProperty(ref _receivedAmount, value, () => RaisePropertyChanged(nameof(Change))); }
        }

        public int Change
        {
            get { return ReceivedAmount - TotalAmount < 0 ? 0 : ReceivedAmount - TotalAmount; }
        }

        public IList<OrderDetail> Items { get; set; } = new ObservableCollection<OrderDetail>();
        
        public void UpdateProperties()
        {
            TotalQuantity = Items.Sum(x => x.Quantity);
            TotalAmount = Items.Sum(x => x.Amount);
        }

        public DateTime? OrderDatetime { get; set; }

        private bool _isDeadline;
        public bool IsDeadline 
        { 
            get => _isDeadline; 
            set => SetProperty(ref _isDeadline, value); 
        }

        private DateTime? _deadlineDatetime;
        public DateTime? DeadlineDatetime 
        {
            get => _deadlineDatetime;
            set => SetProperty(ref _deadlineDatetime, value); 
        }
    }
}
