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
    public partial class Service : IFolderService
    {
        #region FolderInsert
        public FolderModel FolderInsert(FolderModel Model)
        {
            try
            {
                FolderService FolderService = new FolderService();
                //Insert DB
                var Result = FolderService.FolderInsert(Model);
                //Insert Cache
                //GenericList.CacheModelObj.CompanyModellist.Add(Model);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region FolderUpdate
        public FolderModel FolderUpdate(FolderModel Model)
        {
            try
            {
                var FolderModel = FolderQueryById(Model.FolderID);

                if (FolderModel != null)
                {
                    #region Instance Model

                    #endregion
                    #region UI Models

                    #endregion

                    //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 

                    //directly update to DB
                    FolderService FolderService = new FolderService();
                    FolderService.FolderUpdate(Model);
                    return Model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region FolderQueryById
        public FolderModel FolderQueryById(int FolderId)
        {
            try
            {
                //get model from DB
                FolderService FolderService = new FolderService();
                var FolderModel = FolderService.FolderQueryById(FolderId);
                return FolderModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region FolderQueryByName
        public FolderModel FolderQueryByName(string FolderName)
        {
            try
            {
                //get model from DB
                FolderService FolderService = new FolderService();
                var FolderModel = FolderService.FolderQueryByName(FolderName);
                return FolderModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region FolderQueryByNameCode
        public FolderModel FolderQueryByNameCode(string FolderNameCode)
        {
            try
            {
                //get model from DB
                FolderService FolderService = new FolderService();
                var FolderModel = FolderService.FolderQueryByNameCode(FolderNameCode);
                return FolderModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region FolderDeleteById
        public bool FolderDeleteById(int FolderId)
        {
            try
            {
                FolderService FolderService = new FolderService();
                var FolderModel = FolderQueryById(FolderId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 


                if (FolderModel != null)
                {
                    var Result = FolderService.FolderDeleteById(FolderId);
                    return Result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return false;
            }
        }
        #endregion

        #region FolderDeleteByIds
        public bool FolderDeleteByIds(string FolderIds)
        {
            try
            {
                FolderService FolderService = new FolderService();
                var TempFolderIds = FolderIds.Trim(',').Split(',');
                foreach (var ItemId in TempFolderIds)
                {
                    FolderService.FolderDeleteById(ItemId.ToInt());
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return false;
            }
        }
        #endregion

        #region FolderDeleteByCompany
        public bool FolderDeleteByCompany(int CompanyId)
        {
            try
            {
                FolderService FolderService = new FolderService();
                var FolderList = FolderService.FolderDeleteByCompany(CompanyId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                //CategoryModel.UpdateStatus = 3;

                if (FolderList != null)
                {
                    var Result = FolderService.FolderDeleteByCompany(CompanyId);
                    return Result;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return false;
            }
        }
        #endregion

        #region FolderQueryList
        public List<FolderModel> FolderQueryList()
        {
            try
            {
                FolderService FolderService = new FolderService();
                var FolderList = FolderService.FolderQueryList();
                return FolderList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion

        #region FolderQueryListByCompany
        public List<FolderModel> FolderQueryListByCompany(int CompanyId)
        {
            try
            {
                FolderService FolderService = new FolderService();
                var FolderList = FolderService.FolderQueryListByCompany(CompanyId);
                return FolderList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Folder", MessageLevel.Level2, ex.ToString());
                return null;
            }
        }
        #endregion
    }
}
