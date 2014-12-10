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
    public class CasesService
    {
        #region IsModelExist
        private bool IsModelExist(CasesModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.CasesModels.Find(Model.CasesID);
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region CasesInsert
        public CasesModel CasesInsert(CasesModel Model)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    if (!IsModelExist(Model))
                    {
                        Model = db.CasesModels.Add(Model);
                        db.SaveChanges();
                        return Model;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return null;
                }
            }
        }
        #endregion

        #region CasesUpdate
        public CasesModel CasesUpdate(CasesModel Model)
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
                        return new CasesModel();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new CasesModel();
                }
            }
        }
        #endregion

        #region CasesQueryById
        public CasesModel CasesQueryById(int CasesId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.CasesModels.Find(CasesId);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new CasesModel();
                }
            }
        }
        #endregion

        #region CasesQueryByName
        public CasesModel CasesQueryByName(string CasesName)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var model = db.CasesModels.Where(o => o.CasesName == CasesName).SingleOrDefault();
                    return model;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new CasesModel();
                }
            }
        }
        #endregion

        #region CasesDeleteById
        public bool CasesDeleteById(int CasesId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var model = db.CasesModels.Find(CasesId);
                    if (model != null)
                    {
                        db.CasesModels.Remove(model);
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

        #region CasesDeleteByCategory
        public bool CasesDeleteByCategory(int CasesCategoryId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var model = db.CasesModels.Where(o => o.CasesCategoryID == CasesCategoryId);
                    if (model != null)
                    {
                        db.CasesModels.RemoveRange(model);
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

        #region CasesDeleteByCompany
        public bool CasesDeleteByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var CategoryList = db.CasesCategoryModels.Where(o => o.CompanyID == CompanyId).ToList();
                    List<CasesModel> CasesList = new List<CasesModel>();
                    CategoryList.ForEach(c => CasesList.Add(db.CasesModels.Where(o=>o.CasesCategoryID==c.CasesCategoryID).SingleOrDefault()));
                    if (CasesList != null)
                    {
                        db.CasesModels.RemoveRange((IEnumerable<CasesModel>)CasesList);
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

        #region CasesQueryList
        public List<CasesModel> CasesQueryList()
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.CasesModels.ToList();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return null;
                }
            }
        }
        #endregion

        #region CasesQueryListByCategory
        public List<CasesModel> CasesQueryListByCategory(int CasesCategoryId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var CasesList = db.CasesModels.Where(o => o.CasesCategoryID == CasesCategoryId).ToList();
                    return CasesList;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return null;
                }
            }
        }
        #endregion

        #region CasesQueryListByCompany
        public List<CasesModel> CasesQueryListByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var CategoryList = db.CasesCategoryModels.Where(o => o.CompanyID == CompanyId).ToList();
                    List<CasesModel> CasesList = new List<CasesModel>();
                    CategoryList.ForEach(c => CasesList.Add(db.CasesModels.Where(o => o.CasesCategoryID == c.CasesCategoryID).SingleOrDefault()));
                    return CasesList;
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
