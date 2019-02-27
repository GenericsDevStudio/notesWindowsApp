using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesCrossPlt
{
    public class Note
    {
        public int noteid { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime lastChange { get; set; }

        public override string ToString()
        {
            return title;
        }
    }
}
