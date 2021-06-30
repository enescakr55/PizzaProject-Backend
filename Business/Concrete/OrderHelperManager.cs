using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OrderHelperManager : IOrderHelperService
    {
        IOrderHelperDal orderHelperDal;

        public OrderHelperManager(IOrderHelperDal orderHelperDal)
        {
            this.orderHelperDal = orderHelperDal;
        }

        public IResult Add(OrderHelper orderHelper)
        {
            orderHelperDal.Add(orderHelper);
            return new SuccessResult("Başarıyla Eklendi");
        }

        public IResult Delete(OrderHelper orderHelper)
        {
            orderHelperDal.Delete(orderHelper);
            return new SuccessResult("Başarıyla Silindi");
        }

        public IDataResult<List<OrderHelper>> GetAll()
        {
            return new SuccessDataResult<List<OrderHelper>>(orderHelperDal.GetAll());
        }

        public IResult Update(OrderHelper orderHelper)
        {
            orderHelperDal.Update(orderHelper);
            return new SuccessResult("Başarıyla Güncellendi");
        }
    }
}
