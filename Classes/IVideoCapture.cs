using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExposureMachine
{

}

namespace ExposureMachine.Classes
{
    internal interface IVideoCapture
    {
        Dictionary<int, (string, string[])> AvaliableVideoCaptureDevices { get; }
        bool IsVideoCaptureConnected { get; }
        string VideoCaptureMessage { get; }
        void StartCamera(int index, int capabilitiesInd=0);
        int DeviceIndex { get; }
        void FreezeCameraImage();
        void StopCamera();
        int GetVideoCapureDevicesCount();
        int GetVideoCapabilitiesCount();
        void SetSettings(CameraSettings settings);
        event EventHandler<VideoCaptureEventArgs> OnBitmapChanged;
    }
}
