using System;
using System.Collections.Generic;

namespace Infustructure.Models
{
    public partial class Entry
    {
        public int EntryId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneBookId { get; set; }

        public virtual PhoneBook PhoneBook { get; set; }
    }
}
