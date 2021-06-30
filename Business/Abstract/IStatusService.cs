using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStatusService
    {
        IDataResult<List<OrderStatus>> GetAll();
        IResult Add(OrderStatus status);
        IResult Delete(OrderStatus status);
        IResult Update(OrderStatus status);
        IDataResult<OrderStatus> Get(int statusId);
    }
}
