using Microsoft.Xaml.Behaviors;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDV.Behaviors
{
    /// <summary>
    /// avisa quando ocorre inatividade por determinado tempo
    /// 
    /// </summary>
    public class InativityManager
    {
        TimeSpan InactivityTime { get; }
        private readonly Timer InactivityTimer;
        private Point lastMousePosition;
        public event OnStatusChangedEventHander? OnStatusChangedEvent;

        public InativityManager(TimeSpan? inactivityTime)
        {
            if (inactivityTime == null) throw new ArgumentNullException(nameof(inactivityTime), "Inactivity time não pode ser nula.");
            InactivityTime = inactivityTime.Value;

            InactivityTimer = new(InactivityTime.TotalMilliseconds);
            InactivityTimer.Elapsed += EpapsedTimerInactivityHandler;

            InputManager.Current.PreProcessInput += OnActivity;
        }

        private void OnActivity(object sender, PreProcessInputEventArgs e)
        {
            bool HasActivityValid = e.StagingItem.Input switch
            {
                MouseEventArgs mouseEventArgs =>
                    mouseEventArgs.LeftButton != MouseButtonState.Released ||
                    mouseEventArgs.RightButton != MouseButtonState.Released ||
                    mouseEventArgs.MiddleButton != MouseButtonState.Released ||
                    mouseEventArgs.XButton1 != MouseButtonState.Released ||
                    mouseEventArgs.XButton2 != MouseButtonState.Released ||
                    //curso move
                    MouseMove(mouseEventArgs.GetPosition(Application.Current.MainWindow)),
                KeyEventArgs => true,
                _ => false,
            };

            if (HasActivityValid && Status == InactivityStatus.Active)
            {
                RestartTimer();
            }
            else if (Status == InactivityStatus.Inactive && HasActivityValid)
            {
                Status = InactivityStatus.Active;
                OnStatusChangedEvent?.Invoke(new(Status));
            }
        }

        private bool MouseMove(Point newCursoPosition)
        {
            if (lastMousePosition == newCursoPosition)
            {
                return false;
            }
            else
            {
                lastMousePosition = newCursoPosition;
                return true;
            }
        }

        private void RestartTimer()
        {
            StartTimer();
            StopTimer();
        }

        public void StartTimer()
        {
            InactivityTimer.Start();
        }
        public void StopTimer()
        {
            InactivityTimer.Stop();
        }

        public InactivityStatus Status { get; private set; } = InactivityStatus.Active;

        private void EpapsedTimerInactivityHandler(object sender, ElapsedEventArgs e)
        {
            StopTimer();
            Status = InactivityStatus.Inactive;
            OnStatusChangedEvent?.Invoke(new(Status));
        }
    }
    public delegate void OnStatusChangedEventHander(OnInactivityDetectedEventArgs onInactivityDetectedEventArgs);
    public class OnInactivityDetectedEventArgs : EventArgs
    {
        public OnInactivityDetectedEventArgs(InactivityStatus inactivityStatus)
        {
            InactivityStatus = inactivityStatus;
        }

        public InactivityStatus InactivityStatus { get; }
    }
    public enum InactivityStatus
    {
        Active,
        Inactive,
    }
    public class ShowOnInactivityDetectedBehavior : Behavior<Grid>
    {
        private InativityManager? InativityManager;
        public TimeSpan InativityTime { get; set; }

        public FrameworkElement Content { get; set; } = new Grid();
        protected override void OnAttached()
        {
            var index = AssociatedObject.Children.Count + 1;
            Content.Visibility = Visibility.Hidden;
            Panel.SetZIndex(Content, index);
            AssociatedObject.Children.Add(Content);
            InativityManager = new InativityManager(inactivityTime: InativityTime);
            InativityManager.OnStatusChangedEvent += (e) =>
                Dispatcher.Invoke(() => StatusChangedHandler(e));
            InativityManager.StartTimer();
        }

        private void StatusChangedHandler(OnInactivityDetectedEventArgs e)
        {
            Content.Visibility = e.InactivityStatus switch
            {
                InactivityStatus.Inactive => Visibility.Visible,
                InactivityStatus.Active => Visibility.Hidden,
                _ => throw new NotImplementedException(),
            };

        }
    }
}
