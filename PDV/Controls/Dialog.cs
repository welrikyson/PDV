using PDV.Interfaces;
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
        public IDialogService DialogService
        {
            get { return (IDialogService)GetValue(DialogServiceProperty); }
            set { SetValue(DialogServiceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DialogService.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DialogServiceProperty =
            DependencyProperty.Register(nameof(DialogService), typeof(IDialogService), typeof(Dialog), new PropertyMetadata(propertyChangedCallback: DialogServiceChangedHandler));

        private static void DialogServiceChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dialog = (Dialog)d;
            dialog.DialogServiceChangedHandler(e.NewValue);
        }
        private void DialogServiceChangedHandler(object newValue)
        {
            if(newValue is IDialogService dialogService)
            {                
                dialogService.OpenDialogEvent +=
                                OnOpenedDialogHandler;

                dialogService.CloseDialogEvent +=
                     OnClosedDialogHandler;
            }
        }        
        

        private void OnOpenedDialogHandler(object content)
        {
            Content = content;
            IsOpen = true;
            Focus();
            
            if(Parent is Grid grid)
            {                
                foreach (UIElement item in grid.Children)
                {
                    if (item.Equals(this)) continue;
                    item.IsEnabled = false;
                } 
            } 
            
        }

        private void OnClosedDialogHandler(object content)
        {
            if (Parent is Grid grid)
            {
                foreach (UIElement item in grid.Children)
                {
                    if (item.Equals(this)) continue;
                    item.IsEnabled = true;
                }
            }
            IsOpen = false;                        
        }

        static Dialog()
        {   
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Dialog),
                                                     new FrameworkPropertyMetadata(typeof(Dialog)));
            
        }        

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            UpdateLayout();
            MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
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
                                        new FrameworkPropertyMetadata(default(bool),
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
                
                RaiseEvent(new(OpenedEvent));
            }
            else
            {                
                RaiseEvent(new(ClosedEvent));
            }
        }
    }
}
