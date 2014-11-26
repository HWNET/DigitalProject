using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Service
{
    public class UpdateUser
    {
        public UpdateUser()
        {

        }


        public static void UpdateGoodAtWhatModel(UsersModel User, GoodAtWhatModel goodat)
        {
            Digital.Contact.BLL.UsersService UserService = new Contact.BLL.UsersService();
            UserService.UpdateGoodAt(goodat);
            if (goodat.UpdateStatus == 3)
            {
                var UserModel = GenericList.CacheModelObj.UserModellist[User.ID];
                var GoodAtWhat = UserModel.UsersInfoModel.GoodAtWhatModels.Where(o => o.GoodAtWhatID == goodat.GoodAtWhatID).FirstOrDefault();
                if (GoodAtWhat != null)
                {
                    UserModel.UsersInfoModel.GoodAtWhatModels.Remove(GoodAtWhat);
                }
            }
        }

        public static void UpdateUserModel(UsersModel User)
        {
            Digital.Contact.BLL.UsersService UserService = new Contact.BLL.UsersService();
            var UserModel = GenericList.CacheModelObj.UserModellist[User.ID];
            UserModel.UpdateStatus = 0;
            UserService.Edit(UserModel);
           
        }

        public static void UpdateUserInfoModel(UsersModel User,UsersInfoModel UserInfo)
        {
            var UserModel = GenericList.CacheModelObj.UserModellist[User.ID];
            Digital.Contact.BLL.UsersInfoService UserInfoService = new Contact.BLL.UsersInfoService();
            UserModel.UsersInfoModel.UpdateStatus = 0;
            UserInfoService.Edit(UserInfo);
           
        }

        //public static void UpdateUserALLTable(BufferFormat TempBuffer)
        //{
        //    List<Digital.Contact.Models.UsersModel> UserModellist = null;
        //    Digital.Contact.BLL.UsersService UserService = new Contact.BLL.UsersService();
        //    Digital.Contact.BLL.UsersInfoService UserInfoService = new Contact.BLL.UsersInfoService();
        //    if (GenericList.MessageBuffer != null)
        //    {
        //         BufferFormat Buffers = TempBuffer as BufferFormat;
        //        if (Buffers.MainObject as GoodAtWhatModel != null)
        //            {
        //                var TeTempBuffer = Buffers.MainObject as GoodAtWhatModel;
        //                var UserModel=Buffers.RootObject as UsersModel;
        //                UserService.UpdateGoodAt(TeTempBuffer);
        //                if (TeTempBuffer.UpdateStatus == 3)
        //                {
        //                    GenericList.CacheModelObj.UserModellist.Where(o=>o.ID==tt)
        //                    UserModel.UsersInfoModel.GoodAtWhatModels.Remove(TeTempBuffer);
        //                }
        //            }
        //            if (TempBuffer as UsersInfoModel != null)
        //            {
        //                var TeTempBuffer = TempBuffer as UsersInfoModel;
        //                UserInfoService.Edit(TeTempBuffer);
        //                UserModel.UsersInfoModel.UpdateStatus = 0;

        //            }
        //            if (TempBuffer as UsersModel != null)
        //            {
        //                var TeTempBuffer = TempBuffer as UsersModel;
        //                UserService.Edit(UserModel);
        //                UserModel.UpdateStatus = 0;
        //            }
        //            GenericList.MessageBuffer.Dequeue(); 


        //        var Templist = GenericList.MessageBuffer;
        //        foreach (var TempBuffer in Templist)
        //        {
        //            BufferFormat Buffers = TempBuffer as BufferFormat;
        //            if (Buffers. as GoodAtWhatModel != null)
        //            {
        //                var TeTempBuffer = TempBuffer as GoodAtWhatModel;
        //                UserService.UpdateGoodAt(TeTempBuffer);
        //                if (TeTempBuffer.UpdateStatus == 3)
        //                {
        //                    UserModel.UsersInfoModel.GoodAtWhatModels.Remove(TeTempBuffer);
        //                }
        //            }
        //            if (TempBuffer as UsersInfoModel != null)
        //            {
        //                var TeTempBuffer = TempBuffer as UsersInfoModel;
        //                UserInfoService.Edit(TeTempBuffer);
        //                UserModel.UsersInfoModel.UpdateStatus = 0;

        //            }
        //            if (TempBuffer as UsersModel != null)
        //            {
        //                var TeTempBuffer = TempBuffer as UsersModel;
        //                UserService.Edit(UserModel);
        //                UserModel.UpdateStatus = 0;
        //            }
        //            GenericList.MessageBuffer.Dequeue();
        //        }
        //    }
        //    if (GenericList.CacheModelObj != null)
        //    {
        //        UserModellist = GenericList.CacheModelObj.UserModellist;
        //    }
        //    if (UserModellist != null)
        //    {
        //        foreach (var UserModel in UserModellist)
        //        {
        //            //Update UserModel
        //            if (UserModel == null || UserModel.ID == 0 || UserModel.UpdateStatus == 2)
        //            {
        //                UserModel.UpdateStatus = 0;
        //                UserService.Edit(UserModel);
        //            }
        //            //Update GoodAt
                   
        //        }
        //    }

        //}
    }
}
