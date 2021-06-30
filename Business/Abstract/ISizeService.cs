using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ISizeService
    {
        IDataResult<List<Size>> GetAll();
        IDataResult<Size> GetById(int id);
        IResult Add(Size size);
        IResult Delete(Size size);
        IResult Update(Size size);
    }
}
