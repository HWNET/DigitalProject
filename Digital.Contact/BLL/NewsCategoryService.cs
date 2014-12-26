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
    public class NewsCategoryService
    {
        #region IsModelExist
        private bool IsModelExist(NewsCategoryModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.NewsCategoryModels.Find(Model.NewsCategoryID);
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region NewsCategoryInsert
        public NewsCategoryModel NewsCategoryInsert(NewsCategoryModel Model)
        {
            using (var db = new CommunicationContext())
            {

                    if (!IsModelExist(Model))
                    {
                        Model = db.NewsCategoryModels.Add(Model);
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

        #region NewsCategoryUpdate
        public NewsCategoryModel NewsCategoryUpdate(NewsCategoryModel Model)
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
                        return new NewsCategoryModel();
                    }

            }
        }
        #endregion

        #region NewsCategoryQueryById
        public NewsCategoryModel NewsCategoryQueryById(int NewsCategoryId)
        {
            using (var db = new CommunicationContext())
            {

                    return db.NewsCategoryModels.Find(NewsCategoryId);

            }
        }
        #endregion

        #region NewsCategoryQueryByName
        public NewsCategoryModel NewsCategoryQueryByName(string NewsCategoryName)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.NewsCategoryModels.Where(o => o.NewsCategoryName == NewsCategoryName).SingleOrDefault();
                    return model;

            }
        }
        #endregion

        #region NewsCategoryDeleteById
        public bool NewsCategoryDeleteById(int NewsCategoryId)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.NewsCategoryModels.Find(NewsCategoryId);
                    if (model != null)
                    {
                        db.NewsCategoryModels.Remove(model);
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

        #region NewsCategoryDeleteByCompany
        public bool NewsCategoryDeleteByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.NewsCategoryModels.Where(o => o.CompanyID == CompanyId);
                    if (model != null)
                    {
                        db.NewsCategoryModels.RemoveRange(model);
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

        #region NewsCategoryQueryList
        public List<NewsCategoryModel> NewsCategoryQueryList()
        {
            using (var db = new CommunicationContext())
            {

                    return db.NewsCategoryModels.ToList();

            }
        }
        #endregion

        #region NewsCategoryQueryListByCompany
        public List<NewsCategoryModel> NewsCategoryQueryListByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                    return db.NewsCategoryModels.Where(o => o.CompanyID == CompanyId).ToList();

            }
        }
        #endregion
    }
}
