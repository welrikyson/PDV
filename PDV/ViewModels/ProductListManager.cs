using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PDV.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using PDV.Ultis.Moc;

namespace PDV.ViewModels
{
    public class ProductListManager : ObservableObject, INavegable
    {
        private Action? _onSearchTermChange;

        public ProductListManager()
        {
            _onSearchTermChange += OnSearchTermChangedFirstTime;            
        }        
        public IEnumerable<ICommand> ProductListManagerCommands { get; set; } = Enumerable.Empty<ICommand>();
                
        private void OnSearchTermChangedFirstTime()
        {
            Current = new ProductListCart(Mock.CartItems);
            OnPropertyChanged(nameof(Current));
            OnPropertyChanged(nameof(ProductListManagerCommands));
            _onSearchTermChange -=
                OnSearchTermChangedFirstTime;
        }

        #region Properties

        private string _searchTerm = string.Empty;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {                
                _onSearchTermChange?.Invoke();
                SetProperty(ref _searchTerm, value);
            }
        }

        private void OnSearchTermChanged(string obj)
        {
            throw new NotImplementedException();
        }

        private bool _shouldExecutePreset;

        public bool ShouldExecutePreset
        {
            get => _shouldExecutePreset;
            set => SetProperty(ref _shouldExecutePreset, value);
        }

        public object Current { get; set; } = new ProductListHome();

        #endregion
    }

    public static class Mock
    {
        private static readonly (decimal price, string name, int count)[] TupleCartItemArray =
            new (decimal price, string name, int count)[] {
                (10.00m,"Coca-Cola",2),
                (98.00m,"Picanha",1),
                (16.00m,"Carvão",1),
                (22.90m,"Pão de alho",2),
                (16.00m,"Carvão",1), };

        public static List<Models.ChartItem> CartItems
        =>
            (from c in TupleCartItemArray
             let product = new Models.Product(IncrementalId.Next, c.name, c.price)
             select new Models.ChartItem(product, c.count)).ToList();
        
    }
}