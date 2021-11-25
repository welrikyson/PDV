using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDV.Controls
{
    public class Dialog : ContentControl
    {   
        static Dialog()
        {   
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Dialog),
                                                     new FrameworkPropertyMetadata(typeof(Dialog)));
        }
        
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen),
                                        typeof(bool),
                                        typeof(Dialog),
                                        new FrameworkPropertyMetadata(false,
                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                      OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var dialog = (Dialog)d;
            dialog.OnIsOpenChanged();            
        }

        //Create a custom routed event by first registering a RoutedEventID
        //This event uses the bubbling routing strategy
        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
            nameof(Closed),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Dialog));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        public static readonly RoutedEvent OpenedEvent = EventManager.RegisterRoutedEvent(
            nameof(Opened),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Dialog));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Opened
        {
            add { AddHandler(OpenedEvent, value); }
            remove { RemoveHandler(OpenedEvent, value); }
        }

        // This method raises the SpaceKeyDown event
        protected virtual void OnIsOpenChanged()
        {
            if (IsOpen)
            {
                SetValue(IsOpenProperty, true);
                RaiseEvent(new(OpenedEvent));
            }
            else
            {
                SetValue(IsOpenProperty, false);
                RaiseEvent(new(ClosedEvent));
            }
        }
    }
}
