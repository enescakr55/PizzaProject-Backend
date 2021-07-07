using Core.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(IDataResult<int> currentUser, User user);
        IDataResult<User> Get(string phoneNumber,string password);
        IDataResult<User> GetByUserId(int userId);
        IResult ChangePassword(ChangePasswordDto changePasswordDto,User user);
    }
}
