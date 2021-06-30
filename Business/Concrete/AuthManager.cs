using Business.Abstract;
using Business.Services;
using Core.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager:IAuthService
    {
        IHeaderDictionary _header;
        IUserService _userService;
        ILoginInfoService _loginInfoService;
        IStaffService _staffService;

        public AuthManager(IUserService userService, ILoginInfoService loginInfoService, IStaffService staffService)
        {
            _userService = userService;
            _loginInfoService = loginInfoService;
            _staffService = staffService;
        }

        public void setHeader(IHeaderDictionary header)
        {
            _header = header;
        }

        public IResult IsLogged()
        {
            var sessionKey = _header["sessionKey"].ToString();
            if (sessionKey != null)
            {
                var info = _loginInfoService.Get(sessionKey);
                if (info.Success)
                {
                    if (info.Data != null)
                    {
                        if (info.Data.IsActive)
                        {
                            return new SuccessResult("Giriş Yapılmış");
                        }
                    }
                }
            }
            return new ErrorResult("Giriş yapılmamış");
        }

        public IResult IsStaff()
        {
           if(_header != null)
           {
                var sessionKey = _header["sessionKey"].ToString();
                if (sessionKey != null)
                {
                    var info = _loginInfoService.Get(sessionKey);
                    if (info.Success)
                    {
                        if(info.Data != null){ 
                        if (info.Data.IsActive)
                        {
                            var staffControl = _staffService.Get(info.Data.UserId);
                            if (staffControl != null)
                            {
                                    if(staffControl.Data != null)
                                    {
                                        return new SuccessResult("Yetkilisiniz");
                                    }   
                                        //return new SuccessResult(_staffService.Get(info.Data.UserId).Data.ToString());
                            }

                        }
                    }
                    }
                }
            }

            return new ErrorResult("Bu işlemi gerçekleştirmeye yetkiniz yoktur.");
        }
        public IResult RequiredAuth()
        {
            return new ErrorResult("Doğrulama Başarısız");
        }

        public IDataResult<LoginInfo> Login(string username, string password)
        {
            password = Crypter.CreateMD5(password);
            User user = _userService.Get(username, password).Data;
            if (user != null)
            {
                var loginInfoResult = _loginInfoService.CreateInfo(user);
                LoginInfo loginInfo = loginInfoResult.Data;
                if (loginInfoResult.Success)
                {
                    return new SuccessDataResult<LoginInfo>(loginInfo,"Başarıyla giriş yapıldı");
                }
                return new ErrorDataResult<LoginInfo>("Bilgileri kontrol edin");
            }
            return new ErrorDataResult<LoginInfo>("Bilgileri kontrol edin");
        }

        public IResult Logout(string sessionKey)
        {
            var result = _loginInfoService.Get(sessionKey);
            if(result.Data != null)
            {
                result.Data.IsActive = false;
                _loginInfoService.Update(result.Data);
                return new SuccessResult("Oturum kapatıldı");
            }
            return new ErrorResult("Oturum kapatma başarısız oldu");
        }
        public IDataResult<int> GetUserIdBySessionKey()
        {
            var sessionKey = _header["sessionKey"].ToString();
            if (sessionKey != null)
            {
                var info = _loginInfoService.Get(sessionKey);
                if (info.Success)
                {
                    if (info.Data != null)
                    {
                        if (info.Data.IsActive)
                        {
                            return new SuccessDataResult<int>(info.Data.UserId,"Giriş Yapılmış");
                        }
                    }
                }
            }
            return new ErrorDataResult<int>(default,"Giriş yapılmamış");
        }

    }
}
