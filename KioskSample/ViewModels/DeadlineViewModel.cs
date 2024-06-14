using DryIoc;
using KioskSample.Bases;
using KioskSample.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KioskSample.ViewModels
{
    public class DeadlineViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /*
        private IList<Order> _currentOrders;
        public IList<Order> CurrentOrders
        {
            get => _currentOrders;
            set => SetProperty(ref _currentOrders, value);
        }
        */
        private ObservableCollection<Order> _currentOrders;
        public ObservableCollection<Order> CurrentOrders
        {
            get => _currentOrders;
            set => SetProperty(ref _currentOrders, value);
        }

        public ICommand MakeDeadlineCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        private DateTime _deadlineDatetime;
        public DateTime DeadlineDatetime
        {
            get => _deadlineDatetime;
            set => SetProperty(ref _deadlineDatetime, value);
        }

        public DeadlineViewModel()
        {

        }

        public DeadlineViewModel(IContainerProvider containerProvider) : base(containerProvider) 
        {
            Init();
        }

        private void Init()
        {
            MakeDeadlineCommand = new DelegateCommand(OnMakeDeadline);
            LogoutCommand = new DelegateCommand(OnLogout);
            SearchCommand = new DelegateCommand(async () => await OnSearchAsync());
        }

        private async Task OnSearchAsync()
        {
            if (DeadlineDatetime.Date == DateTime.Today)
            {
                CurrentOrders = AppContext.Orders;
            }
            else
            {
                await Task.Delay(10);

                ObservableCollection<Order> list = new();
                for(int i = 0; i < 100; i++)
                {
                    list.Add(new Order
                    {
                        OrderId = Guid.NewGuid(),
                        TotalQuantity = i,
                        TotalAmount = 10000 + i,
                        ReceivedAmount = 10000 + i,
                        OrderDatetime = DeadlineDatetime.Date,
                        IsDeadline = true,
                        DeadlineDatetime = DeadlineDatetime.Date,
                    });
                }
                CurrentOrders = list;              
            }
        }

        private void OnLogout()
        {
            MessageBoxResult result = MessageBox.Show("로그아웃 하시겠습니까?", "확인", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            LogoutManager();
        }

        private void OnMakeDeadline()
        {
            if (CurrentOrders.All(o => o.IsDeadline))
            {
                MessageBox.Show("모든 주문이 마감처리되어 있습니다.");
                return;
            }

            MessageBoxResult result = MessageBox.Show("마감처리 하시겠습니까?", "확인", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (Order order in CurrentOrders)
            {
                order.IsDeadline = true;
                order.DeadlineDatetime = DateTime.Now;
            }
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            DeadlineDatetime = DateTime.Now;
            await OnSearchAsync();
        }
    }
}
