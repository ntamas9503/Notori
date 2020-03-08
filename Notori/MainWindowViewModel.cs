using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Notori
{
    public class MainWindowViewModel : Bindable
    {
        private ObservableCollection<Note> notes;
        private Settings settings;
        private bool isNoteShowing;
        private bool isTodosShowing;
        private Note selectedNote;
        private bool doNotNeedRestart;
        private bool isDarkMode;

        public ObservableCollection<Note> Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged();
            }
        }

        public Settings Settings
        {
            get => settings;
            set
            {
                settings = value;
                OnPropertyChanged();
            }
        }

        public Note SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                OnPropertyChanged();
            }
        }

        public bool IsStartingApplication { get; set; }

        public bool DoNotNeedRestart
        {
            get => doNotNeedRestart;
            set
            {
                doNotNeedRestart = value;
                OnPropertyChanged();
            }
        }

        public bool IsDarkMode
        {
            get => isDarkMode;
            set
            {
                isDarkMode = value;
                OnPropertyChanged();
            }
        }

        public bool IsNoteTakerShowing { get; set; }

        private static MainWindowViewModel VM = null;
        public static MainWindowViewModel Get
        {
            get
            {
                if (VM == null)
                    VM = new MainWindowViewModel();
                return VM;
            }
        }

        public MainWindowViewModel()
        {
            this.notes = new ObservableCollection<Note>();
            this.Settings = DbManager.GetSettings();
            this.IsDarkMode = Settings.IsDarkMode;
            IsStartingApplication = true;
            DoNotNeedRestart = true;
        }

        public void LoadNotesInList()
        {
            var notes = DbManager.GetNotes();
            ConvertNoteListToObservable(notes);
            isNoteShowing = true;
            isTodosShowing = false;
        }

        public void LoadTodosInList()
        {
            var todos = DbManager.GetTodos();
            ConvertNoteListToObservable(todos);
            isNoteShowing = false;
            isTodosShowing = true;
        }

        public void RefreshList()
        {
            if (isNoteShowing)
            {
                LoadNotesInList();
            }
            else
            {
                LoadTodosInList();
            }
        }

        public void UpdateSettings()
        {
            DbManager.UpdateSettings(IsDarkMode);
            if (!IsStartingApplication)
            {
                DoNotNeedRestart = false;
            }
        }

        public void DeleteNote()
        {
            if (SelectedNote != null)
            {
                if (SelectedNote.IsTodo)
                {
                    DbManager.DeleteTodo(SelectedNote.Id);
                }
                else
                {
                    DbManager.DeleteNote(SelectedNote.Id);
                }
            }
        }

        public void DeleteAll()
        {
            if (Notes.Any())
            {
                if (isNoteShowing)
                {
                    DbManager.DropNotes();
                }
                else
                {
                    DbManager.DropTodos();
                }
            }
        }

        private void ConvertNoteListToObservable(List<Note> notes)
        {
            Notes.Clear();
            var orderedList = notes.OrderByDescending(x => x.Timestamp);
            foreach (var note in orderedList)
            {
                Notes.Add(note);
            }
        }
    }
}
