using Core.CrudOperations.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public class EfCommentDal : EfCrudBase<Comment, PizzaDbContext>, ICommentDal
    {
        public List<CommentInfo> GetCommentInfo(Expression<Func<CommentInfo, bool>> filter = null)
        {
            using(PizzaDbContext context  = new PizzaDbContext())
            {
                var commentInfo = from c in context.Comments
                                  join u in context.Users
                                  on c.UserId equals u.Id
                                  select new CommentInfo
                                  {
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      Id = c.Id,
                                      Score = c.Score,
                                      Text = c.Text,
                                      PizzaId = c.PizzaId,
                                      Username = u.Username
                                  };
                return filter == null ? commentInfo.ToList() : commentInfo.Where(filter).ToList();

            }
        }
    }
}
