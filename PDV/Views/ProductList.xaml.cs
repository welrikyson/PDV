using PDV.Interfaces;
using System;
using System.Windows.Input;

namespace PDV.Views
{
    /// <summary>
    ///     Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductListManager : IHavePresets
    {
        public ProductListManager()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(
                GoToSearchTermField,
                OnGotoSearchTermFieldExecuted));
        }

        private void OnGotoSearchTermFieldExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SearchTermTxb.Focus();
        }

        public void ApplyPresets()
        {
            SearchTermTxb.Focus();
        }



        public static RoutedUICommand GoToSearchTermField = new RoutedUICommand();

    }
}