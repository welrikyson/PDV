using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvvmDialogs;
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
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<ViewModels.Main>()
                    });
                    services.AddSingleton<IDialogService>(s => new DialogService( dialogTypeLocator: new DialogTypeLocator()));
                })
                .Build();
        }        

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            _host.Services.GetRequiredService<MainWindow>().Show();            
            base.OnStartup(e);

        }
    }
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<ViewModels.Navigator>();
                services.AddTransient<Commands.Cancel>();
                services.AddTransient<Commands.ProductListFinalize>();

                services.AddTransient<ViewModels.ProductListManager>();

                services.AddSingleton<ViewModels.NavegableFactory>(n =>
                {
                    var productListManager = n.GetRequiredService<ViewModels.ProductListManager>();
                    return new ViewModels.NavegableFactory()
                    {
                        SaleInfo = new ViewModels.SaleInfo(productListManager),
                        ProductListManager = productListManager,
                    };
                });
                services.AddSingleton<ViewModels.Main>();

            });

            return hostBuilder;
        }

        //private static ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider services)
        //{
        //    return ReservationListingViewModel.LoadViewModel(
        //        services.GetRequiredService<HotelStore>(),
        //        services.GetRequiredService<NavigationService<MakeReservationViewModel>>());
        //}
    }
}