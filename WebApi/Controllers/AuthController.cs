using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        IUserService _userService;

        public AuthController(IAuthService authService,IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        [HttpGet("islogged")]
        public IActionResult isLogin()
        {
            _authService.setHeader(Request.Headers);
            return Ok(_authService.IsLogged());
        }
        [HttpGet("isstaff")]
        public IActionResult isStaff()
        {
            _authService.setHeader(Request.Headers);
            return Ok(_authService.IsStaff());
        }
        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            _authService.setHeader(Request.Headers);
            var result = _authService.Login(loginModel.Username, loginModel.Password);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("logout")]
        public IActionResult Logout(string sessionKey)
        {
            _authService.setHeader(Request.Headers);
            var result = _authService.Logout(sessionKey);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("register")]
        public IActionResult Register (User user)
        {
            var result = _userService.Add(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getuserbysession")]
        public IActionResult GetUserBySession()
        {
            _authService.setHeader(Request.Headers);
            var getUserResult = _authService.GetUserIdBySessionKey();
            if (getUserResult.Success)
            {
                var user = _userService.GetByUserId(getUserResult.Data);
                return Ok(user);
            }
            else
            {
                return BadRequest(getUserResult);
            }
            
        }
        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            _authService.setHeader(Request.Headers);
            var getId = _authService.GetUserIdBySessionKey();
            var result = _userService.Update(getId, user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpPost("changepassword")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            _authService.setHeader(Request.Headers);
            var getId = _authService.GetUserIdBySessionKey();
            var getUser = _userService.GetByUserId(getId.Data);
            var result = _userService.ChangePassword(changePasswordDto, getUser.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

    }
}
