using Digital.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;

namespace Digital.Service.Implements
{
    public partial class Service : IUserService
    {



        public UsersModel Login(string UserName, string Password)
        {
            var User = GenericList.CacheModelObj.UserModellist.Where(o => o.Value.Name == UserName && o.Value.Passwords == Password).FirstOrDefault();
            //var User = GenericList.CacheModelObj.UserModellist.Where(o => o.Name == UserName && o.Passwords == Password).FirstOrDefault();
            if (User.Value != null)
            {
                return User.Value;
            }
            else
            {
                return null;
            }
        }

        public UsersModel GetUserInfoByName(string UserName)
        {
            var User = GenericList.CacheModelObj.UserModellist.Where(o => o.Value.Name == UserName ).FirstOrDefault();
            //UsersModel User = GenericList.CacheModelObj.UserModellist.Where(o => o.Name == UserName).FirstOrDefault();
            if (User.Value != null)
            {
                return User.Value;
            }
            else
            {
                return null;
            }
        }

        public UsersModel GetUserInfo(int UserId)
        {
            if (GenericList.CacheModelObj.UserModellist.ContainsKey(UserId))
            {
                 return GenericList.CacheModelObj.UserModellist[UserId];        
            }
            else
            {
                Digital.Contact.BLL.UsersService UserService = new Contact.BLL.UsersService();
                var User= UserService.Find(UserId);
                if (User != null)
                {
                    GenericList.CacheModelObj.UserModellist.Add(UserId, User);
                }
                return User;
            }
        }


        public List<SkillsModel> GetSkillList()
        {
            var Skills = GenericList.CacheModelObj.SkillsModellist.ToList();
            if (Skills != null)
            {
                return Skills;
            }
            else
            {
                return null;
            }
        }

        public UsersInfoModel UpdateUsersInfoModel(UsersModel UserModel)
        {
            var CurrentUserInfo = UserModel.UsersInfoModel;
            var UserInfoModels = GetUserInfo(UserModel.ID).UsersInfoModel;
            if (UserInfoModels == null)
            {
                CurrentUserInfo.UpdateStatus = 1;
                GetUserInfo(UserModel.ID).UsersInfoModel = CurrentUserInfo;
               

            }
            else if(UserInfoModels != CurrentUserInfo)
            {
                UserInfoModels = CurrentUserInfo;
                UserInfoModels.UpdateStatus = 2;
            }
            //更新缓存
            GenericList.CacheModelObj.UserModellist[UserModel.ID].UsersInfoModel = CurrentUserInfo;
            //插入buffer
            GenericList.InsertBuffer(UserModel, UserInfoModels);
            return UserInfoModels;
        }

        public bool UpdateGoodAtWhat(UsersModel UserModel)
        {
            var UserModels = GetUserInfo(UserModel.ID);
            if (UserModels.UsersInfoModel.GoodAtWhatModels != UserModel.UsersInfoModel.GoodAtWhatModels)
            {

                //新添加的goodatwhatModel
                var AddModels = UserModel.UsersInfoModel.GoodAtWhatModels.Where(o => o.GoodAtWhatID==0).FirstOrDefault();
                if (AddModels != null)
                {
                    //增加
                    AddModels.UpdateStatus = 1;
                    //更新缓存
                    UserModels.UsersInfoModel.GoodAtWhatModels.Add(AddModels);
                    //插入buffer
                    GenericList.InsertBuffer(UserModels, AddModels);
                }
                var DelModels = UserModel.UsersInfoModel.GoodAtWhatModels.Where(o => o.UpdateStatus == 3).FirstOrDefault();
                if (DelModels != null)
                {
                    //删除
                     UserModels.UsersInfoModel.GoodAtWhatModels.Where(o => o.GoodAtWhatID == DelModels.GoodAtWhatID).FirstOrDefault().UpdateStatus = 3;
                    //插入buffer
                     GenericList.InsertBuffer(UserModels, DelModels);
                    //DelModels.UpdateStatus = 3;
                }
            }
            return true;
        }

        public bool Register(UsersModel UserModel)
        {
            UserModel.UpdateStatus = 1;
            //插入buffer
            GenericList.InsertBuffer(null, UserModel);
            //因为UserId 是0 ，所以不添加到缓存中，当重新获取时将被从数据库中获取并存入缓存
            //GenericList.CacheModelObj.UserModellist.Add(UserModel);
            return true;
        }

        public UsersModel UpdateUsersModel(UsersModel UserModel)
        {
            var UserModels = GetUserInfo(UserModel.ID);
            UserModels.CompanyID = UserModel.CompanyID;
            UserModels.LastLoginTime = UserModel.LastLoginTime;
            UserModels.LoginIP = UserModel.LoginIP;
            UserModels.Passwords = UserModel.Passwords;
            UserModels.Status = UserModel.Status;
            UserModels.UsersInfoID = UserModel.UsersInfoID; 
            UserModels.UserTypeID = UserModel.UserTypeID;
            //UserModels.IdeaModelList = UserModel.IdeaModelList;
            //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
            UserModels.UpdateStatus = 2;
            //插入buffer
            GenericList.InsertBuffer(null, UserModels);
            return UserModels;
        }
    }
}
