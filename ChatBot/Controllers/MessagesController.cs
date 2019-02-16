using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Data;
using ChatBot.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IHubContext<ChatHub> _hubContext;
        private readonly IRepository _repository;
        
        public MessagesController(IHubContext<ChatHub> hubContext, IRepository repository)
        {
            _hubContext = hubContext;
            _repository = repository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessageDTO message)
        {
            try
            {
                await _repository.AddMessage(message);
                await _hubContext.Clients.Group(message.ChatRoomName).SendAsync("receivemessage", message);
                return Ok(message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

    }
}