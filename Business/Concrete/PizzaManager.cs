using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PizzaManager : IPizzaService
    {
        IPizzaDal pizzaDal;

        public PizzaManager(IPizzaDal pizzaDal)
        {
            this.pizzaDal = pizzaDal;
        }
        public IDataResult<Pizza> GetById(int id){
            return new SuccessDataResult<Pizza>(pizzaDal.Get(p => p.Id == id));    
        }
        public IResult Add(Pizza pizza)
        {
            pizzaDal.Add(pizza);
            return new SuccessResult("Pizza Eklendi");
        }
        public IResult Delete(Pizza pizza)
        {
            pizzaDal.Delete(pizza);
            return new SuccessResult("Pizza silindi");
        }
        public IResult Update(Pizza pizza)
        {
            pizzaDal.Update(pizza);
            return new SuccessResult("Pizza güncellendi");
        }
        public IDataResult<List<Pizza>> GetAll()
        {
            return new SuccessDataResult<List<Pizza>>(pizzaDal.GetAll());
        }

        public IDataResult<List<Pizza>> GetByName(string name)
        {
            return new SuccessDataResult<List<Pizza>>(pizzaDal.GetAll(p => p.PizzaName.ToLower().Contains(name.ToLower())));
        }
    }
}
