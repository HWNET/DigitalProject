using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;
using Digital.Contact.BLL;

namespace Digital.Service.Implements
{
    public partial class Service : ICasesService
    {
        #region CasesInsert
        public bool CasesInsert(CasesModel Model)
        {
            Model.UpdateStatus = 1;
            CasesService CasesService = new CasesService();
            //Insert DB
            var Result=CasesService.CasesInsert(Model);
            //Insert Cache
            //GenericList.CacheModelObj.CompanyModellist.Add(Model);
            return Result;
        }
        #endregion

        #region CasesUpdate
        public CasesModel CasesUpdate(CasesModel Model)
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
                CasesService.CasesUpdate(CasesModel);
                return CasesModel;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region CasesQueryById
        public CasesModel CasesQueryById(int CasesId)
        {
            //get model from DB
            CasesService CasesService = new CasesService();
            var CasesModel = CasesService.CasesQueryById(CasesId);
            return CasesModel;
        }
        #endregion
        #region CasesQueryByName
        public CasesModel CasesQueryByName(string CasesName)
        {
            //get model from DB
            CasesService CasesService = new CasesService();
            var CasesModel = CasesService.CasesQueryByName(CasesName);
            return CasesModel;
        }
        #endregion

        #region CasesDeleteById
        public bool CasesDeleteById(int CasesId)
        {
            //get model from DB
            CasesService CasesService = new CasesService();
            var Result = CasesService.CasesDeleteById(CasesId);
            return Result;
        }
        #endregion

        #region CasesDeleteByCategory
        public bool CasesDeleteByCategory(int CasesCategoryId)
        {
            //get model from DB
            CasesService CasesService = new CasesService();
            var Result = CasesService.CasesDeleteByCategory(CasesCategoryId);
            return Result;
        }
        #endregion

        #region CasesDeleteByCompany
        public bool CasesDeleteByCompany(int CompanyId)
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
        #endregion

        #region CasesQueryList
        public List<CasesModel> CasesQueryList()
        {
            CasesService CasesService = new CasesService();
            var CasesList = CasesService.CasesQueryList();
            return CasesList;
        }
        #endregion

        #region CasesQueryListByCategory
        public List<CasesModel> CasesQueryListByCategory(int CasesCategoryId)
        {
            CasesService CasesService = new CasesService();
            var CasesList = CasesService.CasesQueryListByCategory(CasesCategoryId);
            return CasesList;
        }
        #endregion

        #region CasesQueryListByCompany
        public List<CasesModel> CasesQueryListByCompany(int CompanyId)
        {
            CasesService CasesService = new CasesService();
            var CasesList = CasesService.CasesQueryListByCompany(CompanyId);
            return CasesList;
        }
        #endregion
    }
}
