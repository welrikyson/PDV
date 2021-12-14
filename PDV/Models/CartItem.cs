using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PDV.Models
{
    public class CartItem : ObservableObject
    {
        public CartItem(Product product, int count)
        {
            Product = product;
            Count = count;
            Total = CalculeteTotal(count);
        }

        private decimal CalculeteTotal(int count)
        {
            return Product.Price * count;
        }

        public Product Product { get; }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                if (_count == value)
                {
                    return;
                }

                _count = value;
                SetTotal(CalculeteTotal(_count));
                this.OnPropertyChanged(nameof(Count));
                this.OnPropertyChanged(nameof(Total));
            }
        }

        private void SetTotal(decimal newValue)
        {
            var oldValue = Total;
            Total = newValue;
            TotalChange?.Invoke(oldValue, newValue);
        }

        public decimal Total { get; private set; }
        public event TotalChange? TotalChange;
    }
    public delegate void TotalChange(decimal oldValue, decimal newValue);

}
