using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class StatusManager : IStatusService
    {
        IStatusDal _statusDal;

        public StatusManager(IStatusDal statusDal)
        {
            _statusDal = statusDal;
        }

        public IResult Add(OrderStatus status)
        {
            _statusDal.Add(status);
            return new SuccessResult("Başarıyla Eklendi");
        }

        public IResult Delete(OrderStatus status)
        {
            _statusDal.Delete(status);
            return new SuccessResult("Başarıyla Silindi");
        }

        public IDataResult<OrderStatus> Get(int statusId)
        {
            return new SuccessDataResult<OrderStatus>(_statusDal.Get(p => p.Id == statusId));
        }

        public IDataResult<List<OrderStatus>> GetAll()
        {
            return new SuccessDataResult<List<OrderStatus>>(_statusDal.GetAll());
        }

        public IResult Update(OrderStatus status)
        {
            _statusDal.Update(status);
            return new SuccessResult("Başarıyla Güncellendi");
        }
    }
}
