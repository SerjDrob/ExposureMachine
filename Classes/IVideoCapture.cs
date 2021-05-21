﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ExposureMachine
{
    public class CameraSettings
    {
        public bool Monochrome { get; set; }
        public int Brightness { get; set; }
        public int Contrast { get; set; }
        public int Saturation { get; set; }
    }
}

namespace ExposureMachine.Classes
{   
    
    interface IVideoCapture
    {   
        public void StartCamera(int index);
        public int DeviceIndex { get; }
        public void FreezeCameraImage();
        public void StopCamera();
        public int GetDevicesCount();
        public void SetSettings(CameraSettings settings);
        public event EventHandler<VideoCaptureEventArgs> OnBitmapChanged;
    }
    public class VideoCaptureEventArgs : EventArgs
    {
        public VideoCaptureEventArgs(int index, BitmapImage bi)
        {
            DeviceNum = index;
            BI = bi;
        }
        public int DeviceNum { get; }
        public BitmapImage BI { get; }
    }
}
