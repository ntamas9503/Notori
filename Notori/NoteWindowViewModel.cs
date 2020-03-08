using System;

namespace Notori
{
    public class NoteWindowViewModel : Bindable
    {
        private string note;
        private Settings settings;
        
        public string Note
        {
            get => note;
            set
            {
                note = value;
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

        public NoteWindowViewModel(Settings settings)
        {
            this.settings = settings;
        }

        public void SaveNote()
        {
            if (!string.IsNullOrEmpty(Note))
            {
                if (Note.StartsWith("TODO:", StringComparison.InvariantCultureIgnoreCase))
                {
                    Note newTodo = new Note
                    {
                        NoteText = Note,
                        Timestamp = DateTime.Now,
                        IsTodo = true
                    };
                    DbManager.AddTodo(newTodo);
                }
                else
                {
                    Note newNote = new Note
                    {
                        NoteText =  Note,
                        Timestamp = DateTime.Now,
                        IsTodo = false
                    };
                    DbManager.AddNote(newNote);
                }
            }
        }
    }
}
