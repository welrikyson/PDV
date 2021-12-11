using Microsoft.Toolkit.Mvvm.ComponentModel;
using PDV.Interfaces;
using PDV.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PDV.ViewModels
{
    public class Cart : ObservableObject, INavegable
    {
        public ObservableCollection<ChartItem> CartItems { get; private set; } = new ObservableCollection<ChartItem>();
        public bool ShouldExecutePreset { set; get; }

        public Cart(List<ChartItem>? chartItems = null)
        {
            chartItems?.ForEach(AddItem);
        }

        public void AddItem(ChartItem chartItem)
        {
            CartItems.Add(chartItem);
            OnItemAdd(chartItem);
        }

        private void OnItemAdd(ChartItem CartItemAdded)
        {
            AddOnTotalAndNotify(CartItemAdded.Total);
            CartItemAdded.TotalChange += OnCartItemTotalChanged;
        }

        private void OnCartItemTotalChanged(decimal oldValue, decimal newValue)
        {
            Total -= oldValue;
            AddOnTotalAndNotify(newValue);
        }

        private void AddOnTotalAndNotify(decimal value)
        {
            Total += value;
            OnPropertyChanged(nameof(Total));
        }

        public decimal Total { get; private set; } = 0;

    }
}