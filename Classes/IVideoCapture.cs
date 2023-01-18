using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExposureMachine.Classes
{
    public interface IVideoCapture
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
