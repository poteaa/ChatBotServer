using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Data;
using ChatBot.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepository _repository;

        public LoginController(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> PostAsync([FromBody]Authentication login)
        {
            IActionResult response = null;
            AuthenticatedUser authUser;
            try
            {
                authUser = _repository.AuthenticateUser(login);
                if (authUser == null)
                {
                    response = StatusCode(StatusCodes.Status404NotFound, "User not found"); ;
                }
                else
                {
                    authUser.Token = Guid.NewGuid().ToString();
                    response = Ok(authUser);
                }
            }
            catch (Exception ex)
            {
                // Write the excption to a log
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }
    }
}