using Business.Abstract;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal orderDal;
        IOrderHelperService orderHelperService;
        IPizzaService pizzaService;
        IAuthService authService;

        public OrderManager(IOrderDal orderDal,IOrderHelperService orderHelperService,IPizzaService pizzaService,IAuthService authService)
        {
            this.orderDal = orderDal;
            this.orderHelperService = orderHelperService;
            this.pizzaService = pizzaService;
            this.authService = authService;
        }

        public IResult Add(Order order)
        {
            orderDal.Add(order);
            return new SuccessResult("Başarıyla Eklendi");
        }

        public IResult AddPizzaOrder(PizzaOrder pizzaOrder)
        {
            pizzaOrder.order.Date = DateTime.Now;
            pizzaOrder.order.StatusId = 1;
            PizzaOrderValidation validator = new PizzaOrderValidation();
            var validationResult = validator.Validate(pizzaOrder);
            if (validationResult.IsValid == false)
            {
                return new ErrorDataResult<List<ValidationFailure>>(validationResult.Errors,"Doğrulama Hatası");
            }
            string pizzaTracker = CreatePizzaTracker();
            decimal totalPrice = 0;
            Order order = pizzaOrder.order;
            pizzaOrder.order.OrderCode = pizzaTracker;
            List < OrderHelper > orderHelpers= pizzaOrder.orderHelpers;
            orderDal.Add(pizzaOrder.order);
            foreach (var orderHelper in orderHelpers)
            {
                orderHelper.OrderId = order.Id;
                orderHelperService.Add(orderHelper);
                Pizza selectedPizza = pizzaService.GetById(orderHelper.ProductId).Data;
                totalPrice += selectedPizza.Price;
            }
            pizzaOrder.order.TotalPrice = totalPrice;
            if (pizzaOrder.order.PayWithCard == true)
            {
                FakePayService payService = new FakePayService();
                var payment = payService.Pay(totalPrice, pizzaOrder.creditCard);
                if (payment.Success == false)
                {
                    orderDal.Delete(pizzaOrder.order);
                    foreach (var orderHelper in orderHelpers)
                    {
                        orderHelper.OrderId = order.Id;
                        orderHelperService.Delete(orderHelper);
                    }
                    return payment;

                }
            }
            orderDal.Update(pizzaOrder.order);
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.OrderCode = pizzaTracker;
            orderInfo.TotalPrice = totalPrice;

            return new SuccessDataResult<OrderInfo>(orderInfo,"Siparişiniz Alındı");

            
        }

        public IResult Delete(Order order)
        {
            orderDal.Delete(order);
            return new SuccessResult("Başarıyla Silindi");
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(orderDal.GetAll());
        }
        public IDataResult<List<Order>> GetAllByUserId(int id)
        {
            return new SuccessDataResult<List<Order>>(orderDal.GetAll(p=>p.UserId == id));
        }

        public IResult Update(Order order)
        {
            orderDal.Update(order);
            return new SuccessResult("Başarıyla Silindi");
        }
        public String CreatePizzaTracker()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public IDataResult<Order> GetByTrackerCode(string trackerCode)
        {
            return new SuccessDataResult<Order>(orderDal.Get(s => s.OrderCode == trackerCode));
        }
    }
}
