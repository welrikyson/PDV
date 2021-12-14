using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PDV.Interfaces;
using PDV.Models;
using PDV.Ultis.Moc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reactive;
using System.Reactive.Linq;
using PDV.Services;
using System.Threading;

namespace PDV.ViewModels
{
    public class ProductFinder : ObservableObject, IDialog<Product>
    {
        public event EventResult<Product>? EventResult;
        public List<Product> Products { get; set; }

        public ICommand Confirm { get; }
        public IAsyncRelayCommand Find { get; set; }
        public ProductService ProductService { get; }

        public ProductFinder()
        {
            Products = new();
            Confirm = new RelayCommand<Product>(ConfirmHandler);
            Find = new AsyncRelayCommand<string>(FindHandlerTaskAsync);
            ProductService = new ProductService(new ProductRepository());
        }

        private async Task FindHandlerTaskAsync(string? arg)
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource = new CancellationTokenSource();
            await FindHandlerAsync(arg, CancellationTokenSource.Token);

        }

        private CancellationTokenSource? CancellationTokenSource;
        private async Task FindHandlerAsync(string? searchTerm, CancellationToken cancellationToken)
        {
            TaskCompletionSource tcs = new();

            if (string.IsNullOrEmpty(searchTerm) || searchTerm.Length < 3)
            {
                if (Products.Count > 0)
                {
                    Products = Enumerable.Empty<Product>().ToList();
                    OnPropertyChanged(nameof(Products));
                }
            }
            else
            {                
                var result = await ProductService.GetProducts(searchTerm,cancellationToken);
                Products = result;
                OnPropertyChanged(nameof(Products));
            }
        }

        private void ConfirmHandler(Product? product)
        {
            EventResult?.Invoke(product);
        }
    }
}
