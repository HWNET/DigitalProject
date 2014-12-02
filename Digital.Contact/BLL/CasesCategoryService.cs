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
    public class CasesCategoryService
    {
        #region IsModelExist
        private bool IsModelExist(CasesCategoryModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.CasesCategoryModels.Find(Model.CasesCategoryID);
                if (item==null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region CasesCategoryInsert
        public bool CasesCategoryInsert(CasesCategoryModel Model)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    if (!IsModelExist(Model))
                    {
                        db.CasesCategoryModels.Add(Model);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return false;
                }
            }
        }
        #endregion

        #region CasesCategoryUpdate
        public CasesCategoryModel CasesCategoryUpdate(CasesCategoryModel Model)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    if (IsModelExist(Model))
                    {
                        db.Entry(Model).State = EntityState.Modified;
                        db.SaveChanges();
                        return Model;
                    }
                    else
                    {
                        return new CasesCategoryModel();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new CasesCategoryModel();
                }
            }
        }
        #endregion

        #region CasesCategoryQueryById
        public CasesCategoryModel CasesCategoryQueryById(int CasesCategoryId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.CasesCategoryModels.Find(CasesCategoryId);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new CasesCategoryModel();
                }
            }
        }
        #endregion

        #region CasesCategoryQueryByName
        public CasesCategoryModel CasesCategoryQueryByName(string CasesCategoryName)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var model = db.CasesCategoryModels.Where(o => o.CasesCategoryName == CasesCategoryName).SingleOrDefault();
                    return model;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new CasesCategoryModel();
                }
            }
        }
        #endregion

        #region CasesCategoryDeleteById
        public bool CasesCategoryDeleteById(int CasesCategoryId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var model = db.CasesCategoryModels.Find(CasesCategoryId);
                    if (model != null)
                    {
                        db.CasesCategoryModels.Remove(model);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return false;
                }
            }
        }
        #endregion

        #region CasesCategoryDeleteByCompany
        public bool CasesCategoryDeleteByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var model = db.CasesCategoryModels.Where(o=>o.CompanyID==CompanyId);
                    if (model != null)
                    {
                        db.CasesCategoryModels.RemoveRange(model);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return false;
                }
            }
        }
        #endregion

        #region CasesCategoryQueryList
        public List<CasesCategoryModel> CasesCategoryQueryList()
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.CasesCategoryModels.ToList();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return null;
                }
            }
        }
        #endregion

        #region CasesCategoryQueryListByCompany
        public List<CasesCategoryModel> CasesCategoryQueryListByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.CasesCategoryModels.Where(o=>o.CompanyID==CompanyId).ToList();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return null;
                }
            }
        }
        #endregion
    }
}
