using KioskSample.Bases;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace KioskSample.ViewModels
{
    public class OrderStartViewModel : ViewModelBase
    {
        private DispatcherTimer _timer;

        public ICommand OrderTypeCommand { get; set; }
        public ICommand DisabledCommand { get; set; }   

        public OrderStartViewModel()
        {

        }
        public OrderStartViewModel(IContainerProvider containerProvider) : base(containerProvider) 
        {
            Init();
        }

        private void Init()
        {
            OrderTypeCommand = new DelegateCommand<string>(OnOrderType);
            DisabledCommand = new DelegateCommand<string>(OnDisabled);

            _timer = new DispatcherTimer(TimeSpan.FromSeconds(30),
                DispatcherPriority.Normal, TimerTick, App.Current.Dispatcher);        
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ClearAppContextAndGoHome();
        }

        private void OnOrderType(string orderType)
        {
            AppContext.IsEatIn = orderType.ToLower() == "eatin" ? true : false;
            // 주문 구분 선택 후 메뉴 선택 화면으로 이동
            RegionManager.RequestNavigate("KioskContentRegion", "SelectMenu");
        }
        private void OnDisabled(string para) 
        {
            AppContext.IsDisabledUI = para.ToLower() == "disabled" ? true : false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            AppContext.KioskStatus = Commons.StatusEnum.OrderStart;
            _timer.Stop();
            _timer.Start();
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Destroy();
        }

        public override void Destroy()
        {
            DestroyTimer();
        }

        public void DestroyTimer()
        {
            _timer.Stop();
            _timer = null;
        }

    }
}
