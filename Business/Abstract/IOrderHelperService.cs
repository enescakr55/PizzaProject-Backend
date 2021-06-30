using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderHelperService
    {
        IDataResult<List<OrderHelper>> GetAll();
        IResult Add(OrderHelper orderHelper);
        IResult Delete(OrderHelper orderHelper);
        IResult Update(OrderHelper orderHelper);
    }
}
