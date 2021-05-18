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
            try
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
            catch (Exception e)
            {
                throw new MyCameraException(e.Message);
            }
        }

        private static readonly string[] devices;
        public int DeviceIndex { get; private set; } = 1000;

        public event EventHandler<VideoCaptureEventArgs> OnBitmapChanged;

        public void FreezeCameraImage()
        {
            throw new NotImplementedException();
        }
        private void DelegateEventCallback(Toupcam.eEVENT eEVENT)
        {
            switch (eEVENT)
            {
                case Toupcam.eEVENT.EVENT_EXPOSURE:
                    break;
                case Toupcam.eEVENT.EVENT_TEMPTINT:
                    break;
                case Toupcam.eEVENT.EVENT_CHROME:
                    break;
                case Toupcam.eEVENT.EVENT_IMAGE:
                    OnEventImage();
                    break;
                case Toupcam.eEVENT.EVENT_STILLIMAGE:
                    break;
                case Toupcam.eEVENT.EVENT_WBGAIN:
                    break;
                case Toupcam.eEVENT.EVENT_TRIGGERFAIL:
                    break;
                case Toupcam.eEVENT.EVENT_BLACK:
                    break;
                case Toupcam.eEVENT.EVENT_FFC:
                    break;
                case Toupcam.eEVENT.EVENT_DFC:
                    break;
                case Toupcam.eEVENT.EVENT_ROI:
                    break;
                case Toupcam.eEVENT.EVENT_ERROR:
                    break;
                case Toupcam.eEVENT.EVENT_DISCONNECTED:
                    break;
                case Toupcam.eEVENT.EVENT_NOFRAMETIMEOUT:
                    break;
                case Toupcam.eEVENT.EVENT_AFFEEDBACK:
                    break;
                case Toupcam.eEVENT.EVENT_AFPOSITION:
                    break;
                case Toupcam.eEVENT.EVENT_NOPACKETTIMEOUT:
                    break;
                case Toupcam.eEVENT.EVENT_FACTORY:
                    break;
                default:
                    break;
            }
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
            if (cam_ is null)
            {

                cam_ = Toupcam.Open(devices[index]);
                cam_.put_Chrome(true);
                int width = 0, height = 0;
                if (cam_.get_Size(out width, out height))
                {
                    DeviceIndex = index;
                    bmp_ = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                    
                    var pointer = new IntPtr();
                    
                    if (!cam_.StartPullModeWithCallback(DelegateEventCallback))
                    {
                        throw new MyCameraException("failed to start device");
                    }
                    else
                    {
                        bool autoexpo = true;
                        cam_.get_AutoExpoEnable(out autoexpo);
                    }
                }


              //  CapturingTask = new Task(() =>
              //  {
              //      while (true)
              //      {
              //          OnEventImage();
              //      }
              //  });
              //  CapturingTask.ConfigureAwait(false);
              //  CapturingTask.Start();
              //// CapturingTask.RunSynchronously();
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
            if (bmp_ is not null)
            {
                var bmpdata = bmp_.LockBits(new Rectangle(0, 0, bmp_.Width, bmp_.Height), ImageLockMode.WriteOnly, bmp_.PixelFormat);
                var info = new Toupcam.FrameInfoV2();
                cam_.PullImageV2(bmpdata.Scan0, 24, out info);
                bmp_.UnlockBits(bmpdata);
                try
                {
                    var img = bmp_;
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

        public void SetSettings(CameraSettings settings)
        {
            cam_.put_Chrome(settings.monochrome);
            cam_.put_Brightness(settings.brightness);
            cam_.put_Contrast(settings.contrast);
            cam_.put_Saturation(settings.saturation);
        }
    }
}
