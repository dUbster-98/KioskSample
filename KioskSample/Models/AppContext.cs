using KioskSample.Commons;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KioskSample.Models
{
    public class AppContext : BindableBase, IAppContext
    {
        public bool IsLogin { get; set; }

        private bool _isOpenCase;
        public bool IsOpenCase
        {
            get { return _isOpenCase; }
            set { SetProperty(ref _isOpenCase, value); }
        }

        private bool _isDisabledUI;
        public bool IsDisabledUI
        {
            get { return _isDisabledUI; }
            set { SetProperty(ref _isDisabledUI, value); }
        }

        private StatusEnum _kioskStatus;
        public StatusEnum KioskStatus
        {
            get { return _kioskStatus; }
            set { SetProperty(ref _kioskStatus, value); }
        }

        private bool _isEatIn;
        public bool IsEatIn
        {
            get { return _isEatIn; }
            set { SetProperty(ref _isEatIn, value); }
        }

        private Order _currentOrder;
        public Order CurrentOrder
        {
            get { return _currentOrder; }
            set { SetProperty(ref _currentOrder, value); }
        }

        public IList<Order> Orders { get; set; } = new List<Order>();

        public AppContext()
        {

        }
    }
}
