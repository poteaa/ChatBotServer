using System;
using System.Collections.Generic;

namespace ChatBot.Data.Models
{
    public partial class UserChatRoom
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChatRoomId { get; set; }

        public ChatRoom ChatRoom { get; set; }
        public User User { get; set; }
    }
}
