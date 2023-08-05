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
using System.Windows.Shapes;

namespace CashMachineTask.View
{
	/// <summary>
	/// Логика взаимодействия для SlectorCashDilalog.xaml
	/// </summary>
	public partial class SelectorCashDialog
	{
		public decimal[] Denominations
		{
			get => (decimal[])GetValue(DenominationsProperty);
			set => SetValue(DenominationsProperty, value);
		}

		public static readonly DependencyProperty DenominationsProperty = DependencyProperty.Register(nameof(Denominations),
																									  typeof(decimal[]),
																									  typeof(SelectorCashDialog),
																									  new PropertyMetadata(default(decimal[])));

		public decimal WithdrawalSum
		{
			get => (decimal)GetValue(WithdrawalSumProperty);
			set => SetValue(WithdrawalSumProperty, value);
		}

		public static readonly DependencyProperty WithdrawalSumProperty = DependencyProperty.Register(nameof(WithdrawalSum),
																									  typeof(decimal),
																									  typeof(SelectorCashDialog),
																									  new PropertyMetadata(default(decimal)));

		public decimal SelectedDenomination
		{
			get => (decimal)GetValue(SelectedDenominationProperty);
			set => SetValue(SelectedDenominationProperty, value);
		}

		private static readonly DependencyProperty SelectedDenominationProperty = DependencyProperty.Register(nameof(SelectedDenominationProperty),
																									   typeof(decimal),
																									   typeof(MainWindow),
																									   new PropertyMetadata(default(decimal)));

		public SelectorCashDialog()
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
