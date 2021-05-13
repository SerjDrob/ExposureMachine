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
    public enum Buttons
    {
        [Description("Юстировка ФШ")]
        AlignmentMask,
        [Description("Фиксация ФШ")]
        FixingMask,
        [Description("Фиксация рамки ФШ")]
        FixingFrame,
        [Description("Фиксация подложки")]
        FixingSubstrate,
        [Description("Столик шаровая опора")]
        BallSupport,
        [Description("Подъём столика")]
        LiftingTable
    }
    [AddINotifyPropertyChangedInterface] 
    class MainViewModel
    {
        public ICommand PushCmd { get; set; }
        public ICommand SettingsCmd { get; set; }
        private IVideoCapture LeftCamera;
        private IVideoCapture RightCamera;
        public BitmapImage LeftImage { get; set; }
        public BitmapImage RightImage { get; set; }
        internal MainViewModel()
        {
            PushCmd = new Command(args => PushTheButton(args));
            ((Command)PushCmd).CanExecuteDelegate = StopExec;
            SettingsCmd = new Command(args => Settings());
            _comPort = new ValveSet("COM9");
            LeftCamera = new ToupCamera();
            RightCamera = new ToupCamera();
            LeftCamera.StartCamera(0);
            RightCamera.StartCamera(1);
            LeftCamera.OnBitmapChanged += Camera_OnBitmapChanged;
            RightCamera.OnBitmapChanged += Camera_OnBitmapChanged;
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

        private ICOM _comPort;
        private void Settings()
        {
            new SettingsView() { DataContext = new SettingsViewModel(_comPort) }.Show();
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
                            _comPort.WriteByte(0x1);
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
                            return false;
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
