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
    public partial class Service : ISinglePageService
    {
        #region SinglePageInsert
        public bool SinglePageInsert(SinglePageModel Model)
        {
            Model.UpdateStatus = 1;
            SinglePageService SinglePageService = new SinglePageService();
            //Insert DB
            var Result = SinglePageService.SinglePageInsert(Model);
            //Insert Cache
            //GenericList.CacheModelObj.CompanyModellist.Add(Model);
            return Result;
        }
        #endregion

        #region SinglePageUpdate
        public SinglePageModel SinglePageUpdate(SinglePageModel Model)
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
        #endregion

        #region SinglePageQueryById
        public SinglePageModel SinglePageQueryById(int SinglePageId)
        {
            //get model from DB
            SinglePageService SinglePageService = new SinglePageService();
            var PageModel = SinglePageService.SinglePageQueryById(SinglePageId);
            return PageModel;
        }
        #endregion

        #region SinglePageQueryByName
        public SinglePageModel SinglePageQueryByName(string SinglePageName)
        {
            //get model from DB
            SinglePageService SinglePageService = new SinglePageService();
            var PageModel = SinglePageService.SinglePageQueryByName(SinglePageName);
            return PageModel;
        }
        #endregion

        #region SinglePageDeleteById
        public bool SinglePageDeleteById(int SinglePageId)
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
        #endregion

        #region SinglePageDeleteByCompany
        public bool SinglePageDeleteByCompany(int CompanyId)
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
        #endregion

        #region SinglePageQueryList
        public List<SinglePageModel> SinglePageQueryList()
        {
            SinglePageService SinglePageService = new SinglePageService();
            var PageList = SinglePageService.SinglePageQueryList();
            return PageList;
        }
        #endregion

        #region SinglePageQueryListByCompany
        public List<SinglePageModel> SinglePageQueryListByCompany(int CompanyId)
        {
            SinglePageService SinglePageService = new SinglePageService();
            var PageList = SinglePageService.SinglePageQueryListByCompany(CompanyId);
            return PageList;
        }
        #endregion
    }
}
