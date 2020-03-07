using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace Notori
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private bool isExit;
        private Mutex instanceMutex = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            instanceMutex = new Mutex(true, @"Notori", out var createdNew);
            if (!createdNew)
            {
                instanceMutex = null;
                Application.Current.Shutdown();
                return;
            }
            base.OnStartup(e);
            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
            notifyIcon.Icon = Notori.Properties.Resources.Notori;
            notifyIcon.Visible = true;

            CreateContextMenu();
        }

        private void CreateContextMenu()
        {
            notifyIcon.ContextMenuStrip =
              new System.Windows.Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Manage notes").Click += (s, e) => ShowMainWindow();
            notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        private void ExitApplication()
        {
            isExit = true;
            MainWindow?.Close();
            notifyIcon.Dispose();
            notifyIcon = null;
        }

        private void ShowMainWindow()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!isExit)
            {
                e.Cancel = true;
                MainWindow?.Hide(); // A hidden window can be shown again, a closed one not
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            instanceMutex?.ReleaseMutex();
            notifyIcon?.Dispose();
            base.OnExit(e);
        }
    }
}
