using System.Windows;

namespace CashMachineTask.View
{
    /// <summary>
    /// Логика взаимодействия для SelectorCashModalDialog.xaml
    /// </summary>
    public partial class SelectorCashModalDialog : Window
    {
        public SelectorCashModalDialog()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
