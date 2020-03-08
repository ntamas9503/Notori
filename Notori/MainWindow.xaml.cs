using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;

namespace Notori
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HotkeyManager.Current.AddOrReplace("LaunchNoteWindow", Key.N, ModifierKeys.Alt, LaunchNoteWindow);
            this.DataContext = MainWindowViewModel.Get;
        }

        private void LaunchNoteWindow(object sender, HotkeyEventArgs e)
        {
            if (IsWindowInstantiated<NoteWindow>())
                return;

            NoteWindow note = new NoteWindow(MainWindowViewModel.Get.Settings);
            note.ShowDialog();
        }

        private void OnNotesClicked(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.Get.LoadNotesInList();
        }

        private void OnTodosClicked(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.Get.LoadTodosInList();
        }

        private void OnExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnCheckedChange(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.Get.UpdateSettings();
            if (!MainWindowViewModel.Get.IsStartingApplication)
            {
                ShowChangedMessageBox("on");
            }

            MainWindowViewModel.Get.IsStartingApplication = false;
        }

        private void OnUncheckedChange(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.Get.UpdateSettings();
            if (!MainWindowViewModel.Get.IsStartingApplication)
            {
                ShowChangedMessageBox("off");
            }

            MainWindowViewModel.Get.IsStartingApplication = false;
        }

        private void OnActiveRefresh(object sender, EventArgs e)
        {
            MainWindowViewModel.Get.RefreshList();
        }

        private void ShowChangedMessageBox(string onoff)
        {
            MessageBox.Show($"Please restart the application to set dark mode {onoff}!", "Restart required",
                MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.Get.DeleteNote();
            MainWindowViewModel.Get.RefreshList();
        }

        private void OnDeleteAllClicked(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.Get.DeleteAll();
            MainWindowViewModel.Get.RefreshList();
        }

        public static bool IsWindowInstantiated<T>() where T : Window
        {
            var windows = Application.Current.Windows.Cast<Window>();
            var any = windows.Any(s => s is T);
            return any;
        }
    }
}
