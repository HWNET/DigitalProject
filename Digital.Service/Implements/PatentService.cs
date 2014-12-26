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
    public partial class Service : IPatentService
    {
        #region PatentInsert
        public PatentModel PatentInsert(PatentModel Model)
        {
            try
            {
                Model.UpdateStatus = 1;
                PatentService PatentService = new PatentService();
                //Insert DB
                var Result = PatentService.PatentInsert(Model);
                //Insert Cache
                //GenericList.CacheModelObj.CompanyModellist.Add(Model);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region PatentUpdate
        public PatentModel PatentUpdate(PatentModel Model)
        {
            try
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
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region PatentQueryById
        public PatentModel PatentQueryById(int PatentId)
        {
            try
            {
                //get model from DB
                PatentService PatentService = new PatentService();
                var PatentModel = PatentService.PatentQueryById(PatentId);
                return PatentModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region PatentQueryByName
        public PatentModel PatentQueryByName(string PatentName)
        {
            try
            {
                //get model from DB
                PatentService PatentService = new PatentService();
                var PatentModel = PatentService.PatentQueryByName(PatentName);
                return PatentModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region PatentDeleteById
        public bool PatentDeleteById(int PatentId)
        {
            try
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
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region PatentDeleteByCompany
        public bool PatentDeleteByCompany(int CompanyId)
        {
            try
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
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region PatentQueryList
        public List<PatentModel> PatentQueryList()
        {
            try
            {
                PatentService PatentService = new PatentService();
                var PatentList = PatentService.PatentQueryList();
                return PatentList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region PatentQueryListByCompany
        public List<PatentModel> PatentQueryListByCompany(int CompanyId)
        {
            try
            {
                PatentService PatentService = new PatentService();
                var PatentList = PatentService.PatentQueryListByCompany(CompanyId);
                return PatentList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return null;
            }

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

        #region GetTechnologyDomainList
        public List<TechnologyDomainMode> GetTechnologyDomainList()
        {
            try
            {
                var modes = GenericList.CacheModelObj.TechnologyDomainModelist;
                if (modes != null)
                {
                    return modes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region GetDevelopmentStatusList
        public List<DevelopmentStatusMode> GetDevelopmentStatusList()
        {
            try
            {
                var modes = GenericList.CacheModelObj.DevelopmentStatusModelist;
                if (modes != null)
                {
                    return modes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Patent", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

    }
}
