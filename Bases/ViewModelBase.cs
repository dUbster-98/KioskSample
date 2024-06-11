using DryIoc;
using KioskSample.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace KioskSample.Bases
{
    //INavigationAware를 추가해서 RegionManager.RequestNavigate() 를 이용해서 네비게이션 시켰을 때 실행할 내용을 추가할 수 있도록 작업 했습니다.
    public class ViewModelBase : BindableBase, INavigationAware
    {
        protected IContainerProvider ContainerProvider;
        protected IRegionManager RegionManager;

        //ViewModelBase에서 AppContext를 주입받아 사용할 수 있음 
        private IAppContext _appContext;
        public IAppContext AppContext
        {
            get { return _appContext; }
            set { SetProperty(ref _appContext, value); }
        }

        public ICommand HomeCommand { get; set; }   


        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public ViewModelBase()
        {

        }

        public ViewModelBase(IContainerProvider containerProvider)
        {
            ContainerProvider = containerProvider;
            RegionManager = ContainerProvider.Resolve<IRegionManager>();
            AppContext = containerProvider.Resolve<IAppContext>();
            Init();
        }

        private void Init()
        {
            HomeCommand = new DelegateCommand<string>(OnHome);
        }

        private void OnHome(string viewType)
        {
            if (viewType != "kiosk")
            {
                return;
            }
            ClearAppContextAndGoHome();
        }

        protected void ClearAppContextAndGoHome()
        {
            AppContext.IsEatIn = false;

            var region = RegionManager.Regions["KioskContentRegion"];
            if (region == null) 
            {
                return;
            }
            region.RemoveAll();
            region.RequestNavigate("KioskIntro");
        }

        //OnNavigatedTo, IsNavigationTarget, OnNavigatedFrom 메서드를 뷰모델에서 사용할 수 있도록 virtual 키워드를 추가했습니다.
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
