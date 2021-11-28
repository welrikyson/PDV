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
        private readonly IHost _host;
        public App()
        {
            var hostbuilder = ConfigureHostBuilder();
            _host = hostbuilder.Build();
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
            _host.Start();

            var serviceProvider = _host.Services;

            serviceProvider.GetRequiredService<MainWindow>().Show();
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