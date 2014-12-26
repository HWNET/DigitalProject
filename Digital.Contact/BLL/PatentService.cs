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
    public class PatentService
    {
        #region IsModelExist
        private bool IsModelExist(PatentModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.PatentModels.Find(Model.PatentID);
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region PatentInsert
        public PatentModel PatentInsert(PatentModel Model)
        {
            using (var db = new CommunicationContext())
            {

                    if (!IsModelExist(Model))
                    {
                        Model = db.PatentModels.Add(Model);
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

        #region PatentUpdate
        public PatentModel PatentUpdate(PatentModel Model)
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
                        return new PatentModel();
                    }

            }
        }
        #endregion

        #region PatentQueryById
        public PatentModel PatentQueryById(int PatentId)
        {
            using (var db = new CommunicationContext())
            {

                    return db.PatentModels.Find(PatentId);

            }
        }
        #endregion

        #region PatentQueryByName
        public PatentModel PatentQueryByName(string PatentName)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.PatentModels.Where(o => o.PatentName == PatentName).SingleOrDefault();
                    return model;

            }
        }
        #endregion

        #region PatentDeleteById
        public bool PatentDeleteById(int PatentId)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.PatentModels.Find(PatentId);
                    if (model != null)
                    {
                        db.PatentModels.Remove(model);
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

        #region PatentDeleteByCompany
        public bool PatentDeleteByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.PatentModels.Where(o => o.CompanyID == CompanyId);
                    if (model != null)
                    {
                        db.PatentModels.RemoveRange(model);
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

        #region PatentQueryList
        public List<PatentModel> PatentQueryList()
        {
            using (var db = new CommunicationContext())
            {

                    return db.PatentModels.ToList();

            }
        }
        #endregion

        #region PatentQueryListByCompany
        public List<PatentModel> PatentQueryListByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                    return db.PatentModels.Where(o => o.CompanyID == CompanyId).ToList();

            }
        }
        #endregion

        #region PatentDisable
        public bool PatentDisable(PatentModel Model)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region PatentTransfer
        public bool PatentTransfer(PatentModel Model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
