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

namespace PDV.Controls
{
    /// <summary>
    /// Interaction logic for Numeric.xaml
    /// </summary>
    public partial class Numeric : UserControl
    {
        public Numeric()
        {
            InitializeComponent();
            InputTxtb.PreviewTextInput += PreviewTextInputHandler;
        }

        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            
        }

        public int MinValue { get; set; }
        public int Maxvalue { get; set; }

        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(Numeric), new PropertyMetadata());



        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(Numeric), new PropertyMetadata(TextAlignment.Left));


    }
}
