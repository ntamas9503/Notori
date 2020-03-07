namespace Notori
{
    public class Settings : Bindable
    {
        public int Id { get; set; }
        public bool isDarkMode;
        public bool IsDarkMode 
        {
            get => isDarkMode;
            set
            {
                isDarkMode = value;
                OnPropertyChanged();
            }
        }

        public string Background => IsDarkMode ? "Black" : "White";
        public string TextColor => IsDarkMode ? "White" : "Black";
    }
}
