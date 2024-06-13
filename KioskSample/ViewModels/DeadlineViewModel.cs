using KioskSample.Bases;
using KioskSample.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KioskSample.ViewModels
{
    public class DeadlineViewModel : ViewModelBase
    {
        private IList<Order> _currentOrders;
        public IList<Order> CurrentOrders
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

        }

        private void OnLogout()
        {

        }

        private void OnMakeDeadline()
        {

        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
