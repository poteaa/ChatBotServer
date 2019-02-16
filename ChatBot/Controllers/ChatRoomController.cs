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
    public class ChatRoomsController : ControllerBase
    {
        private IHubContext<ChatHub> _hubContext;

        private readonly IRepository _repository;

        public ChatRoomsController(IHubContext<ChatHub> hubContext, IRepository repository)
        {
            _hubContext = hubContext;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<ChatRoomDTO> chatRooms = await _repository.GetChatRooms();
                return Ok(chatRooms);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                ChatRoomDTO chatRoom = await _repository.GetChatRoom(id);
                return Ok(chatRoom);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}