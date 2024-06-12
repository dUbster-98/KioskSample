﻿using KioskSample.Models;
using KioskSample.Views;
using Prism.Ioc;
using System.Windows;

namespace KioskSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //IAppContext와 AppContext를 싱글톤으로 등록
            containerRegistry.RegisterSingleton<IAppContext, AppContext>(); 

            // KioskContentRegion으로 navigate되어야 하므로 화면을 등록해준다
            containerRegistry.RegisterForNavigation<KioskIntro>();
            containerRegistry.RegisterForNavigation<ManagerLogin>();
            containerRegistry.RegisterForNavigation<OrderStart>();
            containerRegistry.RegisterForNavigation<SelectMenu>();
        }
    }
}
