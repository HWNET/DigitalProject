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
    public partial class Service : IFileCabinetService
    {
        #region FileDirectoryCreate
        public bool FileDirectoryCreate(string UserId, string SubDirectoryName)
        {
            var result = false;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                result = FileCabinetService.FileDirectoryCreate(UserId, SubDirectoryName);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return result;
        }
        #endregion

        #region FileDirectoryPathByName
        public string FileDirectoryPathByName(string UserId, string SubDirectoryName)
        {
            var directoryPath = string.Empty;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                directoryPath = FileCabinetService.FileDirectoryPathByName(UserId, SubDirectoryName);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return directoryPath;
        }
        #endregion
        
        #region FileDirectoryList
        public List<FileFolderMode> FileDirectoryList(string UserId)
        {
            List<FileFolderMode> lstDirectory = null;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                lstDirectory = FileCabinetService.FileDirectoryList(UserId);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return lstDirectory;
        }
        #endregion

        #region FileDirectorySize
        public long FileDirectorySize(string UserId, string SubDirectoryName)
        {
            long directorySize = 0;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                directorySize = FileCabinetService.FileDirectorySize(UserId, SubDirectoryName);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return directorySize;
        }
        #endregion

        #region FileRemove
        public bool FileRemove(string UserId, string SubDirectoryName, string SubFolder, string FileName)
        {
            var result = false;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                result = FileCabinetService.FileRemove(UserId, SubDirectoryName, SubFolder, FileName);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return result;
        }
        #endregion
    }
}
