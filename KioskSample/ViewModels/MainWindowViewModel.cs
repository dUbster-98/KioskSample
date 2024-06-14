using KioskSample.Bases;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace KioskSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "Kiosk Sample";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }

        public MainWindowViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            var regionManager = ContainerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("KioskContentRegion", "KioskIntro");
            regionManager.RegisterViewWithRegion("ManagerContentRegion", "ManagerLogin");
        }
    }
}
