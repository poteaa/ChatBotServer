using ChatBot.Data.Models;
using ChatBot.Model;
using ChatBot.Model.DTO;
using ChatBot.Model.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Data
{
    public class Repository : IRepository
    {
        private AppSettings AppSettings { get; set; }

        public Repository(IOptions<AppSettings> settings)
        {
            AppSettings = settings.Value;
        }

        #region Authentication

        public AuthenticatedUser AuthenticateUser(Authentication login)
        {
            AuthenticatedUser user = null;
            using (var context = new ChatBotContext())
            {
                user = context.User.AsNoTracking()
                    .Where(u => u.Username == login.UserName && u.Password == login.Password)
                    .Select(u => new AuthenticatedUser
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Token = "",
                        Username = u.Username,
                    }).FirstOrDefault<AuthenticatedUser>();
            }
            return user;
        }

        #endregion Authentication

        #region ChatRoom

        public async Task<List<ChatRoomDTO>> GetChatRooms()
        {
            List<ChatRoomDTO> chatRooms = new List<ChatRoomDTO>();
            using (var context = new ChatBotContext())
            {
                chatRooms = await context.ChatRoom.AsNoTracking()
                    .Select(u => new ChatRoomDTO
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToListAsync<ChatRoomDTO>();
            }
            return chatRooms;
        }

        public async Task<ChatRoomDTO> GetChatRoom(int id)
        {
            ChatRoomDTO chatRoom = null;
            using (var context = new ChatBotContext())
            {
                chatRoom = await context.ChatRoom.Include(cr => cr.Message).AsNoTracking()
                    .Where(cr => cr.Id == id)
                    .Select(cr => new ChatRoomDTO
                    {
                        Id = cr.Id,
                        Name = cr.Name,
                        Messages = cr.Message
                                    .OrderByDescending(m => m.Id)
                                    .Select(m => new MessageDTO {
                                        Id = m.Id,
                                        Body = m.Body,
                                        AuthorId = m.UserId,
                                        AuthorName = m.User.Username,
                                        ChatRoomId = m.ChatRoomId
                                    })
                                    .Take(AppSettings.MessagesToRetrieve)
                                    .OrderBy(m => m.Id)
                                    .ToList<MessageDTO>()
                    }).FirstAsync();
            }
            return chatRoom;
        }

        #endregion ChatRoom

        #region Message

        public async Task<List<MessageDTO>> GetMessages(int chatRoomId)
        {
            List<MessageDTO> messages = new List<MessageDTO>();
            using (var context = new ChatBotContext())
            {
                messages = await context.Message
                    .Where(m => m.ChatRoomId == chatRoomId)
                    .Select(m => new MessageDTO
                    {
                        Id = m.Id,
                        Body = m.Body
                    }).ToListAsync<MessageDTO>();
            }
            return messages;
        }

        public async Task<MessageDTO> AddMessage(MessageDTO messageDTO)
        {
            Message message = null;
            using (var context = new ChatBotContext())
            {
                message = new Message
                {
                    Body = messageDTO.Body,
                    UserId = messageDTO.AuthorId,
                    ChatRoomId = messageDTO.ChatRoomId
                };
                context.Message.Add(message);
                await context.SaveChangesAsync();
                messageDTO.Id = message.Id;
            }
            return messageDTO; 
        }

        #endregion Message
    }
}
