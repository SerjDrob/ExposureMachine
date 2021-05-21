using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PropertyChanged;

namespace ExposureMachine.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    
    public partial class VideoSettingsPanel : UserControl
    {
        public VideoSettingsPanel()
        {            
            InitializeComponent();
            myLayout.DataContext = this;           
        }


        public bool Monochrome
        {
            get { return (bool)GetValue(MonochromeProperty); }
            set { SetValue(MonochromeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Monochrome.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonochromeProperty =
            DependencyProperty.Register("Monochrome", typeof(bool), typeof(VideoSettingsPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(ThePropertyChanged)));

        private static void ThePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var o = (VideoSettingsPanel)d;            
            o.Settings = new CameraSettings()
            {
                Monochrome = o.Monochrome,
                Brightness = o.Brightness,
                Contrast = o.Contrast,
                Saturation = o.Saturation
            };
        }

        public int Brightness
        {
            get { return (int)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Brightness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrightnessProperty =
            DependencyProperty.Register("Brightness", typeof(int), typeof(VideoSettingsPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(ThePropertyChanged)));

        public int Saturation
        {
            get { return (int)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Saturation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SaturationProperty =
            DependencyProperty.Register("Saturation", typeof(int), typeof(VideoSettingsPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                new PropertyChangedCallback(ThePropertyChanged)));

        public int Contrast
        {
            get { return (int)GetValue(ContrastProperty); }
            set { SetValue(ContrastProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contrast.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContrastProperty =
            DependencyProperty.Register("Contrast", typeof(int), typeof(VideoSettingsPanel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(ThePropertyChanged)));

        public CameraSettings Settings
        {
            get { return (CameraSettings)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Settings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingsProperty =
            DependencyProperty.Register("Settings", typeof(CameraSettings), typeof(VideoSettingsPanel),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(SettingsPropertyChanged)));

        private static void SettingsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var o = (VideoSettingsPanel)d;
            var settings = (CameraSettings)e.NewValue;
            o.Monochrome = settings.Monochrome;
            o.Saturation = settings.Saturation;
            o.Brightness = settings.Brightness;
            o.Contrast = settings.Contrast;
        }

        public bool ImVisible
        {
            get { return (bool)GetValue(ImVisibleProperty); }
            set { SetValue(ImVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImVisibleProperty =
            DependencyProperty.Register("ImVisible", typeof(bool), typeof(VideoSettingsPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                new PropertyChangedCallback(ImVisiblePropertyChanged)));

        private static void ImVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var o = (VideoSettingsPanel)d;
            o.Visibility = ((bool)e.NewValue)? Visibility.Visible : Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImVisible = false;
        }
    }
}
