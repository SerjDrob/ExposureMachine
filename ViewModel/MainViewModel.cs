using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Exposure_Machine.Model;
using System.Diagnostics;
using System.ComponentModel;
using ExposureMachine.Classes;
using ExposureMachine.View;
using System.Windows.Media.Imaging;
using PropertyChanged; 

namespace ExposureMachine.ViewModel
{
    [Flags]
    public enum Buttons
    {
        [Description("Юстировка ФШ")]
        AlignmentMask = 0b00000001,
        [Description("Фиксация ФШ")]
        FixingMask = 0b00000010,
        [Description("Фиксация рамки ФШ")]
        FixingFrame = 0b00000100,
        [Description("Фиксация подложки")]
        FixingSubstrate = 0b00001000,
        [Description("Столик шаровая опора")]
        BallSupport = 0b00010000,
        [Description("Подъём столика")]
        LiftingTable = 0b00100000
    }
    [AddINotifyPropertyChangedInterface] 
    class MainViewModel
    {
        public ICommand PushCmd { get; set; }
        public ICommand SettingsCmd { get; set; }
        private IVideoCapture LeftCamera;
        private IVideoCapture RightCamera;
        private byte _valvesCondition = default;
        public BitmapImage LeftImage { get; set; }
        public BitmapImage RightImage { get; set; }
        internal MainViewModel()
        {
            PushCmd = new Command(args => PushTheButton(args));
            ((Command)PushCmd).CanExecuteDelegate = StopExec;
            SettingsCmd = new Command(args => Settings());
            _comValves = new ValveSet("COM3");
            try
            {
                LeftCamera = new ToupCamera();
                RightCamera = new ToupCamera();
                LeftCamera.StartCamera(0);
                RightCamera.StartCamera(1);
                LeftCamera.OnBitmapChanged += Camera_OnBitmapChanged;
                RightCamera.OnBitmapChanged += Camera_OnBitmapChanged;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        private void Camera_OnBitmapChanged(object sender, VideoCaptureEventArgs e)
        {
            if (e.DeviceNum == LeftCamera.DeviceIndex)
            {
               LeftImage = e.BI;
            }
            if (e.DeviceNum == RightCamera.DeviceIndex)
            {
                RightImage = e.BI;
            }
        }

        private ICOM _comValves;
        private void Settings()
        {
            new SettingsView() { DataContext = new SettingsViewModel(_comValves) }.Show();
        }

        private void PushTheButton(object parameter)
        {
            switch (parameter)
            {
                case Buttons button:
                    switch (button)
                    {
                        case Buttons.AlignmentMask:                            
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.FixingMask:                            
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.FixingFrame:
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.FixingSubstrate:
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.BallSupport:
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.LiftingTable:
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        default:
                            break;
                    } 
                    _valvesCondition ^= (byte)button;
                    _comValves.WriteLine(_valvesCondition.ByteToString()+"\n");
                    Trace.WriteLine(_valvesCondition.ByteToString());
                    break;
                   
                default:
                    break;
            }
        }
        private bool StopExec(object parameter)
        {
            switch (parameter)
            {
                case Buttons button:
                    switch (button)
                    {
                        case Buttons.AlignmentMask:
                            //return false;
                            break;
                        case Buttons.FixingMask:
                            break;
                        case Buttons.FixingFrame:
                            break;
                        case Buttons.FixingSubstrate:
                            break;
                        case Buttons.BallSupport:
                            break;
                        case Buttons.LiftingTable:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}
