using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PDV.Views;
using System;
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
                    services.AddSingleton<ViewModelLocator>();
                    services.AddSingleton<MainWindow>();
                });


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _host.Start();

            var serviceProvider = _host.Services;
            if (Resources["ViewModelLocator"] is ViewModelLocator viewModelLocator)
            {
                viewModelLocator.ServiceProvider = serviceProvider;
            }

            serviceProvider.GetRequiredService<MainWindow>().Show();
        }
    }
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<ViewModels.Navigator>()
                        .AddTransient<Commands.Cancel>()
                        .AddTransient<Commands.ProductListFinalize>()
                        .AddTransient<ViewModels.ProductListManager>()
                        .AddTransient<ViewModels.DialogService>()
                        .AddSingleton(n =>
                        {
                            var productListManager = n.GetRequiredService<ViewModels.ProductListManager>();
                            return new ViewModels.NavegableFactory()
                            {
                                SaleInfo = new ViewModels.SaleInfo(productListManager),
                                ProductListManager = productListManager,
                            };
                        })
                        .AddSingleton((serviceProvider) =>
                        {
                            var navigator = serviceProvider.GetService<ViewModels.Navigator>();
                            var dialogService = serviceProvider.GetService<ViewModels.DialogService>();
                            return new ViewModels.Main(navigator, dialogService);
                        });

            });
    }
}