using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDV.Behaviors
{
    public class ShowOnInactivityDetectedBehavior : Behavior<Grid>
    {
        private InactivityDetector? _inactivityDetector;
        private Point _inactiveMousePosition;



        public bool IsEnable
        {
            get { return (bool)GetValue(IsEnableProperty); }
            set { SetValue(IsEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnableProperty =
            DependencyProperty.Register("IsEnable", typeof(bool), typeof(ShowOnInactivityDetectedBehavior), new PropertyMetadata(true, OnIsEnableChanged));

        private static void OnIsEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {


            if (d is ShowOnInactivityDetectedBehavior inactivityDetectedBehavior)
            {

                if (e.NewValue.Equals(false))
                    inactivityDetectedBehavior._inactivityDetector?.StopTimer();
                else if (e.NewValue.Equals(true))
                    inactivityDetectedBehavior._inactivityDetector?.Start();
            }


        }

        public TimeSpan InativityTime { get; set; }

        public FrameworkElement Content { get; set; } = new Grid();
        protected override void OnAttached()
        {
            var index = AssociatedObject.Children.Count + 1;
            Content.Visibility = Visibility.Hidden;
            Panel.SetZIndex(Content, index);
            AssociatedObject.Children.Add(Content);
            _inactivityDetector = new(InativityTime);
            _inactivityDetector.DetectedInactivity += () =>
                 Dispatcher.Invoke(ShowElement);
        }

        private void ShowElement()
        {
            Content.Visibility = Visibility.Visible;
            _inactiveMousePosition = Mouse.GetPosition(Application.Current.MainWindow);
            InputManager.Current.PreProcessInput += OnActivity;
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
                    return;
                }
                Debug.WriteLine("Event Mouse Click/M\n");

            }
            else if (inputEventArgs is not KeyEventArgs)
            {
                return;
            }
            HiddenAndDisableEventAnddRestartDetector();
        }

        private void HiddenAndDisableEventAnddRestartDetector()
        {
            Content.Visibility = Visibility.Hidden;
            InputManager.Current.PreProcessInput -= OnActivity;
            _inactivityDetector?.Start();
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

        public void StopTimer()
        {
            _timer.Stop();
        }

        internal void Start()
        {
            _timer.Start();
        }
    }
}
