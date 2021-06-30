using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class FakePayService
    {
        public IResult Pay(decimal price,CreditCard card)
        {
            return new SuccessResult("Ödeme başarılı olarak gerçekleşti");
        }
    }
}
