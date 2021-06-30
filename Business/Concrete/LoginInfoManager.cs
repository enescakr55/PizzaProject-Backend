using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class LoginInfoManager : ILoginInfoService
    {
        ILoginInfoDal _loginInfoDal;

        public LoginInfoManager(ILoginInfoDal loginInfoDal)
        {
            _loginInfoDal = loginInfoDal;
        }

        public IResult Add(LoginInfo loginInfo)
        {
            _loginInfoDal.Add(loginInfo);
            return new SuccessResult("Giriş Bilgisi Eklendi");
        }

        public IResult Delete(LoginInfo loginInfo)
        {
            _loginInfoDal.Delete(loginInfo);
            return new SuccessResult("Giriş Bilgisi Silindi");
        }

        public IDataResult<List<LoginInfo>> GetAll()
        {
            return new SuccessDataResult<List<LoginInfo>>(_loginInfoDal.GetAll());
        }

        public IResult Update(LoginInfo loginInfo)
        {
            _loginInfoDal.Update(loginInfo);
            return new SuccessResult("Giriş Bilgisi Güncellendi");
        }
        public String CreateSessionKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public IDataResult<LoginInfo> CreateInfo(User user)
        {
            LoginInfo loginInfo = new LoginInfo();
            loginInfo.IsActive = true;
            loginInfo.SessionKey = CreateSessionKey();
            loginInfo.UserId = user.Id;
            _loginInfoDal.Add(loginInfo);
            return new SuccessDataResult<LoginInfo>(loginInfo);
        }

        public IDataResult<LoginInfo> Get(string sessionKey)
        {
            var result = _loginInfoDal.Get(p => p.SessionKey == sessionKey);
            return new SuccessDataResult<LoginInfo>(result);
        }
    }
}
