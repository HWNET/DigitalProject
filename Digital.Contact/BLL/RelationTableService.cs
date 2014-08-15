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
    public class RelationTableService : IBaseService<RelationTableModels>
    {
        public IList<RelationTableModels> PageList(int pageIndex, int pageSize, out int TotalCount, out int PageCount)
        {
            using (var db = new CommunicationContext())
            {
                var Modelist = db.RelationTableModels;
                TotalCount = Modelist.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                return Modelist.OrderBy(o => o.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IList<RelationTableModels> PageList<S>(int pageIndex, int pageSize, out int TotalCount, out int PageCount, Func<RelationTableModels, bool> whereLambda, bool isAsc, Func<RelationTableModels, S> orderByLambda)
        {
            using (var db = new CommunicationContext())
            {
                IEnumerable<RelationTableModels> tempData = null;
                if (whereLambda != null)
                {
                    tempData = db.Set<RelationTableModels>().Where<RelationTableModels>(whereLambda);
                }
                else
                {
                    tempData = db.Set<RelationTableModels>();
                }
                TotalCount = tempData.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                if (isAsc)
                {
                    tempData = tempData.OrderBy<RelationTableModels, S>(orderByLambda).
                          Skip<RelationTableModels>(pageSize * (pageIndex - 1)).
                          Take<RelationTableModels>(pageSize).AsQueryable();
                }
                else
                {
                    tempData = tempData.OrderByDescending<RelationTableModels, S>(orderByLambda).
                         Skip<RelationTableModels>(pageSize * (pageIndex - 1)).
                         Take<RelationTableModels>(pageSize).AsQueryable();
                }
                return tempData.AsQueryable().ToList();
            }
        }

        public RelationTableModels Find(int? Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.RelationTableModels.Find(Id);
                }
                catch (Exception ex)
                {
                    //log
                    return new RelationTableModels();
                }
            }
        }

        public bool Edit(RelationTableModels model)
        {
            using (var db = new CommunicationContext())
            {
                if (model != null && model.ID != 0)
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    db.RelationTableModels.Add(model);
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
                    RelationTableModels relationTableModels = db.RelationTableModels.Find(Id);
                    db.RelationTableModels.Remove(relationTableModels);
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
