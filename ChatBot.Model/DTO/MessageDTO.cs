using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot.Model.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int ChatRoomId { get; set; }
        public string ChatRoomName { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
