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
    public partial class Service : ICasesService
    {
        #region CasesInsert
        public CasesModel CasesInsert(CasesModel Model)
        {
            try
            {
                Model.UpdateStatus = 1;
                CasesService CasesService = new CasesService();
                //Insert DB
                var Result = CasesService.CasesInsert(Model);
                //Insert Cache
                //GenericList.CacheModelObj.CompanyModellist.Add(Model);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region CasesUpdate
        public CasesModel CasesUpdate(CasesModel Model)
        {
            try
            {
                var CasesModel = CasesQueryById(Model.CasesID);

                if (CasesModel != null)
                {
                    #region Instance Model

                    #endregion
                    #region UI Models

                    #endregion

                    //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                    CasesModel.UpdateStatus = 2;

                    //directly update to DB
                    CasesService CasesService = new CasesService();
                    CasesService.CasesUpdate(Model);
                    return Model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region CasesQueryById
        public CasesModel CasesQueryById(int CasesId)
        {
            try
            {
                //get model from DB
                CasesService CasesService = new CasesService();
                var CasesModel = CasesService.CasesQueryById(CasesId);
                return CasesModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion
        #region CasesQueryByName
        public CasesModel CasesQueryByName(string CasesName)
        {
            try
            {
                //get model from DB
                CasesService CasesService = new CasesService();
                var CasesModel = CasesService.CasesQueryByName(CasesName);
                return CasesModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region CasesDeleteById
        public bool CasesDeleteById(int CasesId)
        {
            try
            {
            //get model from DB
            CasesService CasesService = new CasesService();
            var Result = CasesService.CasesDeleteById(CasesId);
            return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return false;
            }
        }

        public bool CasesDeleteByIds(string CasesId)
        {
            try
            {

                //get model from DB
                CasesService CasesService = new CasesService();
                var CasesIds=CasesId.Trim(',').Split(',');
                foreach(var  Cases in CasesIds)
                {
                    CasesService.CasesDeleteById(Cases.ToInt());
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return false;
            }
        }
        #endregion

        #region CasesDeleteByCategory
        public bool CasesDeleteByCategory(int CasesCategoryId)
        {
            try
            {
                //get model from DB
                CasesService CasesService = new CasesService();
                var Result = CasesService.CasesDeleteByCategory(CasesCategoryId);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region CasesDeleteByCompany
        public bool CasesDeleteByCompany(int CompanyId)
        {
            try
            {
                CasesService CasesService = new CasesService();
                var CategoryList = CasesService.CasesQueryListByCompany(CompanyId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                //CategoryModel.UpdateStatus = 3;

                if (CategoryList != null)
                {
                    var Result = CasesService.CasesDeleteByCompany(CompanyId);
                    return Result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region CasesQueryList
        public List<CasesModel> CasesQueryList()
        {
            try
            {
                CasesService CasesService = new CasesService();
                var CasesList = CasesService.CasesQueryList();
                return CasesList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region CasesQueryListByCategory
        public List<CasesModel> CasesQueryListByCategory(int CasesCategoryId)
        {
            try
            {
                CasesService CasesService = new CasesService();
                var CasesList = CasesService.CasesQueryListByCategory(CasesCategoryId);
                return CasesList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region CasesQueryListByCompany
        public List<CasesModel> CasesQueryListByCompany(int CompanyId)
        {
            try
            {
                CasesService CasesService = new CasesService();
                var CasesList = CasesService.CasesQueryListByCompany(CompanyId);
                return CasesList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Cases", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion


       
    }
}
