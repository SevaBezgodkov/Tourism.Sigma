using AuthUsers.Models;
using AuthUsers.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthUsers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/add")]
        public async Task<IActionResult> AddAsync(UserModel model)
        {
            try
            {

                await _userService.AddAsync(model);

                return Ok();
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/update")]
        public async Task<IActionResult> UpdateAsync(Guid userId, UserModel model)
        {
            try
            {
                await _userService.UpdateByIdAsync(userId, model);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
