using System;
using System.Collections.Generic;

namespace ChatBot.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> User { get; set; }
    }
}
