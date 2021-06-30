using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class StaffManager : IStaffService
    {
        IStaffDal _staffDal;

        public StaffManager(IStaffDal staffDal)
        {
            _staffDal = staffDal;
        }

        public IResult Add(Staff staff)
        {
            _staffDal.Add(staff);
            return new SuccessResult("Yetkili başarıyla eklendi");
        }

        public IResult Delete(Staff staff)
        {
            _staffDal.Delete(staff);
            return new SuccessResult("Yetkili başarıyla silindi");
        }

        public IDataResult<Staff> Get(int userId)
        {
            return new SuccessDataResult<Staff>(_staffDal.Get(p => p.UserId == userId));
        }

        public IDataResult<List<Staff>> GetAll()
        {
            return new SuccessDataResult<List<Staff>>(_staffDal.GetAll());
        }

        public IResult Update(Staff staff)
        {
            _staffDal.Update(staff);
            return new SuccessResult("Yetkili başarıyla güncellendi");
        }
    }
}
