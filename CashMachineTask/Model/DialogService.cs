using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CashMachineTask.Model
{
    class DialogService : IDialogService
    {
        private static readonly Dictionary<Type, Type> _mapping = new Dictionary<Type, Type>();

        public static void RegisterDialog<TViewModel, TView>() where TViewModel : IDialogViewModel where TView : Window
        {
            _mapping.Add(typeof(TViewModel), typeof(TView));
        }

        public void ShowDialog<TResult>(IDialogViewModel viewModel, Action<bool?, TResult> callback)
        {
            var viewType = _mapping[viewModel.GetType()];
            var item = Activator.CreateInstance(viewType);

            if (item is Window dialog)
            {
                dialog.DataContext = viewModel;
                dialog.Owner = Application.Current.MainWindow;

                EventHandler onClosed = null;
                onClosed = (s, e) =>
                {
                    callback(dialog.DialogResult, (TResult)viewModel.Result);
                    dialog.Closed -= onClosed;
                };

                dialog.Closed += onClosed;
                dialog.ShowDialog();
            }
        }
    }
}