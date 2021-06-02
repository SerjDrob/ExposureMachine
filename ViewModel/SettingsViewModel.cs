using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExposureMachine.Classes;
using ExposureMachine.Common;
using PropertyChanged;
using Exposure_Machine.Model;
using System.Windows.Input;


namespace ExposureMachine.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    class SettingsViewModel
    {
        public SettingsViewModel(ICOM com, Dictionary<Buttons, int> valveAsignment)
        {
            OnMainViewClosingCmd = new Command(args => OnMainViewClosing(args));
            ComPorts = new ObservableCollection<string>(com.AvailablePorts);
            CurrentPortIndex = new List<string>(com.AvailablePorts).FindIndex(name => name == com.ConnectedPort);
            MyPortIsConnected = com.ConnectedPort is not null ? true : false;
            ValveNums = new() { 1, 2, 3, 4, 5, 6, 7 };
            ValveAssignment = valveAsignment;
            ExposureTime = ProgSettings.Default.ExposureTime;
        }
        public ICommand OnMainViewClosingCmd { get; set; }
        public ObservableCollection<int> ValveNums { get; set; }

        private (Buttons, int) _myProp;
        public (Buttons,int) MyProp 
        {
            get => _myProp;
            set
            {
                _myProp = value;
                ValveAssignment[value.Item1] = value.Item2;
            }
        }
        public Dictionary<Buttons, int> ValveAssignment {get; set;}
        public int CurrentPortIndex { get; set; }
        public bool MyPortIsConnected { get; }        
        public ObservableCollection<string> ComPorts { get; init; }
        public int ExposureTime { get; set; }
        public void OnMainViewClosing(object e)
        {
            ProgSettings.Default.ExposureTime = ExposureTime;
            ProgSettings.Default.Save();
            var r = ValveAssignment;
            ValveAssignment.SerializeToJson(ProgSettings.Default.ValvesSettings);
        }
    }
}
