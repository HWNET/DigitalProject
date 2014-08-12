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
    public class UsersService : IBaseService<UsersModel>
    {
     

        public UsersService()
        {
           
        }


        public ClaimsIdentity CreateIdentity(UsersModel user, string authenticationType)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            _identity.AddClaim(new Claim("http://www.yy2xx.com", "longdream"));
            return _identity;
        }

        public bool Login(string UserName, string Password)
        {
            using (var db = new CommunicationContext())
            {
                var User=db.UsersModels.Where(o => o.Name == UserName && o.Passwords == Password).FirstOrDefault();
                if (User!=null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public UsersModel FindByName(string UserName)
        {
            using (var db = new CommunicationContext())
            {
                var User = db.UsersModels.Where(o => o.Name == UserName).FirstOrDefault();
                if (User != null)
                {
                    return User;
                }
                else
                {
                    return null;
                }
            }
        }
        

        public bool IsLogin(UsersModel Users)
        {
            //check user login
            return true;
        }

        public IList<UsersModel> PageList(int pageIndex, int pageSize, out int TotalCount, out int PageCount)
        {
            using (var db = new CommunicationContext())
            {
                var Modelist = db.UsersModels;
                TotalCount = Modelist.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                return Modelist.OrderBy(o => o.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IList<UsersModel> PageList<S>(int pageIndex, int pageSize, out int TotalCount, out int PageCount, 
            Func<UsersModel, bool> whereLambda, bool isAsc, Func<UsersModel, S> orderByLambda)
        {
            using (var db = new CommunicationContext())
            {
                IEnumerable<UsersModel> tempData = null;
                if (whereLambda != null)
                {
                    tempData = db.Set<UsersModel>().Where<UsersModel>(whereLambda);
                }
                else
                {
                    tempData = db.Set<UsersModel>();
                }
                TotalCount = tempData.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                if (isAsc)
                {
                    tempData = tempData.OrderBy<UsersModel, S>(orderByLambda).
                          Skip<UsersModel>(pageSize * (pageIndex - 1)).
                          Take<UsersModel>(pageSize).AsQueryable();
                }
                else
                {
                    tempData = tempData.OrderByDescending<UsersModel, S>(orderByLambda).
                         Skip<UsersModel>(pageSize * (pageIndex - 1)).
                         Take<UsersModel>(pageSize).AsQueryable();
                }
                return tempData.AsQueryable().ToList();
            }
        }  

        public UsersModel Find(int? Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.UsersModels.Find(Id);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new UsersModel();
                }
            }
        }


        public bool Edit(UsersModel usersmodel)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    if (usersmodel != null && usersmodel.ID != 0)
                    {
                        db.Entry(usersmodel).State = EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        db.UsersModels.Add(usersmodel);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch(DbEntityValidationException dbEx )
                {
                    return false;
                }
            }
        }

        public bool Delete(int Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    UsersModel usersmodel = db.UsersModels.Find(Id);
                    db.UsersModels.Remove(usersmodel);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    Logger.Error(ex);
                    return false;
                }
            }
        }

        

    }
}
