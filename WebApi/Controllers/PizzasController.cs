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
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        
        IPizzaService pizzaService;
        IAuthService authService;
        public PizzasController(IPizzaService pizzaService,IAuthService authService)
        {
            this.pizzaService = pizzaService;
            this.authService = authService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            
            return Ok(pizzaService.GetAll());
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            return Ok(pizzaService.GetById(id));
        }
        [HttpPost("add")]
        public IActionResult Add(Pizza pizza)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(pizzaService.Add(pizza));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
            
        }
        [HttpPost("delete")]
        public IActionResult Delete(Pizza pizza)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(pizzaService.Delete(pizza));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }
        [HttpPost("update")]
        public IActionResult Update(Pizza pizza)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(pizzaService.Update(pizza));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }
        [HttpGet("getbyname")]
        public IActionResult getByName(string name)
        {
            return Ok(pizzaService.GetByName(name));
        }
    }
}
