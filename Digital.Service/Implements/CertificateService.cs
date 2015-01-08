using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;
using Digital.Contact.BLL;
using Digital.Common.Utilities;
using Digital.Common.Logging;

namespace Digital.Service.Implements
{
    public partial class Service : ICertificateService
    {
        #region CertificateInsert
        public CertificateModel CertificateInsert(CertificateModel Model)
        {
            try
            {
                CertificateService CertificateService = new CertificateService();
                //Insert DB
                var Result = CertificateService.CertificateInsert(Model);
                //Insert Cache
                //GenericList.CacheModelObj.CompanyModellist.Add(Model);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Certificate", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region CertificateUpdate
        public CertificateModel CertificateUpdate(CertificateModel Model)
        {
            try
            {
                var CertificateModel = CertificateQueryById(Model.CertificateID);

                if (CertificateModel != null)
                {
                    #region Instance Model

                    #endregion
                    #region UI Models

                    #endregion

                    //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 

                    //directly update to DB
                    CertificateService CertificateService = new CertificateService();
                    CertificateService.CertificateUpdate(Model);
                    return Model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Certificate", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region CertificateQueryById
        public CertificateModel CertificateQueryById(int CertificateId)
        {
            try
            {
                //get model from DB
                CertificateService CertificateService = new CertificateService();
                var CertificateModel = CertificateService.CertificateQueryById(CertificateId);
                return CertificateModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Certificate", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region CertificateQueryByName
        public CertificateModel CertificateQueryByName(string CertificateName)
        {
            try
            {
                //get model from DB
                CertificateService CertificateService = new CertificateService();
                var CertificateModel = CertificateService.CertificateQueryByName(CertificateName);
                return CertificateModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Certificate", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region CertificateDeleteById
        public bool CertificateDeleteById(int CertificateId)
        {
            try
            {
                CertificateService CertificateService = new CertificateService();
                var CertificateModel = CertificateQueryById(CertificateId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
               

                if (CertificateModel != null)
                {
                    var Result = CertificateService.CertificateDeleteById(CertificateId);
                    return Result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Certificate", MessageLevel.Level2, ex.ToString());
                return false;
            }
        }
        #endregion

        #region CertificateDeleteByIds
        public bool CertificateDeleteByIds(string CertificateIds)
        {
            try
            {
                CertificateService CertificateService = new CertificateService();
                var TempCertificateIds = CertificateIds.Trim(',').Split(',');
                foreach (var ItemId in TempCertificateIds)
                {
                    CertificateService.CertificateDeleteById(ItemId.ToInt());
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Certificate", MessageLevel.Level2, ex.ToString());
                return false;
            }
        }
        #endregion

        #region CertificateDeleteByCompany
        public bool CertificateDeleteByCompany(int CompanyId)
        {
            try
            {
                CertificateService CertificateService = new CertificateService();
                var CertificateList = CertificateService.CertificateQueryListByCompany(CompanyId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                //CategoryModel.UpdateStatus = 3;

                if (CertificateList != null)
                {
                    var Result = CertificateService.CertificateDeleteByCompany(CompanyId);
                    return Result;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                logger.WriteInfo("Certificate", MessageLevel.Level2, ex.ToString());
                return false;
            }
        }
        #endregion

        #region CertificateQueryList
        public List<CertificateModel> CertificateQueryList()
        {
            try
            {
                CertificateService CertificateService = new CertificateService();
                var CertificateList = CertificateService.CertificateQueryList();
                return CertificateList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Certificate", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region CertificateQueryListByCompany
        public List<CertificateModel> CertificateQueryListByCompany(int CompanyId)
        {
            try
            {
                CertificateService CertificateService = new CertificateService();
                var CertificateList = CertificateService.CertificateQueryListByCompany(CompanyId);
                return CertificateList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Certificate", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion
    }
}
