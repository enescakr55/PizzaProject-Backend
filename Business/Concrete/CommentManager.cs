using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public IResult Add(Comment comment)
        {
            _commentDal.Add(comment);
            return new SuccessResult("Başarıyla Eklendi");
        }

        public IResult Delete(Comment comment)
        {
            _commentDal.Delete(comment);
            return new SuccessResult("Başarıyla Silindi");
        }

        public IDataResult<List<Comment>> GetAll()
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll());
        }

        public IDataResult<List<CommentInfo>> GetAllByPizzaId(int id)
        {
            return new SuccessDataResult<List<CommentInfo>>(_commentDal.GetCommentInfo(p=>p.PizzaId == id));
        }

        public IDataResult<List<Comment>> GetAllByUserId(int userid)
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll(p => p.UserId == userid));
        }

        public IDataResult<Comment> GetById(int id)
        {
            return new SuccessDataResult<Comment>(_commentDal.Get(p => p.Id == id));
        }

        public IDataResult<List<Comment>> GetUserComment(int userId, int pizzaId)
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll(p => p.PizzaId == pizzaId && p.UserId == userId));
        }

        public IResult Update(Comment comment)
        {
            _commentDal.Update(comment);
            return new SuccessResult("Başarıyla güncellendi");
        }
    }
}
