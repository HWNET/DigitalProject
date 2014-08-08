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
    public class TemplateService : IBaseService<TemplateModels>
    {
        public TemplateService()
        {
           
        }
        public IList<TemplateModels> PageList(int pageIndex, int pageSize, out int TotalCount, out int PageCount)
        {
            using (var db = new CommunicationContext())
            {
                var Modelist = db.TemplateModels;
                TotalCount = Modelist.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                return Modelist.OrderBy(o => o.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IList<TemplateModels> PageList<S>(int pageIndex, int pageSize, out int TotalCount, out int PageCount, 
            Func<TemplateModels, bool> whereLambda, bool isAsc, Func<TemplateModels, S> orderByLambda)
        {
            using (var db = new CommunicationContext())
            {
                IEnumerable<TemplateModels> tempData = null;
                if (whereLambda != null)
                {
                    tempData = db.Set<TemplateModels>().Where<TemplateModels>(whereLambda);
                }
                else
                {
                    tempData = db.Set<TemplateModels>();
                }
                TotalCount = tempData.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                if (isAsc)
                {
                    tempData = tempData.OrderBy<TemplateModels, S>(orderByLambda).
                          Skip<TemplateModels>(pageSize * (pageIndex - 1)).
                          Take<TemplateModels>(pageSize).AsQueryable();
                }
                else
                {
                    tempData = tempData.OrderByDescending<TemplateModels, S>(orderByLambda).
                         Skip<TemplateModels>(pageSize * (pageIndex - 1)).
                         Take<TemplateModels>(pageSize).AsQueryable();
                }
                return tempData.AsQueryable().ToList();
            }
        }  

        public TemplateModels Find(int? Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.TemplateModels.Find(Id);
                }
                catch (Exception ex)
                {
                    //log
                    return new TemplateModels();
                }
            }
        }


        public bool Edit(TemplateModels templateModels)
        {
            using (var db = new CommunicationContext())
            {
                if (templateModels != null && templateModels.ID != 0)
                {
                    db.Entry(templateModels).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    db.TemplateModels.Add(templateModels);
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
                    TemplateModels templateModels = db.TemplateModels.Find(Id);
                    db.TemplateModels.Remove(templateModels);
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
