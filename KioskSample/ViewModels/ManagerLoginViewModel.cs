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
    public class ManagerLoginViewModel : ViewModelBase
    {
        private string _id;
        public string ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value);}
        }

        public ICommand LoginCommand { get; set; }

        public ManagerLoginViewModel()
        {

        }
        public ManagerLoginViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            Init();
        }

        private void Init()
        {
            LoginCommand = new DelegateCommand(OnLogin);
        }

        private void OnLogin()
        {
            if (ID?.ToLower() != "admin" || Password != "password")
            {
                return;
            }
            AppContext.IsLogin = true;
            RegionManager.RequestNavigate("ManagerContentRegion", "Deadline");
        }
    }
}
