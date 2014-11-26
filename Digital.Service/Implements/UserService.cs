﻿using Digital.Service.Interfaces;
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
            var User = GenericList.CacheModelObj.UserModellist.Where(o => o.Name == UserName && o.Passwords == Password).FirstOrDefault();
            if (User != null)
            {
                return User;
            }
            else
            {
                return null;
            }
        }

        public UsersModel GetUserInfoByName(string UserName)
        {
            UsersModel User = GenericList.CacheModelObj.UserModellist.Where(o => o.Name == UserName).FirstOrDefault();
            if (User != null)
            {
                return User;
            }
            else
            {
                return null;
            }
        }

        public UsersModel GetUserInfo(int UserId)
        {
            UsersModel User = GenericList.CacheModelObj.UserModellist.Where(o => o.ID == UserId).FirstOrDefault();
            if (User != null)
            {
                return User;
            }
            else
            {
                return null;
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
            GenericList.CacheModelObj.UserModellist.Where(o => o.ID == UserModel.ID).FirstOrDefault().UsersInfoModel = CurrentUserInfo;
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
                    UserModels.UsersInfoModel.GoodAtWhatModels.Add(AddModels);
                    GenericList.InsertBuffer(UserModels, AddModels);
                }
                var DelModels = UserModel.UsersInfoModel.GoodAtWhatModels.Where(o => o.UpdateStatus == 3).FirstOrDefault();
                if (DelModels != null)
                {
                    //删除
                     UserModels.UsersInfoModel.GoodAtWhatModels.Where(o => o.GoodAtWhatID == DelModels.GoodAtWhatID).FirstOrDefault().UpdateStatus = 3;
                     GenericList.InsertBuffer(UserModels, DelModels);
                    //DelModels.UpdateStatus = 3;
                }
            }
            return true;
        }

        public bool Register(UsersModel UserModel)
        {
            UserModel.UpdateStatus = 1;
            GenericList.InsertBuffer(null, UserModel);
            GenericList.CacheModelObj.UserModellist.Add(UserModel);
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
            GenericList.InsertBuffer(null, UserModels);
            return UserModels;
        }
    }
}
