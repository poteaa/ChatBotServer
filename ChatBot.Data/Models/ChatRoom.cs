using System;
using System.Collections.Generic;

namespace ChatBot.Data.Models
{
    public partial class ChatRoom
    {
        public ChatRoom()
        {
            Message = new HashSet<Message>();
            UserChatRoom = new HashSet<UserChatRoom>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Message> Message { get; set; }
        public ICollection<UserChatRoom> UserChatRoom { get; set; }
    }
}
