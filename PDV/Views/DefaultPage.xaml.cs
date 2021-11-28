using PDV.Ultis.Moc;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace PDV.Views
{
    /// <summary>
    /// Interaction logic for DefaultPage.xaml
    /// </summary>
    public partial class DefaultPage
    {
        InactivityDetector inactivityDetector = new(TimeSpan.FromSeconds(4));
        private Point _inactiveMousePosition;
        private Window parentWindow;

        public DefaultPage()
        {
            InitializeComponent();
            inactivityDetector.DetectedInactivity += () =>
                 Dispatcher.Invoke(ShowLockScreen);
            _inactiveMousePosition = Mouse.GetPosition(Application.Current.MainWindow);
            parentWindow = Application.Current.MainWindow;            
        }

        private void OnActivity(object sender, PreProcessInputEventArgs e)
        {
            InputEventArgs inputEventArgs = e.StagingItem.Input;
            if (inputEventArgs is MouseEventArgs mea)
            {
                // no button is pressed and the position is still the same as the application became inactive
                if (mea.LeftButton == MouseButtonState.Released &&
                    mea.RightButton == MouseButtonState.Released &&
                    mea.MiddleButton == MouseButtonState.Released &&
                    mea.XButton1 == MouseButtonState.Released &&
                    mea.XButton2 == MouseButtonState.Released &&
                    _inactiveMousePosition == mea.GetPosition(Application.Current.MainWindow))
                {
                    Debug.WriteLine("mouseMove\n");
                    return;
                }
                HiddenAndDisableEventAnddRestartDetector();
            }
        }

        private void ShowLockScreen()
        {
            Visibility = Visibility.Visible;
            parentWindow.PreviewKeyDown += KeyDownHandler;
            InputManager.Current.PreProcessInput += OnActivity;
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            HiddenAndDisableEventAnddRestartDetector();
        }

        private void HiddenAndDisableEventAnddRestartDetector()
        {
            Visibility = Visibility.Hidden;
            parentWindow.PreviewKeyDown -= KeyDownHandler;
            InputManager.Current.PreProcessInput -= OnActivity;
            inactivityDetector.Start();
            _inactiveMousePosition = Mouse.GetPosition(Application.Current.MainWindow);
        }
    }

    public class InactivityDetector
    {
        private static readonly TimeSpan TickTimeSpan = TimeSpan.FromSeconds(1);
        private readonly Timer _timer = new(TickTimeSpan.TotalMilliseconds);
        private readonly double _secondsOfInativity;
        public event Action? DetectedInactivity;

        public InactivityDetector(TimeSpan time)
        {
            _secondsOfInativity = time.TotalSeconds;
            _timer.Elapsed += OnTick;
            _timer.Start();

        }
        private void OnTick(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine("TICK\n");
            if (GetIdleTime() > _secondsOfInativity)
            {
                StopTimer();
                DetectedInactivity?.Invoke();
            }
        }
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dwTime;
        }

        // returns seconds since last input
        static long GetIdleTime()
        {
            var info = new LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO)) };
            GetLastInputInfo(ref info);
            return (Environment.TickCount - info.dwTime) / 1000;
        }

        private void StopTimer()
        {
            _timer.Stop();
        }

        internal void Start()
        {
            _timer.Start();
        }
    }
}
