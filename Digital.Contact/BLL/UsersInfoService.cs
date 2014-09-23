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
    public class UsersInfoService : IBaseService<UsersInfoModel>
    {

        public IList<UsersInfoModel> PageList(int pageIndex, int pageSize, out int TotalCount, out int PageCount)
        {
            throw new NotImplementedException();
        }

        public IList<UsersInfoModel> PageList<S>(int pageIndex, int pageSize, out int TotalCount, out int PageCount, Func<UsersInfoModel, bool> whereLambda, bool isAsc, Func<UsersInfoModel, S> orderByLambda)
        {
            throw new NotImplementedException();
        }

        public UsersInfoModel Find(int? Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.UsersInfoModel.Find(Id);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new UsersInfoModel();
                }
            }
        }

        public bool Edit(UsersInfoModel model)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    if (model != null && model.UsersInfoID != 0)
                    {
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        db.UsersInfoModel.Add(model);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    Logger.Error(dbEx.InnerException.ToString());
                    return false;
                }
            }
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
