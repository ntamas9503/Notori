using System;
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
        private MainWindowViewModel viewModel;
        private Settings settings;

        public MainWindow()
        {
            InitializeComponent();
            HotkeyManager.Current.AddOrReplace("LaunchNoteWindow", Key.N, ModifierKeys.Alt, LaunchNoteWindow);
            this.settings = DbManager.GetSettings();
            viewModel = new MainWindowViewModel(settings);
            this.DataContext = viewModel;
        }

        private void LaunchNoteWindow(object sender, HotkeyEventArgs e)
        {
            NoteWindow note = new NoteWindow(settings);
            note.ShowDialog();
        }

        private void OnNotesClicked(object sender, RoutedEventArgs e)
        {
            viewModel.LoadNotesInList();
        }

        private void OnTodosClicked(object sender, RoutedEventArgs e)
        {
            viewModel.LoadTodosInList();
        }

        private void OnExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnCheckedChange(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateSettings();
            if (!viewModel.IsStartingApplication)
            {
                ShowChangedMessageBox("on");
            }

            viewModel.IsStartingApplication = false;
        }

        private void OnUncheckedChange(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateSettings();
            if (!viewModel.IsStartingApplication)
            {
                ShowChangedMessageBox("off");
            }

            viewModel.IsStartingApplication = false;
        }

        private void OnActiveRefresh(object sender, EventArgs e)
        {
            viewModel.RefreshList();
        }

        private void ShowChangedMessageBox(string onoff)
        {
            MessageBox.Show($"Please restart the application to set dark mode {onoff}!", "Restart required",
                MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteNote();
            viewModel.RefreshList();
        }

        private void OnDeleteAllClicked(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteAll();
            viewModel.RefreshList();
        }
    }
}
