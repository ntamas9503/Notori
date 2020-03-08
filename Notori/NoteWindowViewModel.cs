using System;

namespace Notori
{
    public class NoteWindowViewModel : Bindable
    {
        private string note;
        private Settings settings;
        private string remainingCharacters;
        
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

        public string RemainingCharacters
        {
            get => remainingCharacters;
            set
            {
                remainingCharacters = value;
                OnPropertyChanged();
            }
        }

        public int MaxCharacters => 300;

        public NoteWindowViewModel(Settings settings)
        {
            this.settings = settings;
            RemainingCharacters = $"0/{MaxCharacters.ToString()}";
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

        public void ShowRemainingCharacters()
        {
            RemainingCharacters = $"{Note?.Length}/{MaxCharacters.ToString()}";
        }
    }
}
