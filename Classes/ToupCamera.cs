using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.IO;

namespace ExposureMachine.Classes
{
    class ToupCamera : IVideoCapture
    {
        static ToupCamera()
        {
            var arr = Toupcam.EnumV2();

            if (arr.Length <= 0)
            {
                throw new MyCameraException("no device");
            }
            else
            {
                devices = new string[arr.Length];
                devices = arr.Select(a => a.id).ToArray();
            }
        }

        private static readonly string[] devices;
        public int DeviceIndex { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public event EventHandler<VideoCaptureEventArgs> OnBitmapChanged;

        public void FreezeCameraImage()
        {
            throw new NotImplementedException();
        }

        public int GetDevicesCount()
        {
            throw new NotImplementedException();
        }

        public void StartCamera(int index)
        {
            if (index>devices.Length-1)
            {
                throw new MyCameraException($"Камера с индексом {index} не подключена");
            }
            if (cam_ != null)
            {
                cam_ = Toupcam.Open(devices[index]);
                CapturingTask = new Task(() =>
                {
                    while (true)
                    {
                        OnEventImage();
                    }
                });
                CapturingTask.ConfigureAwait(false);
            }
        }
        private Task CapturingTask; 
        public void StopCamera()
        {
            throw new NotImplementedException();
        }

        private Toupcam cam_ = null;
        
        private Bitmap bmp_ = null;
        
        private uint MSG_CAMEVENT = 0x8001; // WM_APP = 0x8000

        private void OnEventError()
        {
            if (cam_ != null)
            {
                cam_.Close();
                cam_ = null;
            }
           throw new MyCameraException("Error");
        }

        private void OnEventDisconnected()
        {
            if (cam_ != null)
            {
                cam_.Close();
                cam_ = null;
            }
            throw new MyCameraException("The camera is disconnected, maybe has been pulled out.");
        }

        private void OnEventExposure()
        {
            if (cam_ != null)
            {
                uint nTime = 0;
                if (cam_.get_ExpoTime(out nTime))
                {
                    //trackBar1.Value = (int)nTime;
                    //label1.Text = (nTime / 1000).ToString() + " ms";
                }
            }
        }

        private void OnEventImage()
        {
            if (bmp_ != null)
            {
                BitmapData bmpdata = bmp_.LockBits(new Rectangle(0, 0, bmp_.Width, bmp_.Height), ImageLockMode.WriteOnly, bmp_.PixelFormat);
                Toupcam.FrameInfoV2 info = new Toupcam.FrameInfoV2();
                cam_.PullImageV2(bmpdata.Scan0, 24, out info);
                bmp_.UnlockBits(bmpdata);
                try
                {
                    using var img = bmp_;


                    var ms = new MemoryStream();
                    img.Save(ms, ImageFormat.Bmp);

                    ms.Seek(0, SeekOrigin.Begin);

                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    OnBitmapChanged?.Invoke(null, new VideoCaptureEventArgs(DeviceIndex, bitmap));
                }
                catch (Exception ex)
                {
                }
            }
        }
        
    }
}
