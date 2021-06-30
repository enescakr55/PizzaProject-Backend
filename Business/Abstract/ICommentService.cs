using Core.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IDataResult<List<Comment>> GetAll();
        IDataResult< List<CommentInfo>> GetAllByPizzaId(int id);
        IDataResult<List<Comment>> GetAllByUserId(int userid);
        IDataResult<Comment> GetById(int id);
        IResult Add(Comment comment);
        IResult Update(Comment comment);
        IResult Delete(Comment comment);
        IDataResult<List<Comment>> GetUserComment(int userId, int pizzaId);
    }
}
