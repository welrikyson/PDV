using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PDV.Views;
using System.Windows;

namespace PDV
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IHost _host = ConfigureHostBuilder().Build();
        public App()
        {
            _host.Start();
        }

        private static IHostBuilder ConfigureHostBuilder() =>
            Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                });

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);         

            var serviceProvider = _host.Services;

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = new MainViewModel();
            mainWindow.Show();

        }
    }


    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureServices(services =>
            {

            });
    }

}