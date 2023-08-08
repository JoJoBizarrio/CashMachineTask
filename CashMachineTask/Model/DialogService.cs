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

        public void ShowDialog<TViewModel>(IDialogParametrs dialogParametrs, Action<bool?, object> callback)
        {
            var viewType = _mapping[typeof(TViewModel)];
            var item = Activator.CreateInstance(viewType);

            if (item is Window dialog)
            {
                var dialogViewModel = (IDialogViewModel)Activator.CreateInstance(typeof(TViewModel));
                dialog.DataContext = dialogViewModel;
                dialog.Owner = dialogParametrs.GetValue<Window>("Owner");

                dialogViewModel.OnDialogOpened(dialogParametrs);

                EventHandler onClosed = null;
                onClosed = (s, e) =>
                {
                    callback(dialog.DialogResult, dialogViewModel.Result);
                    dialog.Closed -= onClosed;
                };

                dialog.Closed += onClosed;
                dialog.ShowDialog();
            }
        }

        public void ShowDialog(IDialogViewModel viewModel, Action<bool?, object> callback)
        {
            var viewType = _mapping[viewModel.GetType()];
            var item = Activator.CreateInstance(viewType);

            if (item is Window dialog)
            {
                dialog.DataContext = viewModel;
                dialog.Owner = viewModel.Owner;

                EventHandler onClosed = null;
                onClosed = (s, e) =>
                {
                    callback(dialog.DialogResult, viewModel.Result);
                    dialog.Closed -= onClosed;
                };

                dialog.Closed += onClosed;
                dialog.ShowDialog();
            }
        }
    }
}