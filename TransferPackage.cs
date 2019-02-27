using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesCrossPlt
{
    public class TransferPackage
    {
        public int packageId { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
        public int userId { get; set; }
        public List<Note> notes = new List<Note>();
    }
}
