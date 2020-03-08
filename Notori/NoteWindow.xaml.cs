using System.Windows;
using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;

namespace Notori
{
    /// <summary>
    /// Interaction logic for NoteWindow.xaml
    /// </summary>
    public partial class NoteWindow : Window
    {
        private NoteWindowViewModel viewModel;
        public NoteWindow(Settings settings)
        {
            InitializeComponent();
            HotkeyManager.Current.AddOrReplace("SaveNote", Key.S, ModifierKeys.Alt, SaveNote);
            this.viewModel = new NoteWindowViewModel(settings);
            this.DataContext = this.viewModel;
        }

        private void SaveNote(object sender, HotkeyEventArgs e)
        {
            if (MainWindow.IsWindowInstantiated<NoteWindow>())
            {
                viewModel.SaveNote();
                Close();
            }
        }
    }
}
