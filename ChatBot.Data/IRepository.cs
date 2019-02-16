using System.Collections.Generic;
using System.Threading.Tasks;
using ChatBot.Model;
using ChatBot.Model.DTO;

namespace ChatBot.Data
{
    public interface IRepository
    {
        Task<MessageDTO> AddMessage(MessageDTO messageDTO);
        AuthenticatedUser AuthenticateUser(Authentication login);
        Task<ChatRoomDTO> GetChatRoom(int id);
        Task<List<ChatRoomDTO>> GetChatRooms();
        Task<List<MessageDTO>> GetMessages(int chatRoomId);
    }
}