using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class PizzaOrder
    {
        public Order order { get; set; }
        public List<OrderHelper> orderHelpers { get; set; }
        public CreditCard creditCard { get; set; }
    }
}
