using Core.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        public IDataResult<LoginInfo> Login(string phoneNumber, string password);
        public IResult Logout(string sessionKey);
        public IResult IsLogged();
        public IResult IsStaff();
        public void setHeader(IHeaderDictionary header);
        public IDataResult<int> GetUserIdBySessionKey();

    }
}
