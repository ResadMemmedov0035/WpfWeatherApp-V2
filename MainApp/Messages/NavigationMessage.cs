using GalaSoft.MvvmLight;
using MainApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Messages
{
    class NavigationMessage : IMessage
    {
        public ViewModelBase ViewModel { get; set; }
    }
}
