using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "IUserService", SessionMode = SessionMode.Required)]
    public interface IUserService
    {
        [OperationContract]
        UsersModel Login(string UserName, string Password);
        [OperationContract]
        UsersModel GetUserInfo(int UserId);
        [OperationContract]
        List<SkillsModel> GetSkillList();
        [OperationContract]
        UsersInfoModel UpdateUsersInfoModel(UsersModel UserModel);
        [OperationContract]
        bool UpdateGoodAtWhat(UsersModel UserModel);
        [OperationContract]
        UsersModel UpdateUsersModel(UsersModel UserModel);
        [OperationContract]
        UsersModel GetUserInfoByName(string UserName);
        [OperationContract]
        bool Register(UsersModel UserModel);

    }
}
