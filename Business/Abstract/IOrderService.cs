using Core.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<Order>> GetAll();
        IResult Add(Order order);
        IResult Delete(Order order);
        IResult Update(Order order);
        IResult AddPizzaOrder(PizzaOrder pizzaOrder);
        IDataResult<List<Order>> GetAllByUserId(int id);
        IDataResult<Order> GetByTrackerCode(string trackerCode);
    }
}
