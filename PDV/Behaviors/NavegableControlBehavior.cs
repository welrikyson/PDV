using System.Windows;
using PDV.Interfaces;

namespace PDV.Behaviors
{
    public class NavegableControlBehavior
    {
        // Using a DependencyProperty as the backing store for SholdInitialize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SholdInitializeProperty =
            DependencyProperty.RegisterAttached("SholdInitialize", typeof(bool), typeof(NavegableControlBehavior),
                new PropertyMetadata(OnShouldInitializeChanged));

        public static bool GetSholdInitialize(DependencyObject obj)
        {
            return (bool)obj.GetValue(SholdInitializeProperty);
        }

        public static void SetSholdInitialize(DependencyObject obj, bool value)
        {
            obj.SetValue(SholdInitializeProperty, value);
        }

        private static void OnShouldInitializeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;
            if (d is IHavePresets control)
                if ((bool)e.NewValue)
                {
                    control.ApplyPresets();
                    SetSholdInitialize(d, false);
                }
        }
    }
}