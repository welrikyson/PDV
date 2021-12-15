using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDV.Components.UserControls
{
    /// <summary>
    /// Interaction logic for Option.xaml
    /// </summary>
    public partial class Option : UserControl
    {
        public Option()
        {
            InitializeComponent();
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            btn.Focus();
        }

    }
}
