using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Data
{
    public class Language
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}
