using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;
using Digital.Contact.BLL;
using Digital.Common.Logging;

namespace Digital.Service.Implements
{
    public partial class Service : INewsCategoryService
    {
        #region NewsCategoryInsert
        public NewsCategoryModel NewsCategoryInsert(NewsCategoryModel Model)
        {
            try
            {
                Model.UpdateStatus = 1;
                NewsCategoryService NewsCategoryService = new NewsCategoryService();
                //Insert DB
                var Result = NewsCategoryService.NewsCategoryInsert(Model);
                //Insert Cache
                //GenericList.CacheModelObj.CompanyModellist.Add(Model);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("NewsCategory", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsCategoryUpdate
        public NewsCategoryModel NewsCategoryUpdate(NewsCategoryModel Model)
        {
            try
            {
                var CategoryModel = NewsCategoryQueryById(Model.NewsCategoryID);

                if (CategoryModel != null)
                {
                    #region Instance Model

                    #endregion
                    #region UI Models

                    #endregion

                    //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                    CategoryModel.UpdateStatus = 2;

                    //directly update to DB
                    NewsCategoryService NewsCategoryService = new NewsCategoryService();
                    NewsCategoryService.NewsCategoryUpdate(Model);
                    return Model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("NewsCategory", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsCategoryQueryById
        public NewsCategoryModel NewsCategoryQueryById(int NewsCategoryId)
        {
            try
            {
                //get model from DB
                NewsCategoryService NewsCategoryModel = new NewsCategoryService();
                var CategoryModel = NewsCategoryModel.NewsCategoryQueryById(NewsCategoryId);
                return CategoryModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("NewsCategory", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsCategoryQueryByName
        public NewsCategoryModel NewsCategoryQueryByName(string NewsCategoryName)
        {
            try
            {
                //get model from DB
                NewsCategoryService NewsCategoryService = new NewsCategoryService();
                var CategoryModel = NewsCategoryService.NewsCategoryQueryByName(NewsCategoryName);
                return CategoryModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("NewsCategory", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsCategoryDeleteById
        public bool NewsCategoryDeleteById(int NewsCategoryId)
        {
            try
            {
                NewsCategoryService NewsCategoryService = new NewsCategoryService();
                var CategoryModel = NewsCategoryQueryById(NewsCategoryId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                CategoryModel.UpdateStatus = 3;

                if (CategoryModel != null)
                {
                    var Result = NewsCategoryService.NewsCategoryDeleteById(NewsCategoryId);
                    return Result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("NewsCategory", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region NewsCategoryDeleteByCompany
        public bool NewsCategoryDeleteByCompany(int CompanyId)
        {
            try
            {
                NewsCategoryService NewsCategoryService = new NewsCategoryService();
                var CategoryList = NewsCategoryService.NewsCategoryQueryListByCompany(CompanyId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                //CategoryModel.UpdateStatus = 3;

                if (CategoryList != null)
                {
                    var Result = NewsCategoryService.NewsCategoryDeleteByCompany(CompanyId);
                    return Result;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                logger.WriteInfo("NewsCategory", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region NewsCategoryQueryList
        public List<NewsCategoryModel> NewsCategoryQueryList()
        {
            try
            {
                NewsCategoryService NewsCategoryService = new NewsCategoryService();
                var CategoryList = NewsCategoryService.NewsCategoryQueryList();
                return CategoryList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("NewsCategory", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsCategoryQueryListByCompany
        public List<NewsCategoryModel> NewsCategoryQueryListByCompany(int CompanyId)
        {
            try
            {
                NewsCategoryService NewsCategoryService = new NewsCategoryService();
                var CategoryList = NewsCategoryService.NewsCategoryQueryListByCompany(CompanyId);
                return CategoryList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("NewsCategory", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion
    }
}
