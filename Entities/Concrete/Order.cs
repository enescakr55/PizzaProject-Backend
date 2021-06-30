using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{

    public class Order:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string OrderCode { get; set; }
        public int? StatusId { get; set; }
        public DateTime? Date { get; set; }
        public decimal TotalPrice { get; set; }
        public bool? PayWithCard { get; set; }
        public string Desc { get; set; }

    }
}
