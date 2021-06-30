using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPizzaService
    {
        IDataResult<Pizza> GetById(int id);
        IDataResult<List<Pizza>> GetByName(string name);
        IDataResult<List<Pizza>> GetAll();
        IResult Add(Pizza pizza);
        IResult Delete(Pizza pizza);
        IResult Update(Pizza pizza);
    }
}
