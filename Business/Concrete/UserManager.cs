using Business.Abstract;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        //IAuthService _authService;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
            //_authService = authService;

        }

        public IResult Add(User user)
        {
            UserValidation validator = new UserValidation();
            var validationResult = validator.Validate(user);
            if (validationResult.IsValid)
            {
                var getUsr = _userDal.Get(p => p.Username.ToLower() == user.Username.ToLower());
                if (getUsr != null)
                {
                    return new ErrorResult("Bu kullanıcı adı alınmış");
                }
                user.Password = Crypter.CreateMD5(user.Password);
                _userDal.Add(user);
                return new SuccessResult("Kullanıcı Eklendi");
            }
            else
            {
                return new ErrorDataResult<List<ValidationFailure>>(validationResult.Errors);
            }

        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult("Kullanıcı Silindi");
        }
        public IDataResult<User> GetByUserId(int userId)
        {
            User getUser = _userDal.Get(p => p.Id == userId);
            getUser.Password = "";
            return new SuccessDataResult<User>(getUser);
        }
        public IDataResult<User> Get(string username, string password)
        {
            User getUser = _userDal.Get(p => p.Username == username && p.Password == password);
            return new SuccessDataResult<User>(getUser);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IResult Update(IDataResult<int> currentUser,User user)
        {
            var loggedUser = currentUser;
            if(loggedUser.Success){
                var getUser = _userDal.Get(p => p.Id == loggedUser.Data);
                user.Password = getUser.Password;
                user.Username = getUser.Username;
                UserValidation validator = new UserValidation();
                var validationResult = validator.Validate(user);
                if (validationResult.IsValid)
                {
                    user.Id = getUser.Id;
                    var getUsr = _userDal.Get(p => p.Username.ToLower() == user.Username.ToLower());
                    if ((getUsr != null) && (getUsr.Id != user.Id))
                    {
                        return new ErrorResult("Bu kullanıcı adı alınmış");
                    }
                    _userDal.Update(user);
                    return new SuccessResult("Kullanıcı Güncellendi");
                }
                else
                {
                    return new ErrorDataResult<List<ValidationFailure>>(validationResult.Errors);
                }
            }
            else
            {
                return new ErrorResult("Lütfen giriş yapınız");
            } 


        }
    }
}
