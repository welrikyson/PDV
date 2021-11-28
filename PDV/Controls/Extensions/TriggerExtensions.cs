using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace PDV.Controls.Extensions
{
    public static class TriggerExtensions

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
