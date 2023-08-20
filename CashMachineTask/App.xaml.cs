using CashMachineTask.Model;
using CashMachineTask.View;
using CashMachineTask.ViewModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace CashMachineTask
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		// logger dosnt work. why?
		private static Logger _logger = LogManager.GetCurrentClassLogger();

		protected override void OnStartup(StartupEventArgs e)
		{
			SetupExceptionHandling();

			MainWindow mainWindow = new MainWindow();

			var cassettesList = new List<Cassette>() { new Cassette(200, 2, 200), new Cassette(500, 5, 3000) };
			var cashMachine = new CashMachine(cassettesList);

			var dialogService = new DialogService();
            DialogService.RegisterDialog<SelectorCashDialogViewModel, SelectorCashModalDialog>();
            
			mainWindow.DataContext = new MainWindowViewModel(cashMachine, dialogService);
			mainWindow.Show();
		}

		private void SetupExceptionHandling()
		{
			AppDomain.CurrentDomain.UnhandledException += (s, e) =>
				LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

			DispatcherUnhandledException += (s, e) =>
			{
				LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
				e.Handled = true;
			};

			TaskScheduler.UnobservedTaskException += (s, e) =>
			{
				LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
				e.SetObserved();
			};
		}

		private void LogUnhandledException(Exception exception, string source)
		{
			string message = $"Unhandled exception ({source})";

			try
			{
				System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
				message = string.Format("Unhandled exception in {0} v{1}", assemblyName.Name, assemblyName.Version);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Exception in LogUnhandledException");
			}
			finally
			{
				_logger.Error(exception, message);
			}
		}
	}
}