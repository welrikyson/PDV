namespace PDV.Views
{
    /// <summary>
    ///     Interaction logic for SaleInfo.xaml
    /// </summary>
    public partial class SaleInfo
    {
        public SaleInfo()
        {
            InitializeComponent();
            Loaded += (_, _) => { AnyTextBox.Focus(); };
        }
    }
}