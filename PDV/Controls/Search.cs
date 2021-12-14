using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PDV.Controls
{
    [TemplatePart(Name = Input, Type = typeof(TextBox))]
    [TemplatePart(Name = SearchResult, Type = typeof(ItemsControl))]
    public class Search : Control
    {
        private const string Input = "PART_Input";
        private const string SearchResult = "PART_SearchResult";
        private TextBox? _input;
        private ItemsControl? _searchResult;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _input  = GetTemplateChild(Input) as TextBox;            
            _searchResult = GetTemplateChild(Input) as ItemsControl;
        }
    }
}
