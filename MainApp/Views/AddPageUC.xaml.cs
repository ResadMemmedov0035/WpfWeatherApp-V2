using GalaSoft.MvvmLight.Messaging;
using MainApp.Messages;
using MainApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainApp.Views
{
    /// <summary>
    /// Interaction logic for AddPageUC.xaml
    /// </summary>
    public partial class AddPageUC : UserControl
    {
        public AddPageUC()
        {
            InitializeComponent();
        }

        private void MapRightMouseButtonClick(object sender, MouseButtonEventArgs e)
        {
            var location = Map.ViewportPointToLocation(e.GetPosition(Map));
            //if (DataContext is AddPageVM vm)
            //{
            //    vm.Latitude = location.Latitude.ToString();
            //    vm.Longitude = location.Longitude.ToString();
            //}
            var messenger = App.Container.GetInstance<Messenger>();
            messenger.Send(new LocationMessage() { Latitude = location.Latitude, Longitude = location.Longitude });
        }
    }
}
