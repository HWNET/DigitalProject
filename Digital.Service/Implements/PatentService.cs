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
    public partial class Service : IPatentService
    {
        #region PatentInsert
        public PatentModel PatentInsert(PatentModel Model)
        {
            Model.UpdateStatus = 1;
            PatentService PatentService = new PatentService();
            //Insert DB
            var Result = PatentService.PatentInsert(Model);
            //Insert Cache
            //GenericList.CacheModelObj.CompanyModellist.Add(Model);
            return Result;
        }
        #endregion

        #region PatentUpdate
        public PatentModel PatentUpdate(PatentModel Model)
        {
            var PatentModel = PatentQueryById(Model.PatentID);

            if (PatentModel != null)
            {
                #region Instance Model

                #endregion
                #region UI Models

                #endregion

                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                PatentModel.UpdateStatus = 2;

                //directly update to DB
                PatentService PatentService = new PatentService();
                PatentService.PatentUpdate(Model);
                return Model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region PatentQueryById
        public PatentModel PatentQueryById(int PatentId)
        {
            //get model from DB
            PatentService PatentService = new PatentService();
            var PatentModel = PatentService.PatentQueryById(PatentId);
            return PatentModel;
        }
        #endregion

        #region PatentQueryByName
        public PatentModel PatentQueryByName(string PatentName)
        {
            //get model from DB
            PatentService PatentService = new PatentService();
            var PatentModel = PatentService.PatentQueryByName(PatentName);
            return PatentModel;
        }
        #endregion

        #region PatentDeleteById
        public bool PatentDeleteById(int PatentId)
        {
            PatentService PatentService = new PatentService();
            var PatentModel = PatentQueryById(PatentId);
            //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
            //PatentModel.UpdateStatus = 3;

            if (PatentModel != null)
            {
                var Result = PatentService.PatentDeleteById(PatentId);
                return Result;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region PatentDeleteByCompany
        public bool PatentDeleteByCompany(int CompanyId)
        {
            PatentService PatentService = new PatentService();
            var PatentList = PatentService.PatentQueryListByCompany(CompanyId);
            //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
            //CategoryModel.UpdateStatus = 3;

            if (PatentList != null)
            {
                var Result = PatentService.PatentDeleteByCompany(CompanyId);
                return Result;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region PatentQueryList
        public List<PatentModel> PatentQueryList()
        {
            PatentService PatentService = new PatentService();
            var PatentList = PatentService.PatentQueryList();
            return PatentList;
        }
        #endregion

        #region PatentQueryListByCompany
        public List<PatentModel> PatentQueryListByCompany(int CompanyId)
        {
            PatentService PatentService = new PatentService();
            var PatentList = PatentService.PatentQueryListByCompany(CompanyId);
            return PatentList;
        }
        #endregion

        public bool PatentDisable(PatentModel Model)
        {
            throw new NotImplementedException();
        }

        public bool PatentTransfer(PatentModel Model)
        {
            throw new NotImplementedException();
        }
    }
}
