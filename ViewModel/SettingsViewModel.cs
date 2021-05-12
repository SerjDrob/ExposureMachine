using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExposureMachine.Classes;

namespace ExposureMachine.ViewModel
{
    class SettingsViewModel
    {
        public SettingsViewModel(ICOM com)
        {            
            ComPorts = new ObservableCollection<string>(com.AvailablePorts);
            CurrentPortIndex = new List<string>(com.AvailablePorts).FindIndex(name => name == com.ConnectedPort);
            MyPortIsConnected = com.ConnectedPort is not null ? true : false;
        }
       
        public int CurrentPortIndex { get; set; }
        public bool MyPortIsConnected { get; }
        public ObservableCollection<string> ComPorts { get; init; }
    }
}
