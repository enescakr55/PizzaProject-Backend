using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class LoginInfo:IEntity
    {
        public int Id { get; set; }
        public string SessionKey { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
