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
    public class CertificateService
    {
        #region IsModelExist
        private bool IsModelExist(CertificateModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.CertificateModels.Find(Model.CertificateID);
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region CertificateInsert
        public CertificateModel CertificateInsert(CertificateModel Model)
        {
            using (var db = new CommunicationContext())
            {
                if (!IsModelExist(Model))
                {
                    Model = db.CertificateModels.Add(Model);
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

        #region CertificateUpdate
        public CertificateModel CertificateUpdate(CertificateModel Model)
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
                    return new CertificateModel();
                }
            }
        }
        #endregion

        #region CertificateQueryById
        public CertificateModel CertificateQueryById(int CertificateId)
        {
            using (var db = new CommunicationContext())
            {
                return db.CertificateModels.Find(CertificateId);
            }
        }
        #endregion

        #region CertificateQueryByName
        public CertificateModel CertificateQueryByName(string CertificateName)
        {
            using (var db = new CommunicationContext())
            {

                var model = db.CertificateModels.Where(o => o.CertificateName == CertificateName).SingleOrDefault();
                return model;

            }
        }
        #endregion

        #region CertificateDeleteById
        public bool CertificateDeleteById(int CertificateId)
        {
            using (var db = new CommunicationContext())
            {

                var model = db.CertificateModels.Find(CertificateId);
                if (model != null)
                {
                    db.CertificateModels.Remove(model);
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

        #region CertificateDeleteByCompany
        public bool CertificateDeleteByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                var model = db.CertificateModels.Where(o => o.CompanyID == CompanyId);
                if (model != null)
                {
                    db.CertificateModels.RemoveRange(model);
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

        #region CertificateQueryList
        public List<CertificateModel> CertificateQueryList()
        {
            using (var db = new CommunicationContext())
            {

                return db.CertificateModels.ToList();

            }
        }
        #endregion

        #region CertificateQueryListByCompany
        public List<CertificateModel> CertificateQueryListByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                return db.CertificateModels.Where(o => o.CompanyID == CompanyId).ToList();
            }
        }
        #endregion
    }
}
