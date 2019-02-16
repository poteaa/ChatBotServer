using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatBot
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            //await Clients.Group(roomName).SendAsync("receivemessage", new MessageDTO
            //{
            //    AuthorName = "Test",
            //    Body = $"Connected to {roomName}"
            //});
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
