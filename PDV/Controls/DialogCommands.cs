using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDV.Controls
{
    public static class DialogCommands
    {
        public static RoutedUICommand Close { get; } = new RoutedUICommand();
    }
}
