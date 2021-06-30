using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Staff:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
