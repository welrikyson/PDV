using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDV.Controls
{
    [TemplatePart(Name = Input, Type = typeof(TextBox))]
    [TemplatePart(Name = SearchResult, Type = typeof(ListBox))]
    public class Search : Control
    {
        private const string Input = "PART_Input";
        private const string SearchResult = "PART_SearchResult";
        private TextBox? _input;
        private ListBox? _searchResult;

        public ICommand Confirm
        {
            get { return (ICommand)GetValue(ConfirmProperty); }
            set { SetValue(ConfirmProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Confirm.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConfirmProperty =
            DependencyProperty.Register("Confirm", typeof(ICommand), typeof(Search), new PropertyMetadata());


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public IEnumerable Results
        {
            get { return (IEnumerable)GetValue(ResultsProperty); }
            set { SetValue(ResultsProperty, value); }
        }


        public DataTemplate ResultItemTemplate
        {
            get { return (DataTemplate)GetValue(ResultItemTemplateProperty); }
            set { SetValue(ResultItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResultItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultItemTemplateProperty =
            DependencyProperty.Register("ResultItemTemplate", typeof(DataTemplate), typeof(Search), new PropertyMetadata());


        // Using a DependencyProperty as the backing store for Results.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultsProperty =
            DependencyProperty.Register("Results", typeof(IEnumerable), typeof(Search), new PropertyMetadata(default, OnResultsChanged));

        private static void OnResultsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Search)d).OnResultsChanged();
        }

        private void OnResultsChanged()
        {
            if (_searchResult != null)
            {
                _searchResult.SelectedIndex = 0;
            }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(Search), new PropertyMetadata());


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _input = GetTemplateChild(Input) as TextBox;
            if (_input != null)
            {
                _input.TextChanged += OnInputTextChangedHandler;
                _input.KeyUp += OnInputKeyPressHandler;
            }

            _searchResult = GetTemplateChild(SearchResult) as ListBox;
            if (_searchResult != null)
            {
                _searchResult.Focusable = false;
                _searchResult.GotFocus += (s, e) =>
                {
                    _input?.Focus();
                };
            }
        }

        private void OnInputKeyPressHandler(object sender, KeyEventArgs e)
        {
            if (_searchResult == null) return;
            if (e.Key == Key.Down)
            {
                if (_searchResult.Items.Count > 1)
                {
                    if (_searchResult.SelectedIndex != _searchResult.Items.Count)
                    {
                        _searchResult.SelectedIndex++;
                        _searchResult.ScrollIntoView(_searchResult.SelectedItem);
                    }
                }
            }
            if (e.Key == Key.Up)
            {
                if (_searchResult.Items.Count > 1)
                {
                    if (_searchResult.SelectedIndex != 0)
                    {
                        _searchResult.SelectedIndex--;
                        _searchResult.ScrollIntoView(_searchResult.SelectedItem);
                    }
                }
            }

            if(e.Key == Key.Enter)
            {
                if(Confirm != null)
                {
                    if (Confirm.CanExecute(_searchResult.SelectedItem))
                    {
                        Confirm.Execute(_searchResult.SelectedItem);
                    }
                }                
            }
        }

        private void OnInputTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            if (_input == null || Command == null) return;
            if (Command.CanExecute(_input.Text))
            {
                Command.Execute(_input.Text);
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if (_input != null)
            {
                _input?.Focus();
                _input?.Select(_input.Text.Length, 0);
            }
        }
    }

    public interface ISearchResultSource
    {
        IEnumerable Searcher(string term);
    }
}
