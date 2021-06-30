using Business.Abstract;
using Core.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/statuses")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        IStatusService _statusService;
        IAuthService _authService;

        public StatusesController(IStatusService statusService, IAuthService authService)
        {
            _statusService = statusService;
            _authService = authService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(_statusService.GetAll());
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            return Ok(_statusService.Get(id));
        }
        [HttpPost("addstatus")]
        public IActionResult AddStatus(OrderStatus status)
        {
            _authService.setHeader(Request.Headers);
            if (_authService.IsLogged().Success && _authService.IsStaff().Success)
            {
                return Ok(_statusService.Add(status));
            }
            else
            {
                return BadRequest(new ErrorResult("Lütfen giriş yaptığınızdan ve bu işlemi yapmaya yetkiniz olduğundan emin olun"));
            }
        }
        [HttpGet("deletestatus")]
        public IActionResult DeleteStatus(int statusid)
        {
            _authService.setHeader(Request.Headers);
            OrderStatus orderStatus = new OrderStatus();
            orderStatus.Id = statusid;
            if (_authService.IsLogged().Success && _authService.IsStaff().Success)
            {
                return Ok(_statusService.Delete(orderStatus));
            }
            else
            {
                return BadRequest(new ErrorResult("Lütfen giriş yaptığınızdan ve bu işlemi yapmaya yetkiniz olduğundan emin olun"));
            }
        }
    }
}
