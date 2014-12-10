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
    public partial class Service : ICasesCategoryService
    {
        #region CasesCategoryInsert
        public CasesCategoryModel CasesCategoryInsert(CasesCategoryModel Model)
        {
            Model.UpdateStatus = 1;
            CasesCategoryService CasesCategoryService = new CasesCategoryService();
            //Insert DB
            var Result=CasesCategoryService.CasesCategoryInsert(Model);
            //Insert Cache
            //GenericList.CacheModelObj.CompanyModellist.Add(Model);
            return Result;
        }
        #endregion

        #region CasesCategoryUpdate
        public CasesCategoryModel CasesCategoryUpdate(CasesCategoryModel Model)
        {
            var CategoryModel = CasesCategoryQueryById(Model.CasesCategoryID);

            if (CategoryModel != null)
            {
                #region Instance Model
                //CategoryModel.CasesCategoryName = Model.CasesCategoryName;
                //CategoryModel.CasesCategoryContent = Model.CasesCategoryContent;
                //CategoryModel.CasesCategoryID = Model.CasesCategoryID;
                //CategoryModel.CasesCategoryOrderID = Model.CasesCategoryOrderID;
                //CategoryModel.CasesCategoryParentID = CategoryModel.CasesCategoryParentID;
                //CategoryModel.CasesCategoryPicture = CategoryModel.CasesCategoryPicture;
                //CategoryModel.CasesModels = Model.CasesModels;
                //CategoryModel.CompanyID = CategoryModel.CompanyID;
                #endregion
                #region UI Models

                #endregion

                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                CategoryModel.UpdateStatus = 2;

                //directly update to DB
                CasesCategoryService CasesCategoryService = new CasesCategoryService();
                CasesCategoryService.CasesCategoryUpdate(Model);
                return Model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region CasesCategoryQueryById
        public CasesCategoryModel CasesCategoryQueryById(int CasesCategoryId)
        {
            //get model from DB
            CasesCategoryService CasesCategoryService = new CasesCategoryService();
            var CategoryModel = CasesCategoryService.CasesCategoryQueryById(CasesCategoryId);
            return CategoryModel;
        }
        #endregion

        #region CasesCategoryQueryByName
        public CasesCategoryModel CasesCategoryQueryByName(string CasesCategoryName)
        {
            //get model from DB
            CasesCategoryService CasesCategoryService = new CasesCategoryService();
            var CategoryModel = CasesCategoryService.CasesCategoryQueryByName(CasesCategoryName);
            return CategoryModel;
        }
        #endregion

        #region CasesCategoryDeleteById
        public bool CasesCategoryDeleteById(int CasesCategoryId)
        {
            CasesCategoryService CasesCategoryService = new CasesCategoryService();
            var CategoryModel = CasesCategoryQueryById(CasesCategoryId);
            //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
            CategoryModel.UpdateStatus = 3;

            if (CategoryModel!=null)
            {
                var Result=CasesCategoryService.CasesCategoryDeleteById(CasesCategoryId);
                return Result;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region CasesCategoryDeleteByCompany
        public bool CasesCategoryDeleteByCompany(int CompanyId)
        {
            CasesCategoryService CasesCategoryService = new CasesCategoryService();
            var CategoryList = CasesCategoryService.CasesCategoryQueryListByCompany(CompanyId);
            //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
            //CategoryModel.UpdateStatus = 3;

            if (CategoryList != null)
            {
                var Result = CasesCategoryService.CasesCategoryDeleteByCompany(CompanyId);
                return Result;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region CasesCategoryQueryList
        public List<CasesCategoryModel> CasesCategoryQueryList()
        {
            CasesCategoryService CasesCategoryService = new CasesCategoryService();
            var CategoryList = CasesCategoryService.CasesCategoryQueryList();
            return CategoryList;
        }
        #endregion

        #region CasesCategoryQueryListByCompany
        public List<CasesCategoryModel> CasesCategoryQueryListByCompany(int CompanyId)
        {
            CasesCategoryService CasesCategoryService = new CasesCategoryService();
            var CategoryList = CasesCategoryService.CasesCategoryQueryListByCompany(CompanyId);
            return CategoryList;
        }
        #endregion
    }
}
