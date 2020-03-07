using System;
using System.Globalization;

namespace Notori
{
    public class Note
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool IsTodo { get; set; }

        public string LocalizedTime
        {
            get
            {
                var currentCulture = CultureInfo.CurrentCulture;
                return this.Timestamp?.ToString("f", currentCulture);
            }
        }
    }
}
