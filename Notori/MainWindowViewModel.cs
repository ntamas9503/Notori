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
        
        public MainWindowViewModel(Settings settings)
        {
            this.notes = new ObservableCollection<Note>();
            this.Settings = settings;
            IsStartingApplication = true;
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
            DbManager.UpdateSettings(Settings);
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
            if (isNoteShowing)
            {
                DbManager.DropNotes();
            }
            else
            {
                DbManager.DropTodos();
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
