using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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
                new FrameworkPropertyMetadata(typeof(MainGrid)));
        }
       
        // Create a custom routed event by first registering a RoutedEventID
        // This event uses the bubbling routing strategy
        public static readonly RoutedEvent MKeyDownEvent = EventManager.RegisterRoutedEvent(
            "MKeyDown",
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
        public bool SholdConsumeOnKeyMPressed { get; set; }
        // Here KeyDown attached event is customized for the desired key
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            var popup = new Popup();
            
            if (e.Key == Key.M)
            {
                RaiseSpaceKeyDownEvent();
                e.Handled = SholdConsumeOnKeyMPressed;
            }
        }
    }
}
