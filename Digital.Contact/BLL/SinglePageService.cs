using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Common.Logging;
using Digital.Contact.DAL;
using Digital.Contact.Models;
using System.Data.Entity;
namespace Digital.Contact.BLL
{
    public class SinglePageService
    {
        #region IsModelExist
        private bool IsModelExist(SinglePageModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.SinglePageModels.Find(Model.PageID);
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region SinglePageInsert
        public SinglePageModel SinglePageInsert(SinglePageModel Model)
        {
            using (var db = new CommunicationContext())
            {

                    if (!IsModelExist(Model))
                    {
                        Model = db.SinglePageModels.Add(Model);
                        db.SaveChanges();
                        return Model;
                    }
                    else
                    {
                        return null;
                    }

            }
        }
        #endregion

        #region SinglePageUpdate
        public SinglePageModel SinglePageUpdate(SinglePageModel Model)
        {
            using (var db = new CommunicationContext())
            {

                    if (IsModelExist(Model))
                    {
                        db.Entry(Model).State = EntityState.Modified;
                        db.SaveChanges();
                        return Model;
                    }
                    else
                    {
                        return new SinglePageModel();
                    }

            }
        }
        #endregion

        #region SinglePageQueryById
        public SinglePageModel SinglePageQueryById(int SinglePageId)
        {
            using (var db = new CommunicationContext())
            {

                    return db.SinglePageModels.Find(SinglePageId);

            }
        }
        #endregion

        #region SinglePageQueryByName
        public SinglePageModel SinglePageQueryByName(string SinglePageName)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.SinglePageModels.Where(o => o.PageTitle == SinglePageName).SingleOrDefault();
                    return model;

            }
        }
        #endregion

        #region SinglePageDeleteById
        public bool SinglePageDeleteById(int SinglePageId)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.SinglePageModels.Find(SinglePageId);
                    if (model != null)
                    {
                        db.SinglePageModels.Remove(model);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

            }
        }
        #endregion

        #region SinglePageDeleteByCompany
        public bool SinglePageDeleteByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.SinglePageModels.Where(o => o.CompanyID == CompanyId);
                    if (model != null)
                    {
                        db.SinglePageModels.RemoveRange(model);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

            }
        }
        #endregion

        #region SinglePageQueryList
        public List<SinglePageModel> SinglePageQueryList()
        {
            using (var db = new CommunicationContext())
            {

                    return db.SinglePageModels.ToList();

            }
        }
        #endregion

        #region SinglePageQueryListByCompany
        public List<SinglePageModel> SinglePageQueryListByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                    return db.SinglePageModels.Where(o => o.CompanyID == CompanyId).ToList();

            }
        }
        #endregion
    }
}
