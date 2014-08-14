using Digital.Contact.DAL;
using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.BLL
{
    public class TempColumnService : IBaseService<TempColumnModel>
    {
        public IList<TempColumnModel> PageList(int pageIndex, int pageSize, out int TotalCount, out int PageCount)
        {
            using (var db = new CommunicationContext())
            {
                var Modelist = db.TempColumnModels;
                TotalCount = Modelist.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                return Modelist.OrderBy(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IList<TempColumnModel> PageList<S>(int pageIndex, int pageSize, out int TotalCount, out int PageCount,
            Func<TempColumnModel, bool> whereLambda, bool isAsc, Func<TempColumnModel, S> orderByLambda)
        {
            using (var db = new CommunicationContext())
            {
                IEnumerable<TempColumnModel> tempData = null;
                if (whereLambda != null)
                {
                    tempData = db.Set<TempColumnModel>().Where<TempColumnModel>(whereLambda);
                }
                else
                {
                    tempData = db.Set<TempColumnModel>();
                }
                TotalCount = tempData.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                if (isAsc)
                {
                    tempData = tempData.OrderBy<TempColumnModel, S>(orderByLambda).
                          Skip<TempColumnModel>(pageSize * (pageIndex - 1)).
                          Take<TempColumnModel>(pageSize).AsQueryable();
                }
                else
                {
                    tempData = tempData.OrderByDescending<TempColumnModel, S>(orderByLambda).
                         Skip<TempColumnModel>(pageSize * (pageIndex - 1)).
                         Take<TempColumnModel>(pageSize).AsQueryable();
                }
                return tempData.AsQueryable().ToList();
            }
        }

        public TempColumnModel Find(int? Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.TempColumnModels.Find(Id);
                }
                catch (Exception ex)
                {
                    //log
                    return new TempColumnModel();
                }
            }
        }


        public bool Edit(TempColumnModel TempColumnModels)
        {
            using (var db = new CommunicationContext())
            {
                if (TempColumnModels != null && TempColumnModels.Id != 0)
                {
                    db.Entry(TempColumnModels).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    db.TempColumnModels.Add(TempColumnModels);
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public bool Delete(int Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    TempColumnModel TempColumnModels = db.TempColumnModels.Find(Id);
                    db.TempColumnModels.Remove(TempColumnModels);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    //log
                    return false;
                }
            }
        }
    }
}
