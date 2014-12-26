using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Common;
using Digital.Common.Utilities;
using Digital.Contact.Models;
using System.IO;

namespace Digital.Contact.BLL
{
    public class FileCabinetService
    {
        protected static string DirectoryRoot { get; set; }

        #region FileDirectoryName
        public static string FileDirectoryName(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return string.Empty;
            }
            return new DirectoryInfo(dirPath).Name;
        }
        #endregion

        #region IsExistDirectoryRoot
        protected bool IsExistDirectoryRoot(string UserId)
        {
            bool result=false;
            var subFolder = "UserInfo" + CryptoService.MD5Encrypt(UserId,SerurityType.UserInfoFolder);
            var rootDirectory = Path.Combine(UploadConfigContext.UploadPath, subFolder);

            if (Directory.Exists(rootDirectory))
            {
                result = true;
                FileCabinetService.DirectoryRoot = rootDirectory;
            }
            return result;
        }
        #endregion

        #region FileDirectoryCreate
        public bool FileDirectoryCreate(string UserId,string SubDirectoryName)
        {
            bool result = false;

            if (this.IsExistDirectoryRoot(UserId))
            {
                var subDirectory = Path.Combine(FileCabinetService.DirectoryRoot, SubDirectoryName);
                if (!Directory.Exists(subDirectory))
                {
                    var dirInfo=Directory.CreateDirectory(subDirectory);
                    if (dirInfo != null && dirInfo.Exists)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }
        #endregion

        #region FileDirectoryPathByName
        public string FileDirectoryPathByName(string UserId, string SubDirectoryName)
        {
            string result = string.Empty;

            if (this.IsExistDirectoryRoot(UserId))
            {
                var subDirectory = Path.Combine(FileCabinetService.DirectoryRoot, SubDirectoryName);
                if (Directory.Exists(subDirectory))
                {
                    result = subDirectory;
                }
            }

            return result;
        }
        #endregion

        #region FileDirectoryList
        public List<FileFolderMode> FileDirectoryList(string UserId)
        {
            List<FileFolderMode> DirectoryList = new List<FileFolderMode>();
            List<FilesMode> FileList = new List<FilesMode>();
            if (this.IsExistDirectoryRoot(UserId))
            {
                DirectoryInfo DirectoryRoot = new DirectoryInfo(FileCabinetService.DirectoryRoot);
                var lstDirectory = DirectoryRoot.GetDirectories();
                foreach (var dir in lstDirectory)
                {
                    var FolderMode = new FileFolderMode
                    { 
                         FolderName=dir.Name,
                         FolderPath=dir.FullName,
                    };

                    var FolderSize = this.FileDirectorySize(UserId, dir.FullName);
                    FolderMode.FolderSize = FolderSize;
                    FolderMode.FolderDate = dir.CreationTimeUtc;

                    #region GetFiles
                    var lstFiles = dir.GetFiles();
                    foreach (var file in lstFiles)
                    {
                        var FilesMode = new FilesMode { 
                            FolderName=dir.Name,
                            FileName=file.Name,
                            FileExtensionName=file.Extension,
                            FilePath=file.FullName,
                            FileSize=file.Length, 
                            FileDate=file.CreationTimeUtc
                        };

                        FileList.Add(FilesMode);
                    }

                    if (FileList!=null&&FileList.Count>0)
                    {
                        FolderMode.FileList = FileList;
                    }
                    #endregion

                    DirectoryList.Add(FolderMode);
                }
            }
            return DirectoryList;
        }
        #endregion

        #region FilesRemoveFromDirectory
        public bool FileRemove(string UserId, string SubDirectoryName,string SubFolder,string FileName)
        {
            bool result = false;
            if (this.IsExistDirectoryRoot(UserId))
            {
                var subDirectory = Path.Combine(FileCabinetService.DirectoryRoot, SubDirectoryName);
                var filePath = Path.Combine(subDirectory, SubFolder, FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            return result;
        }
        #endregion

        #region FileDirectorySize
        public long FileDirectorySize(string UserId, string SubDirectoryName)
        {
            long dirSize = 0;
            if (this.IsExistDirectoryRoot(UserId))
            {
                DirectoryInfo dir = new DirectoryInfo(SubDirectoryName);
                foreach (FileInfo file in dir.GetFiles())
                {
                    dirSize += file.Length;
                }

                foreach (DirectoryInfo subdir in dir.GetDirectories())
                {
                    dirSize += this.FileDirectorySize(UserId, subdir.FullName);
                }

            }

            return dirSize;
        }
        #endregion
    }
}
