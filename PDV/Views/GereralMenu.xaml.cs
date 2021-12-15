using PDV.Components.UserControls;
using PDV.Interfaces;
using PDV.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            first.Focusable = true;
        }

        Option Option { get => this.first; }
        
    }
}
