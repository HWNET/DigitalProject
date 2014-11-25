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

        public static void UpdateUserALLTable()
        {
            List<Digital.Contact.Models.UsersModel> UserModellist=null;
              Digital.Contact.BLL.UsersService UserService = new Contact.BLL.UsersService();
              Digital.Contact.BLL.UsersInfoService UserInfoService = new Contact.BLL.UsersInfoService();
            if(GenericList.CacheModelObj!=null)
            {
               UserModellist= GenericList.CacheModelObj.UserModellist;
            }
            if (UserModellist != null)
            {
                foreach (var UserModel in UserModellist)
                {
                    //Update UserModel
                    if (UserModel == null || UserModel.ID == 0 || UserModel.UpdateStatus == 2)
                    {
                        UserModel.UpdateStatus = 0;
                        UserService.Edit(UserModel);
                    }
                    //Update UserInfo
                    if (UserModel.UsersInfoModel == null || UserModel.UsersInfoModel.UsersInfoID == 0 || UserModel.UsersInfoModel.UpdateStatus == 2)
                    {
                        UserModel.UsersInfoModel.UpdateStatus = 0;
                        UserInfoService.Edit(UserModel.UsersInfoModel);
                    }
                    //Update GoodAt
                    UserService.UpdateGoodAt(UserModel.UsersInfoModel.GoodAtWhatModels);
                    //delete gooda
                    var DeleteList=UserModel.UsersInfoModel.GoodAtWhatModels.Where(o=>o.UpdateStatus==3).ToList();
                    foreach (var DelGoodat in DeleteList)
                    {
                        UserModel.UsersInfoModel.GoodAtWhatModels.Remove(DelGoodat);
                    }
                }
                foreach (var UserModel in UserModellist)
                {
                    UserModel.UpdateStatus = 0;
                    UserModel.UsersInfoModel.UpdateStatus = 0;
                    foreach (var Goodat in UserModel.UsersInfoModel.GoodAtWhatModels)
                    {
                        Goodat.UpdateStatus = 0;
                    }
                }
            }
           
        }
    }
}
