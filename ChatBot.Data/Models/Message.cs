using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot.Data.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }

        public ChatRoom ChatRoom { get; set; }
        public User User { get; set; }
    }
}
