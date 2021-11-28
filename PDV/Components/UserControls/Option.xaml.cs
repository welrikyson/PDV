using System.Windows;
using System.Windows.Controls;

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

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (e.OriginalSource is Option opt)
            {
                opt.btn.Focus();
            }
        }
    }
}
