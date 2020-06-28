using System;
using System.Collections.Generic;

namespace Infustructure.Models
{
    public partial class PhoneBook
    {
        public PhoneBook()
        {
            Entry = new HashSet<Entry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Entry> Entry { get; set; }
    }
}
