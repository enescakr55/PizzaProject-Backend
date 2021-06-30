using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Size:IEntity
    {
        public int Id { get; set; }
        public string SizeName { get; set; }
    }
}
