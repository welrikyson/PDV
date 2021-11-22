using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PDV.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
            Dialog.Style = CreatNewDialogStyle(Dialog.Style);
            Dialog.CreateNewBooleanTrigger(receviesValue: true,
                                           inProperty: new(Controls.Dialog.IsOpenProperty),
                                           whenEvent: Controls.MainGrid.MKeyDownEvent,
                                           inElement: MainGrid);

            Dialog.CreateNewBooleanTrigger(receviesValue: false,
                                           inProperty: new(Controls.Dialog.IsOpenProperty),
                                           whenEvent: ButtonBase.ClickEvent,
                                           CloseDialogButton);

            content.CreateNewBooleanTrigger(receviesValue: false,
                                            inProperty: new(IsEnabledProperty),
                                            whenEvent: Controls.Dialog.OpenedEvent,
                                            inElement: Dialog);
            content.CreateNewBooleanTrigger(receviesValue: true,
                                            inProperty: new(IsEnabledProperty),
                                            whenEvent: Controls.Dialog.ClosedEvent,
                                            inElement: Dialog);
            FocusManager.AddLostFocusHandler(content, OnLostFocus);
            content.IsEnabledChanged += ContentEnableChangedHandler;
        }

        private Style CreatNewDialogStyle(Style from)
        {
            Style dialogStyle = new Style(typeof(Controls.Dialog), from)
            {

            };
            var trigger = new Trigger()
            {
                Property = Controls.Dialog.IsOpenProperty,
                Value = true,
            };

            trigger.Setters.Add(new Setter()
            {
                Property = FocusManager.FocusedElementProperty,
                Value = Dialog.Content,
            });
            dialogStyle.Triggers.Add(trigger);

            return dialogStyle;
        }

        private void ContentEnableChangedHandler(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {                
                if (LastFocusableElement == null) return;
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    LastFocusableElement.Focus();
                }), System.Windows.Threading.DispatcherPriority.Render);
            }
        }

        IInputElement? LastFocusableElement;
        private void OnLostFocus(object sender, RoutedEventArgs e)
        {            
            if (e.OriginalSource is UIElement lastFocusedElement)
            {
                LastFocusableElement = lastFocusedElement;
            }
        }
    }

    internal static class TriggersExtension
    {
        public static void CreateNewBooleanTrigger(this DependencyObject target,
                                                   bool receviesValue,
                                                   PropertyPath inProperty,
                                                   RoutedEvent @whenEvent,
                                                   FrameworkElement inElement)
        {
            BooleanAnimationUsingKeyFrames booleanAnimation = new()
            {
                BeginTime = TimeSpan.Zero,
                Duration = TimeSpan.Zero,
            };

            booleanAnimation.
                KeyFrames.Add(
                new DiscreteBooleanKeyFrame() { Value = receviesValue, KeyTime = TimeSpan.Zero });
            Storyboard storyboard = new();
            storyboard.Children.Add(booleanAnimation);
            Storyboard.SetTarget(storyboard, target);
            Storyboard.SetTargetProperty(storyboard, inProperty);

            EventTrigger eventTrigger = new();
            eventTrigger.RoutedEvent = @whenEvent;
            eventTrigger.Actions.Add(new BeginStoryboard() { Storyboard = storyboard });
            
            inElement.Triggers.Add(eventTrigger);
        }
    }

}
