using System;
using System.Collections.Generic;

namespace ChatBot.Data.Models
{
    public partial class User
    {
        public User()
        {
            Message = new HashSet<Message>();
            UserChatRoom = new HashSet<UserChatRoom>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }
        public ICollection<Message> Message { get; set; }
        public ICollection<UserChatRoom> UserChatRoom { get; set; }
    }
}
