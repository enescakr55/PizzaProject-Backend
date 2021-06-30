using Core.CrudOperations;
using Core.CrudOperations.EntityFramework;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IPizzaDal:ICrudBase<Pizza>
    {
    }
}
