using Digital.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Service.Implements
{
    public partial class Service : IConfigService
    {
        public List<Digital.Contact.Models.MenuModel> GetMenuList()
        {
            List<Digital.Contact.Models.MenuModel> MenuList = new List<Contact.Models.MenuModel>();
            if (GenericList.CacheModelObj != null)
            {
                return GenericList.CacheModelObj.MenuModellist;
            }
            else
            {
                return null;
            }
        }

        public List<Digital.Contact.Models.UsersModel> GetUserList()
        {
            Digital.Contact.BLL.UsersService UserService = new Contact.BLL.UsersService();
            return UserService.GetAllUserList();
        }
    }
    
}
