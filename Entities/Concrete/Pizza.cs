using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Pizza:IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SizeId { get; set; }
        public string PizzaName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string PictureLink { get; set; }

    }
}
