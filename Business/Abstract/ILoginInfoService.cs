using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ILoginInfoService
    {
        IDataResult<List<LoginInfo>> GetAll();
        IResult Add(LoginInfo loginInfo);
        IResult Delete(LoginInfo loginInfo);
        IResult Update(LoginInfo loginInfo);
        IDataResult<LoginInfo> CreateInfo(User user);
        IDataResult<LoginInfo> Get(string sessionKey);
    }
}
