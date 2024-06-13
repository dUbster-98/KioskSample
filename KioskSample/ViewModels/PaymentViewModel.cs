using KioskSample.Bases;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace KioskSample.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        public ICommand CompleteCommand { get; set; }

        public PaymentViewModel()
        {

        }

        public PaymentViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            Init();
        }

        private void Init()
        {
            CompleteCommand = new DelegateCommand(OnCompleted);
        }

        private void OnCompleted()
        {
            AppContext.CurrentOrder.OrderDatetime = DateTime.Now;
            //결제 완료 목록에 추가
            AppContext.Orders.Add(AppContext.CurrentOrder);
            AppContext.CurrentOrder = null;
            //처음 화면으로 이동
            ClearAppContextAndGoHome();
        }
    }
}
