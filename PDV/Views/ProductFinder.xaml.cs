using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reactive;
using System.Reactive.Linq;
using PDV.Controls;

namespace PDV.Views
{
    /// <summary>
    /// Interaction logic for ProductFinder.xaml
    /// </summary>
    public partial class ProductFinder : UserControl
    {        
        public ProductFinder()
        {
            InitializeComponent();            
        }

        public Search Search { get => search; }
    }
}
