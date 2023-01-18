using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Exposure_Machine.Model;
using ExposureMachine.Classes;
using ExposureMachine.Common;
using ExposureMachine.View;
//using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Timers;
using System.Windows;
//using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace ExposureMachine.ViewModel;

[INotifyPropertyChanged]
public partial class MainViewModel
{
   
    public int CountDownTime { get; set; }
    private Timer _timer;
    private readonly IVideoCapture LeftCamera;
    private readonly IVideoCapture RightCamera;
    private byte _valvesCondition = default;
    private ICOM _comValves;
    private Dictionary<Buttons, int> _valveAssignment;
    private const string SETTINGS_FOLDER = "Settings";
    private CameraSettings _leftCameraSettings;
    private CameraSettings _rightCameraSettings;

    public bool IsExposing { get; set; } = false;
    public int ExposingTime { get; set; }
    public BitmapImage LeftImage { get; set; }
    public BitmapImage RightImage { get; set; }
    public Visibility PromptsVisibility { get; set; } = Visibility.Collapsed;
    public CameraSettings LeftCameraSettings
    {
        get => _leftCameraSettings;
        set
        {
            _leftCameraSettings = value;
            CameraSettings(LeftCamera, value);
        }
    }
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
    public bool LeftCameraXMirror { get; set; }
    public bool RightCameraXMirror { get; set; }
    public bool LeftCameraYMirror { get; set; }
    public bool RightCameraYMirror { get; set; }
    private bool LeftCamToRightCamChanged { get; set; }
    public MainViewModel(IVideoCapture leftCamera, IVideoCapture rightCamera)
    {
        _comValves = new ValveSet("COM5");
        LeftCamera = leftCamera;
        RightCamera = rightCamera;

        LeftCameraSettings = ApplyCameraSettings(ProgSettings.Default.LeftCameraSettings);
        RightCameraSettings = ApplyCameraSettings(ProgSettings.Default.RightCameraSettings);
        LeftCameraXMirror = ProgSettings.Default.LeftCameraXMirror;
        LeftCameraYMirror = ProgSettings.Default.LeftCameraYMirror;

        RightCameraXMirror = ProgSettings.Default.RightCameraXMirror;
        RightCameraYMirror = ProgSettings.Default.RightCameraYMirror;
        LeftCamToRightCamChanged = ProgSettings.Default.LeftCamToRightChanged;

        _valveAssignment = ApplyValveAssignment();
        _timer = new Timer();
        _timer.Interval = 1000;
        _timer.Elapsed += _timer_Elapsed;
        CountDownTime = ProgSettings.Default.ExposureTime;
        try
        {
            //LeftCamera = new USBCamera();// new ToupCamera();
            //RightCamera = new USBCamera();
            //CameraSettings(LeftCamera, LeftCameraSettings);
            //CameraSettings(RightCamera, RightCameraSettings);

            LeftCamera.StartCamera(0);
            RightCamera.StartCamera(1);
            LeftCamera.OnBitmapChanged += LeftCamera_OnBitmapChanged;
            RightCamera.OnBitmapChanged += RightCamera_OnBitmapChanged;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
       
    }

    private void RightCamera_OnBitmapChanged(object sender, VideoCaptureEventArgs e) => RightImage = e.Image;
    private void LeftCamera_OnBitmapChanged(object sender, VideoCaptureEventArgs e) => LeftImage = e.Image;
    [RelayCommand]
    private void MirrorCamera(object cameraTransforms)
    {
        switch (cameraTransforms)
        {
            case CameraTransforms transforms:
                switch (transforms)
                {
                    case CameraTransforms.LeftCameraXMirror:
                        LeftCameraXMirror ^= true;
                        break;
                    case CameraTransforms.LeftCameraYMirror:
                        LeftCameraYMirror ^= true;
                        break;
                    case CameraTransforms.RightCameraXMirror:
                        RightCameraXMirror ^= true;
                        break;
                    case CameraTransforms.RightCameraYMirror:
                        RightCameraYMirror ^= true;
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }
    private void _timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        ExposingTime--;
        if (ExposingTime == 0)
        {
            IsExposing = false;
            _timer.Stop();
        }
    }
    private Dictionary<Buttons, int> ApplyValveAssignment()
    {
        var fileName = Path.Combine(SETTINGS_FOLDER, ProgSettings.Default.ValvesSettings);
        var result = StaticMethods.DeSerializeObjectJson<Dictionary<Buttons, int>>(fileName);
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
    private CameraSettings ApplyCameraSettings(string fileName)
    {
        var filePath = Path.Combine(SETTINGS_FOLDER, fileName);
        var s = StaticMethods.DeSerializeObjectJson<CameraSettings>(filePath);

        if (s is null)
        {
            s = new() { Brightness = 10, Contrast = 26, Monochrome = true, Saturation = 55 };
        }
        return s;
    }
    [RelayCommand]
    public void OnMainViewClosing(object e)
    {
        var res = LeftCameraSettings.SerializeToJson(ProgSettings.Default.LeftCameraSettings);
        res = RightCameraSettings.SerializeToJson(ProgSettings.Default.RightCameraSettings);

        ProgSettings.Default.LeftCameraXMirror = LeftCameraXMirror;
        ProgSettings.Default.LeftCameraYMirror = LeftCameraYMirror;
        ProgSettings.Default.RightCameraXMirror = RightCameraXMirror;
        ProgSettings.Default.RightCameraYMirror = RightCameraYMirror;
        ProgSettings.Default.LeftCamToRightChanged = LeftCamToRightCamChanged;
        ProgSettings.Default.Save();
    }
    [RelayCommand]
    private void ShowVideoSettings(object obj)
    {
        if (obj is string)
        {
            LeftCameraVisibility = (string)obj == "LeftCam";
            RightCameraVisibility = (string)obj == "RightCam";
        }
    }
    [RelayCommand]
    private void ReplaceCameras()
    {
        LeftCamToRightCamChanged ^= true;
    }
    private void CameraSettings(IVideoCapture cam, CameraSettings settings)
    {
        cam?.SetSettings(settings);
    }
    [RelayCommand]
    private void SetPrompts()
    {
        PromptsVisibility = PromptsVisibility switch
        {
            Visibility.Collapsed => Visibility.Visible,
            Visibility.Visible => Visibility.Collapsed
        };
    }
    [RelayCommand]
    private void Settings()
    {
        new SettingsView() { DataContext = new SettingsViewModel(_comValves, _valveAssignment) }.ShowDialog();
        CountDownTime = ProgSettings.Default.ExposureTime;
    }
    [RelayCommand(CanExecute = nameof(StopExec))]
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
                    case Buttons.Exposing:
                        ExposingTime = CountDownTime;
                        _timer.Start();
                        break;
                    default:
                        break;
                }
                var v = (byte)(1 << _valveAssignment[button] - 1);
                _valvesCondition ^= (byte)(1 << _valveAssignment[button] - 1);
                _comValves.WriteLine(_valvesCondition.ByteToString());
               // _comValves.WriteLine("010101010101");
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
