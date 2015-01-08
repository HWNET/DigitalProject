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
    public class FolderService
    {
        #region IsModelExist
        private bool IsModelExist(FolderModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.FolderModels.Find(Model.FolderID);
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region FolderInsert
        public FolderModel FolderInsert(FolderModel Model)
        {
            using (var db = new CommunicationContext())
            {
                if (!IsModelExist(Model))
                {
                    Model = db.FolderModels.Add(Model);
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

        #region FolderUpdate
        public FolderModel FolderUpdate(FolderModel Model)
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
                    return new FolderModel();
                }
            }
        }
        #endregion

        #region FolderQueryById
        public FolderModel FolderQueryById(int FolderId)
        {
            using (var db = new CommunicationContext())
            {
                return db.FolderModels.Find(FolderId);
            }
        }
        #endregion

        #region FolderQueryByName
        public FolderModel FolderQueryByName(string FolderName)
        {
            using (var db = new CommunicationContext())
            {

                var model = db.FolderModels.Where(o => o.FolderName == FolderName).SingleOrDefault();
                return model;

            }
        }
        #endregion

        #region FolderQueryByNameCode
        public FolderModel FolderQueryByNameCode(string FolderNameCode)
        {
            using (var db = new CommunicationContext())
            {

                var model = db.FolderModels.Where(o => o.FolderNameCode == FolderNameCode).SingleOrDefault();
                return model;

            }
        }
        #endregion

        #region FolderDeleteById
        public bool FolderDeleteById(int FolderId)
        {
            using (var db = new CommunicationContext())
            {

                var model = db.FolderModels.Find(FolderId);
                if (model != null)
                {
                    db.FolderModels.Remove(model);
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

        #region FolderDeleteByCompany
        public bool FolderDeleteByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                var model = db.FolderModels.Where(o => o.CompanyID == CompanyId);
                if (model != null)
                {
                    db.FolderModels.RemoveRange(model);
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

        #region FolderQueryList
        public List<FolderModel> FolderQueryList()
        {
            using (var db = new CommunicationContext())
            {

                return db.FolderModels.ToList();

            }
        }
        #endregion

        #region FolderQueryListByCompany
        public List<FolderModel> FolderQueryListByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                return db.FolderModels.Where(o => o.CompanyID == CompanyId).ToList();
            }
        }
        #endregion
    }
}
