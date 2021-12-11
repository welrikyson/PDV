using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PDV.Controls
{
    public class MainGrid : ContentControl
    {
        // This constructor is provided automatically if you
        // add a Custom Control (WPF) to your project
        static MainGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MainGrid),
                new FrameworkPropertyMetadata(typeof(ContentControl)));
        }
        public MainGrid()
        {
            FocusManager.AddLostFocusHandler(this, OnLostFocus);
        }
        public IInputElement? LastFocusableElement { get; private set; }
        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is UIElement lastFocusedElement)
            {
                LastFocusableElement = lastFocusedElement;
            }
        }

        public void SetFocus()
        {
            if (LastFocusableElement == null) return;

            Dispatcher.BeginInvoke(action: delegate () { LastFocusableElement.Focus(); },
                                       priority: DispatcherPriority.Render);
        }

        // Create a custom routed event by first registering a RoutedEventID
        // This event uses the bubbling routing strategy
        public static readonly RoutedEvent MKeyDownEvent = EventManager.RegisterRoutedEvent(
            nameof(MKeyDown),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(MainGrid));

        // Provide CLR accessors for the event
        public event RoutedEventHandler MKeyDown
        {
            add { AddHandler(MKeyDownEvent, value); }
            remove { RemoveHandler(MKeyDownEvent, value); }
        }

        // This method raises the SpaceKeyDown event
        protected virtual void RaiseSpaceKeyDownEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(MKeyDownEvent);
            RaiseEvent(args);
        }
        public bool SholdConsumeOnKeyMPressed { get; set; } = true;
        // Here KeyDown attached event is customized for the desired key
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Handled) return;
            if (e.Key == Key.M)
            {
                RaiseSpaceKeyDownEvent();
                e.Handled = SholdConsumeOnKeyMPressed;
            }
        }
    }
}
