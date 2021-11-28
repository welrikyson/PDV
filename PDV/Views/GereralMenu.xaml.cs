using System.Windows;
using System.Windows.Controls;

namespace PDV.Views
{
    /// <summary>
    /// Interaction logic for GereralMenu.xaml
    /// </summary>
    public partial class GereralMenu : UserControl
    {
        public GereralMenu()
        {
            InitializeComponent();
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(GereralMenu), new PropertyMetadata(false));

    }
}
