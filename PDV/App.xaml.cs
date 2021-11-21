using PDV.Views;
using System.Windows;

namespace PDV
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static Window StartUpWindow => new MainWindow();
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StartUpWindow.Show();
        }
    }
}