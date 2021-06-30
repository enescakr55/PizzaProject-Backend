using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrderStatus:IEntity
    {
        public int Id { get; set; }
        public string IdStr { get; set; }
        public string StatusName { get; set; }
    }
}
