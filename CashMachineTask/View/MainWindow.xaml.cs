using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace CashMachineTask.View
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public decimal PreferDenomination
		{
			get => (decimal)GetValue(PreferDenominationProperty);
			set => SetValue(PreferDenominationProperty, value);
		}

		public static readonly DependencyProperty PreferDenominationProperty =
			DependencyProperty.Register(nameof(PreferDenomination),
				typeof(decimal),
				typeof(MainWindow),
				new PropertyMetadata(default(decimal)));

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Test_Click(object sender, RoutedEventArgs e)
		{



		}

		private void WithdrawalButton_Click(object sender, RoutedEventArgs e)
		{
			var selector = new SelectorCashDialog();
			selector.Owner = this;
			var dec = decimal.Parse(InputTextBox.Text);
			var denom = DenomiantionListView.ItemsSource;

			selector.WithdrawalSum = dec;
			selector.Denominations = (decimal[])denom;

			if (selector.ShowDialog() == true)
			{
				PreferDenomination = selector.SelectedDenomination;
			}
			else
			{
				PreferDenomination = 0;
			}
		}
	}
}