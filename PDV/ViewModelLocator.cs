using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PDV
{
    public class ViewModelLocator
    {
        public IServiceProvider ServiceProvider { get; internal set; }
    }
}
