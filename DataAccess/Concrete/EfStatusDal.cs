using Core.CrudOperations;
using Core.CrudOperations.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    
    public class EfStatusDal:EfCrudBase<OrderStatus,PizzaDbContext>,IStatusDal
    {
    }
}
