using GalaSoft.MvvmLight.Messaging;
using MainApp.Validators;
using MainApp.ViewModels;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Container = new Container();
            Container.RegisterSingleton<MainWindow>();
            Container.RegisterSingleton<MainVM>();
            Container.RegisterSingleton<ListPageVM>();
            Container.Register<AddPageVM>();
            Container.Register<DetailsPageVM>();

            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<Messenger>();
            Container.RegisterSingleton<AddPageVmValidator>();

            var main = Container.GetInstance<MainWindow>();
            main.Show();

            base.OnStartup(e);
        }
    }
}
