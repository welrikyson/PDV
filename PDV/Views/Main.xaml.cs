using PDV.Controls.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PDV.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
            Dialog.Style = CreatNewDialogStyle(Dialog.Style);
            Dialog.CreateNewBooleanTrigger(receviesValue: true,
                                           inProperty: new(Controls.Dialog.IsOpenProperty),
                                           whenEvent: Controls.MainGrid.MKeyDownEvent,
                                           inElement: MainGrid);

            Dialog.CreateNewBooleanTrigger(receviesValue: false,
                                           inProperty: new(Controls.Dialog.IsOpenProperty),
                                           whenEvent: ButtonBase.ClickEvent,
                                           CloseDialogButton);

            content.CreateNewBooleanTrigger(receviesValue: false,
                                            inProperty: new(IsEnabledProperty),
                                            whenEvent: Controls.Dialog.OpenedEvent,
                                            inElement: Dialog);
            content.CreateNewBooleanTrigger(receviesValue: true,
                                            inProperty: new(IsEnabledProperty),
                                            whenEvent: Controls.Dialog.ClosedEvent,
                                            inElement: Dialog);
            content.IsEnabledChanged += ContentIsEnableHandler;
        }

        private DependencyPropertyChangedEventHandler ContentIsEnableHandler
        {
            get => (s, e) =>
            {
                if (e.NewValue.Equals(true))
                {
                    MainGrid.SetFocus();
                }
            };
        }

        #region DialogStyle
        private Style CreatNewDialogStyle(Style from)
        {
            Style dialogStyle = new(typeof(Controls.Dialog), from);
            var trigger = new Trigger()
            {
                Property = Controls.Dialog.IsOpenProperty,
                Value = true,
            };

            trigger.Setters.Add(new Setter()
            {
                Property = FocusManager.FocusedElementProperty,
                Value = Dialog.Content,
            });
            dialogStyle.Triggers.Add(trigger);

            return dialogStyle;
        }
        #endregion
    }
}
