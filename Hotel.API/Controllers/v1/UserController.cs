using Hotel.API.Wrappers;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Environment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetAllAsync([FromQuery] PaggedRequest paggedRequest)
        {
            try
            {
                var total = await _userDao.GetTotalAsync();
                var users = await _userDao.GetAllAsync(paggedRequest.Page, paggedRequest.Size);
                return Ok(new PagedResponse<List<User>>(users, total, paggedRequest));
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, LogLevel.Error);
                return BadRequest(new Response(ex));
            }
        }
    }
}
