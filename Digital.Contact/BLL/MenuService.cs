using Digital.Common.Logging;
using Digital.Contact.DAL;
using Digital.Contact.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.BLL
{
    public class MenuService
    {
        public IList<MenuModel> MenuList(int UserId)
        {
            using (var db = new CommunicationContext())
            {
                return db.MenuModel.ToList();
            }
        }

        public MenuModel GetMenuModel(int MenuId)
        {
            using (var db = new CommunicationContext())
            {
                return db.MenuModel.Find(MenuId);
            }
        }
    }
}
