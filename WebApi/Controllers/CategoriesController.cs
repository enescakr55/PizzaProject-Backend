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
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService categoryService;
        IAuthService authService;
        public CategoriesController(ICategoryService categoryService,IAuthService authService)
        {
            this.categoryService = categoryService;
            this.authService = authService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {

            return Ok(categoryService.GetAll());
        }
        [HttpPost("add")]
        public IActionResult Add(Category category)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(categoryService.Add(category));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }
        [HttpPost("delete")]
        public IActionResult Delete(Category category)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(categoryService.Delete(category));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }
        [HttpPost("update")]
        public IActionResult Update(Category category)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(categoryService.Update(category));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }
    }
}
