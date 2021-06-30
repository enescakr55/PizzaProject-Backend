using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IStaffService
    {
        IDataResult<List<Staff>> GetAll();
        IResult Add(Staff staff);
        IResult Delete(Staff staff);
        IResult Update(Staff staff);
        IDataResult<Staff> Get(int userId);
    }
}
