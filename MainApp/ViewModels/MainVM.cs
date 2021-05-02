using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MainApp.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.ViewModels
{
    class MainVM : ViewModelBase
    {
        private IMessenger Messenger;

        private ViewModelBase currentViewModel;

        public ViewModelBase CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }

        public MainVM(IMessenger messenger)
        {
            Messenger = messenger;
            CurrentViewModel = App.Container.GetInstance<ListPageVM>();

            Messenger.Register<NavigationMessage>(this, message =>
            {
                CurrentViewModel = message.ViewModel;
            });
        }
    }
}
