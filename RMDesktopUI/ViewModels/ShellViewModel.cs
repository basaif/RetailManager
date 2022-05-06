using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private SalesViewModel _salesVM;

        private IEventAggregator _events;

        private ILoggedInUserModel _user;
        public ShellViewModel(IEventAggregator events, SalesViewModel salesVM,
            ILoggedInUserModel user)
        {
            _events = events;
            _events.Subscribe(this);
            _salesVM = salesVM;

            ActivateItem(IoC.Get<LoginViewModel>());
            _user = user;
        }

        public void ExitApplication()
        {
            TryClose();
        }

        public void LogOut()
        {
            _user.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);

        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public bool IsLoggedIn {
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(_user.Token))
                {
                    output = true;
                }

                return output;
            } 
        }
    }
}
