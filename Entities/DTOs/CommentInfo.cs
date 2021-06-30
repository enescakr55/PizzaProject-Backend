using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CommentInfo
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Score { get; set; }
        public int PizzaId { get; set; }
        public string Username { get; set; }

    }
}
