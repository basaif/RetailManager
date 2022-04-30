﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMDesktopUI.EventModels;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private SalesViewModel _salesVM;

        private IEventAggregator _events;
        public ShellViewModel(IEventAggregator events, SalesViewModel salesVM)
        {
            _events = events;
            _events.Subscribe(this);
            _salesVM = salesVM;

            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
        }
    }
}
