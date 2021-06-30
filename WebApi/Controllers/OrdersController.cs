using Business.Abstract;
using Core.Results;
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
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService orderService;
        IOrderHelperService orderHelperService;
        IAuthService authService;
        public OrdersController(IOrderService orderService, IOrderHelperService orderHelperService,IAuthService authService)
        {
            this.orderService = orderService;
            this.orderHelperService = orderHelperService;
            this.authService = authService;
        }
        [HttpPost("addpizzaorder")]
        public IActionResult addPizzaOrder(PizzaOrder pizzaOrder)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsLogged().Success)
            {
                pizzaOrder.order.UserId = authService.GetUserIdBySessionKey().Data;
            }
            var result = orderService.AddPizzaOrder(pizzaOrder);
            if (result.Success)
            {
               return Ok(result);
            }
            return BadRequest(result);
            
        }
        [HttpGet("getallpizzaorders")]

        public IActionResult getAllPizzaOrder()
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                List<PizzaOrder> pizzaOrders = new List<PizzaOrder>();
                var orders = orderService.GetAll().Data;
                var orderHelpers = orderHelperService.GetAll().Data;
                foreach (var order in orders)
                {
                    Order currentorder = new Order();
                    PizzaOrder pizzaOrder = new PizzaOrder();
                    List<OrderHelper> currentHelpers = new List<OrderHelper>();
                    currentorder = order;
                    foreach (var orderHelper in orderHelpers)
                    {
                        if (orderHelper.OrderId == order.Id)
                        {
                            currentHelpers.Add(orderHelper);
                        }

                    }
                    pizzaOrder.order = currentorder;
                    pizzaOrder.orderHelpers = currentHelpers;
                    pizzaOrder.creditCard = null;
                    pizzaOrders.Add(pizzaOrder);
                }
                return Ok(new SuccessDataResult<List<PizzaOrder>>(pizzaOrders));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }
        [HttpGet("getordersbysession")]
        public IActionResult GetBySession()
        {
            authService.setHeader(Request.Headers);
            if (authService.IsLogged().Success)
            {
                int sessionId = authService.GetUserIdBySessionKey().Data;
                List<PizzaOrder> pizzaOrders = new List<PizzaOrder>();
                var orders = orderService.GetAllByUserId(sessionId).Data;
                var orderHelpers = orderHelperService.GetAll().Data;
                foreach (var order in orders)
                {
                    Order currentorder = new Order();
                    PizzaOrder pizzaOrder = new PizzaOrder();
                    List<OrderHelper> currentHelpers = new List<OrderHelper>();
                    currentorder = order;
                    foreach (var orderHelper in orderHelpers)
                    {
                        if (orderHelper.OrderId == order.Id)
                        {
                            currentHelpers.Add(orderHelper);
                        }
                    }
                    pizzaOrder.order = currentorder;
                    pizzaOrder.orderHelpers = currentHelpers;
                    pizzaOrder.creditCard = null;
                    pizzaOrders.Add(pizzaOrder);
                }
                return Ok(new SuccessDataResult<List<PizzaOrder>>(pizzaOrders));
            }
            else
            {
                return BadRequest(authService.IsLogged());
            }
        }
        [HttpGet("getbytracker")]
        public IActionResult GetByTracker(string tracker)
        {
            return Ok(orderService.GetByTrackerCode(tracker));
        }
        [HttpPost("update")]

        public IActionResult Update(Order order)
        {
            authService.setHeader(Request.Headers);
            if (authService.IsStaff().Success)
            {
                return Ok(orderService.Update(order));
            }
            else
            {
                return BadRequest(authService.IsStaff());
            }
        }

    }
}
