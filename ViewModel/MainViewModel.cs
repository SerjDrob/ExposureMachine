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
using System.Windows;
using PropertyChanged;
using ExposureMachine.Common;

namespace ExposureMachine.ViewModel
{


    [AddINotifyPropertyChangedInterface] 
    class MainViewModel
    {
        public ICommand PushCmd { get; set; }
        public ICommand SettingsCmd { get; set; }
        public ICommand PromptsCmd { get; set; }
        public ICommand ShowVideoSettingsCmd { get; set; }
        public ICommand OnMainViewClosingCmd { get; set; }
        private IVideoCapture LeftCamera;
        private IVideoCapture RightCamera;
        private byte _valvesCondition = default;
        public BitmapImage LeftImage { get; set; }
        public BitmapImage RightImage { get; set; }
        public Visibility PromptsVisibility { get; set; } = Visibility.Collapsed;        

        private CameraSettings _leftCameraSettings;
        public CameraSettings LeftCameraSettings 
        {
            get => _leftCameraSettings;
            set
            {
                _leftCameraSettings = value;
                CameraSettings(LeftCamera, value);
            }
        }

        private CameraSettings _rightCameraSettings;
        public CameraSettings RightCameraSettings
        {
            get => _rightCameraSettings;
            set
            {
                _rightCameraSettings = value;
                CameraSettings(RightCamera, value);
            }
        }

        public bool LeftCameraVisibility { get; set; } = false;
        public bool RightCameraVisibility { get; set; } = false;

        internal MainViewModel()
        {
            PushCmd = new Command(args => PushTheButton(args));
            ((Command)PushCmd).CanExecuteDelegate = StopExec;
            SettingsCmd = new Command(args => Settings());
            PromptsCmd = new Command(args => SetPrompts());
            ShowVideoSettingsCmd = new Command(args => ShowVideoSettings(args));
            OnMainViewClosingCmd = new Command(args => OnMainViewClosing(args));
            _comValves = new ValveSet("COM4");
           
            LeftCameraSettings = ApplyCameraSettings(ProgSettings.Default.LeftCameraSettings);
            RightCameraSettings = ApplyCameraSettings(ProgSettings.Default.RightCameraSettings);
            _valveAssignment = ApplyValveAssignment(ProgSettings.Default.ValvesSettings);
            try
            {
                LeftCamera = new ToupCamera();
                RightCamera = new ToupCamera();
                CameraSettings(LeftCamera, LeftCameraSettings);
                CameraSettings(RightCamera, RightCameraSettings);
                
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

        private Dictionary<Buttons, int> ApplyValveAssignment(string filename)
        {
            var result = StaticMethods.DeSerializeObjectJson<Dictionary<Buttons, int>>(ProgSettings.Default.ValvesSettings);
            if (result is null)
            {
                result = new()
                {
                    { Buttons.AlignmentMask, 1 },
                    { Buttons.BallSupport, 2 },
                    { Buttons.Exposing, 8 },
                    { Buttons.FixingFrame, 3 },
                    { Buttons.FixingMask, 4 },
                    { Buttons.FixingSubstrate, 5 },
                    { Buttons.FixingTable, 6 },
                    { Buttons.Gap, 7 },
                    { Buttons.LiftingTable, 0 }
                };
            }
            return result;
        }
        private CameraSettings ApplyCameraSettings(string filename)
        {
            var s = StaticMethods.DeSerializeObjectJson<CameraSettings>(filename);
            
            if (s is null)
            {
                s = new() { Brightness = 10, Contrast = 26, Monochrome = true, Saturation = 55 };
            }
            return s;
        }
        public void OnMainViewClosing(object e)
        {
            var res = LeftCameraSettings.SerializeToJson(ProgSettings.Default.LeftCameraSettings);
            res = RightCameraSettings.SerializeToJson(ProgSettings.Default.RightCameraSettings);
        }
        private void ShowVideoSettings(object obj)
        {
            if (obj is string)
            {
                LeftCameraVisibility = (string)obj == "LeftCam";
                RightCameraVisibility = (string)obj == "RightCam";
            }
        }

        private void CameraSettings(IVideoCapture cam, CameraSettings settings)
        {   
            cam?.SetSettings(settings);
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
        private void SetPrompts()
        {
            PromptsVisibility = PromptsVisibility switch
            {
                Visibility.Collapsed => Visibility.Visible,
                Visibility.Visible => Visibility.Collapsed
            };
        }
        
        private ICOM _comValves;
        private Dictionary<Buttons, int> _valveAssignment;
        private void Settings()
        {
            new SettingsView() { DataContext = new SettingsViewModel(_comValves, _valveAssignment) }.Show();
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
                    var v = (byte)(1 << (_valveAssignment[button] - 1));
                    _valvesCondition ^= (byte)(1<<(_valveAssignment[button]-1));
                    _comValves.WriteLine(_valvesCondition.ByteToString());
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
