using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/sizes")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        ISizeService sizeService;
        IAuthService authService;
        public SizesController(ISizeService sizeService,IAuthService authService)
        {
            this.sizeService = sizeService;
            this.authService = authService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {

            return Ok(sizeService.GetAll());
        }
        [HttpPost("add")]
        public IActionResult Add(Size size)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(sizeService.Add(size));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }
        [HttpPost("delete")]
        public IActionResult Delete(Size size)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(sizeService.Delete(size));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }
        [HttpPost("update")]
        public IActionResult Update(Size size)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(sizeService.Update(size));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            return Ok(sizeService.GetById(id));
        }
    }
}
