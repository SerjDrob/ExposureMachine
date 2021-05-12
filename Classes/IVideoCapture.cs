using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ExposureMachine.Classes
{   
    interface IVideoCapture
    {   
        public void StartCamera(int index);
        public int DeviceIndex { get; init; }
        public void FreezeCameraImage();
        public void StopCamera();
        public int GetDevicesCount();

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
