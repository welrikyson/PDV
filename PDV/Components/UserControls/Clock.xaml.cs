using System.Windows.Controls;

namespace PDV.Components.UserControls
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        public Clock()
        {
            InitializeComponent();
            DataContext = new Components.ViewModels.Clock();
        }
    }
}
