using Hotel.Domain.Adapters;
using Hotel.Domain.Environment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hotel.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserDao _userDao;
        private ILogger _logger;

        public UserController(IUserDao userDao, ILogger logger)
        {
            _userDao = userDao;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await _userDao.GetAllAsync();
                return Ok(new { users });
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, LogLevel.Error);
                return BadRequest(new { ex.Message });
            }
        }
    }
}
