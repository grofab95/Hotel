using AutoMapper;
using Hotel.API.Wrappers;
using Hotel.Application.Dtos.TokenDtos;
using Hotel.Application.Dtos.UserDtos;
using Hotel.Application.Managers;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Environment;
using Hotel.Domain.Utilities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserDao _userDao;
        private ILogger _logger;
        private IMapper _mapper;

        public UserController(IUserDao userDao, ILogger logger, IMapper mapper)
        {
            _userDao = userDao;
            _logger = logger;
            _mapper = mapper;
        }
               
        [Route("authorize")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AuthorizeAsync([FromBody] UserCredentialDto userCredential)
        {
            try
            {
                var user = await _userDao.VerifyCredentialAsync(userCredential.Email, userCredential.Password);
                var token = TokenManager.GnerateToken(user);
                await _userDao.UpdateTokenAsync(user.Id, token);
                return Ok(new Response<TokenResponse>(new TokenResponse(token)));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ex));
            }
        }

        [Route("addUser")]
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserCreateDto dto)
        {
            try
            {
                var user = await _userDao.AddUser(new User(dto.Name, dto.Surname, dto.Email, new Password(dto.Password)));
                return Ok(new Response<User>(user));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ex));
            }
        }

        [Route("getUsers")]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] PaggedRequest paggedRequest)
        {
            try
            {
                var total = await _userDao.GetTotalAsync();
                var users = await _userDao.GetAllAsync(paggedRequest.Page, paggedRequest.Size);
                var mapped = _mapper.Map<List<UserGetDto>>(users);
                return Ok(new PagedResponse<List<UserGetDto>>(mapped, total, paggedRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ex));
            }
        }
    }
}
