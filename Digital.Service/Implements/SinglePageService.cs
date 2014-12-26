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
    public partial class Service : ISinglePageService
    {
        #region SinglePageInsert
        public SinglePageModel SinglePageInsert(SinglePageModel Model)
        {
            try
            {
                Model.UpdateStatus = 1;
                SinglePageService SinglePageService = new SinglePageService();
                //Insert DB
                var Result = SinglePageService.SinglePageInsert(Model);
                //Insert Cache
                //GenericList.CacheModelObj.CompanyModellist.Add(Model);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("SinglePage", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region SinglePageUpdate
        public SinglePageModel SinglePageUpdate(SinglePageModel Model)
        {
            try
            {
                var PageModel = SinglePageQueryById(Model.PageID);

                if (PageModel != null)
                {
                    #region Instance Model

                    #endregion
                    #region UI Models

                    #endregion

                    //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                    PageModel.UpdateStatus = 2;

                    //directly update to DB
                    SinglePageService SinglePageService = new SinglePageService();
                    SinglePageService.SinglePageUpdate(Model);
                    return Model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("SinglePage", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region SinglePageQueryById
        public SinglePageModel SinglePageQueryById(int SinglePageId)
        {
            try
            {
                //get model from DB
                SinglePageService SinglePageService = new SinglePageService();
                var PageModel = SinglePageService.SinglePageQueryById(SinglePageId);
                return PageModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("SinglePage", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region SinglePageQueryByName
        public SinglePageModel SinglePageQueryByName(string SinglePageName)
        {
            try
            {
                //get model from DB
                SinglePageService SinglePageService = new SinglePageService();
                var PageModel = SinglePageService.SinglePageQueryByName(SinglePageName);
                return PageModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("SinglePage", MessageLevel.Level2, ex.ToString());
                return null;
            }
 
        }
        #endregion

        #region SinglePageDeleteById
        public bool SinglePageDeleteById(int SinglePageId)
        {
            try
            {
                SinglePageService SinglePageService = new SinglePageService();
                var PageModel = SinglePageQueryById(SinglePageId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                //PatentModel.UpdateStatus = 3;

                if (PageModel != null)
                {
                    var Result = SinglePageService.SinglePageDeleteById(SinglePageId);
                    return Result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("SinglePage", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region SinglePageDeleteByCompany
        public bool SinglePageDeleteByCompany(int CompanyId)
        {
            try
            {
                SinglePageService SinglePageService = new SinglePageService();
                var PageList = SinglePageService.SinglePageQueryListByCompany(CompanyId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                //CategoryModel.UpdateStatus = 3;

                if (PageList != null)
                {
                    var Result = SinglePageService.SinglePageDeleteByCompany(CompanyId);
                    return Result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("SinglePage", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region SinglePageQueryList
        public List<SinglePageModel> SinglePageQueryList()
        {
            try
            {
                SinglePageService SinglePageService = new SinglePageService();
                var PageList = SinglePageService.SinglePageQueryList();
                return PageList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("SinglePage", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region SinglePageQueryListByCompany
        public List<SinglePageModel> SinglePageQueryListByCompany(int CompanyId)
        {
            try
            {
                SinglePageService SinglePageService = new SinglePageService();
                var PageList = SinglePageService.SinglePageQueryListByCompany(CompanyId);
                return PageList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("SinglePage", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion
    }
}
