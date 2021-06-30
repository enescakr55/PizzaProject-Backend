using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class SizeManager : ISizeService
    {
        ISizeDal sizeDal;

        public SizeManager(ISizeDal sizeDal)
        {
            this.sizeDal = sizeDal;
        }

        public IResult Add(Size size)
        {
            sizeDal.Add(size);
            return new SuccessResult("Başarıyla Eklendi");
        }

        public IResult Delete(Size size)
        {
            sizeDal.Delete(size);
            return new SuccessResult("Başarıyla Silindi");
        }

        public IDataResult<List<Size>> GetAll()
        {
            return new SuccessDataResult<List<Size>>(sizeDal.GetAll());
        }

        public IDataResult<Size> GetById(int id)
        {
            return new SuccessDataResult<Size>(sizeDal.Get(p => p.Id == id));
        }

        public IResult Update(Size size)
        {
            sizeDal.Update(size);
            return new SuccessResult("Başarıyla Güncellendi");
        }
    }
}
