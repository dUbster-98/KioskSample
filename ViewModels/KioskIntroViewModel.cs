using DryIoc;
using KioskSample.Bases;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace KioskSample.ViewModels
{
	public class KioskIntroViewModel : ViewModelBase
	{
        public ICommand MouseDownCommand { get; set; }  

        public KioskIntroViewModel()
        {

        }

        public KioskIntroViewModel(IContainerProvider containerProvider) : base(containerProvider) 
        {
            Init();
        }

        private void Init()
        {
            MouseDownCommand = new DelegateCommand(OnMouseDown);
        }

        private void OnMouseDown()
        {
            RegionManager.RequestNavigate("KioskContentRegion", "OrderStart");
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //동영상 재생이 xaml에서 자체적으로 동작하고 있기 때문에 OnNavigatedFrom에서 중지하는 코드를 추가하지 않았습니다. 

        }
    }
}
