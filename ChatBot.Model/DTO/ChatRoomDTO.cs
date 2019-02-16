using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot.Model.DTO
{
    public class ChatRoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MessageDTO> Messages { get; set; }
    }
}
