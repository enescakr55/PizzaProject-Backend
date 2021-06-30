using Business.Abstract;
using Core.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        IAuthService _authService;
        IUserService _userService;
        ICommentService _commentService;

        public CommentsController(IAuthService authService, IUserService userService, ICommentService commentService)
        {
            _authService = authService;
            _userService = userService;
            _commentService = commentService;
        }
        [HttpPost("addcomment")]
        public IActionResult AddComment(Comment comment)
        {
            _authService.setHeader(Request.Headers);
            var isLogged = _authService.IsLogged();
            if (isLogged.Success)
            {
                
                var userId = _authService.GetUserIdBySessionKey().Data;
                comment.UserId = userId;
                if (comment.Score > 5)
                    comment.Score = 5;
                if (comment.Score < 1)
                    comment.Score = 1;
                var getcomments = _commentService.GetUserComment(userId, comment.PizzaId);
                if(getcomments.Data.Count > 0)
                {
                    comment.Id = getcomments.Data[0].Id;
                    return Ok(_commentService.Update(comment));
                }

                return Ok(_commentService.Add(comment));
            }
            else
            {
                return BadRequest(isLogged);
            }
        }
        [HttpGet("deletecomment")]
        public IActionResult DeleteComment(int commentId)
        {
            _authService.setHeader(Request.Headers);
            var userResult = _authService.GetUserIdBySessionKey();
            var result = _commentService.GetById(commentId);
            if (result.Success && userResult.Success)
            {
                if(result.Data != null)
                {
                    if(result.Data.UserId == userResult.Data)
                    {
                        var c = new Comment();
                        c.Id = commentId;
                        return Ok(_commentService.Delete(c));
                    }
                    else
                    {
                        return BadRequest(new ErrorResult("Bu yorum sizin değil"));
                    }
                }
            }
            return BadRequest(new ErrorResult("Yorum silinemedi"));
        }
        [HttpGet("getcomments")]
        public IActionResult GetComments(int pizzaId)
        {
            return Ok(_commentService.GetAllByPizzaId(pizzaId));
        }
        [HttpGet("getmycommentsbypizzaid")]
        public IActionResult GetMyCommentsByPizzaId(int pizzaId)
        {
            _authService.setHeader(Request.Headers);
            var isLogged = _authService.IsLogged();
            if (isLogged.Success)
            {
                var userResult = _authService.GetUserIdBySessionKey();
                return Ok(_commentService.GetUserComment(userResult.Data, pizzaId));
            }
            else
            {
                return BadRequest(isLogged);
            }
        }
    }
}
