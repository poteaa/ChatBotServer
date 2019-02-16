using ChatBot.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot.Model
{
    public class AuthenticatedUser : UserDTO
    {
        public string Token { get; set; }
    }
}
