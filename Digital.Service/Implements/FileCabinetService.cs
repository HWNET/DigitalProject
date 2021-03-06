﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;
using Digital.Contact.BLL;
using Digital.Common.Logging;
using System.IO;

namespace Digital.Service.Implements
{
    public partial class Service : IFileCabinetService
    {
        #region GetUploadPath
        public bool VerifyUploadPath(string UploadPath)
        {
            var result = false;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                result=FileCabinetService.VerifyUploadPath(UploadPath);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return result;
        }
        #endregion
        #region FileDirectoryCreate
        public bool FileDirectoryCreate(string UserId, string SubDirectoryName, string SubDirectoryNameCode)
        {
            var result = false;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                result = FileCabinetService.FileDirectoryCreate(UserId, SubDirectoryName, SubDirectoryNameCode);
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

        #region FilesList
        public List<FilesMode> FilesList(string UserId, string FolderName, string FolderPath)
        {
            List<FilesMode> lstFiles = null;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                lstFiles = FileCabinetService.FilesList(UserId,FolderName,FolderPath);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return lstFiles;
        }
        #endregion

        #region FilesListByDirectory
        public List<FilesMode> FilesListByDirectory(string UserId, string FolderName)
        {
            List<FilesMode> lstFiles = null;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                lstFiles = FileCabinetService.FilesListByDirectory(UserId,FolderName);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return lstFiles;
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
        public bool FileRemove(string UserId, string FolderName, string FileName)
        {
            var result = false;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                result = FileCabinetService.FileRemove(UserId, FolderName, FileName);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return result;
        }
        #endregion

        #region FileStreamByFile
        public byte[] FileStreamByFile(string UserId, string FolderName, string FileName)
        {
            byte[] iStream = null;
            try
            {
                FileCabinetService FileCabinetService = new FileCabinetService();
                iStream = FileCabinetService.FileStreamByFile(UserId,FolderName,FileName);
            }
            catch (Exception ex)
            {
                logger.WriteInfo("FileCabinet", MessageLevel.Level2, ex.ToString());
            }
            return iStream;
        }
        #endregion
    }
}
