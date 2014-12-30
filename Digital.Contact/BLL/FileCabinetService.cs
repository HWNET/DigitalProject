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
        protected static string UploadPath { get; set; }

        #region GetUploadPath
        public bool VerifyUploadPath(string UploadPath)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(UploadPath))
            {
                FileCabinetService.UploadPath = UploadPath;
                if (Directory.Exists(UploadPath))
                {
                    result = true;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        #endregion

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
            var rootDirectory = Path.Combine(FileCabinetService.UploadPath, subFolder);

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


        #region FilesList
        List<FilesMode> FileList = null;
        public List<FilesMode> FilesList(string UserId,string FolderName,string FolderPath)
        {
            if (FileList==null)
            {
                FileList = new List<FilesMode>();
            }
            if (this.IsExistDirectoryRoot(UserId))
            {
                DirectoryInfo dirMode = null;

                if (!string.IsNullOrEmpty(FolderName)&&string.IsNullOrEmpty(FolderPath))
                {
                    var dirPath = Path.Combine(FileCabinetService.DirectoryRoot, FolderName);
                    dirMode = new DirectoryInfo(dirPath);
                }
                else if (string.IsNullOrEmpty(FolderName) && !string.IsNullOrEmpty(FolderPath))
                {
                    dirMode = new DirectoryInfo(FolderPath);
                }
                else
                {
                    var dirPath = Path.Combine(FileCabinetService.DirectoryRoot);
                    dirMode = new DirectoryInfo(dirPath);
                }

                #region GetFiles
                var lstFiles = dirMode.GetFiles();
                foreach (var file in lstFiles)
                {
                    var FilesMode = new FilesMode
                    {
                        FolderParent=dirMode.Parent.Name,
                        FolderName = dirMode.Name,
                        FileName = file.Name,
                        FileFullName = file.FullName,
                        FileExtensionName = file.Extension,
                        FilePath = file.FullName,
                        FileSize = file.Length.ToString(),
                        FileDate = file.CreationTimeUtc.ToShortDateString()
                    };

                    FileList.Add(FilesMode);
                }
                #endregion

                var lstDirectory = dirMode.GetDirectories();
                foreach (var dir in lstDirectory)
                {
                    this.FilesList(UserId, string.Empty, dir.FullName);
                }
            }
            return FileList;
        }
        #endregion

        #region FileStreamByFile
        public byte[] FileStreamByFile(string UserId, string FolderName, string FileName)
        {
            FileStream iStream = null;
            FilesMode FilesMode = null;
            byte[] bytes = null;
            var FilesList = this.FilesListByDirectory(UserId, FolderName);
            if (FilesList!=null)
            {
                FilesMode = FilesList.Where(o => o.FileName == FileName).SingleOrDefault();
            }

            if (FilesMode!=null)
            {
                FileInfo fileInfo = new FileInfo(FilesMode.FileFullName);
                if (fileInfo.Exists)
                {
                    iStream = new FileStream(fileInfo.FullName,FileMode.Open);
                    bytes = new byte[(int)iStream.Length];
                    iStream.Read(bytes, 0, bytes.Length);
                    iStream.Close();
                    iStream.Dispose();
                }
            }

            return bytes;
        }
        #endregion

        #region FilesListByDirectory
        public List<FilesMode> FilesListByDirectory(string UserId, string FolderName)
        {
            List<FileFolderMode> DirectoryList = new List<FileFolderMode>();
            List<FilesMode> FileList = new List<FilesMode>();
            if (this.IsExistDirectoryRoot(UserId))
            {
                var DirectorySubFolder = Path.Combine(FileCabinetService.DirectoryRoot, FolderName);
                DirectoryInfo DirSubFolder = new DirectoryInfo(DirectorySubFolder);
                var lstDirectory = DirSubFolder.GetDirectories();
                foreach (var dir in lstDirectory)
                {
                        #region GetFiles
                        var lstFiles = dir.GetFiles();
                        foreach (var file in lstFiles)
                        {
                            var FilesMode = new FilesMode
                            {
                                FolderParent=FolderName,
                                FolderName = dir.Name,
                                FileName = file.Name,
                                FileFullName=file.FullName,
                                FileExtensionName = file.Extension,
                                FilePath = file.FullName,
                                FileSize = file.Length.ToString(),
                                FileDate = file.CreationTimeUtc.ToShortDateString()
                            };

                            FileList.Add(FilesMode);
                        }
                        #endregion
                }
            }
            return FileList;
        }
        #endregion

        #region FileDirectoryList

        public List<FileFolderMode> FileDirectoryList(string UserId)
        {
            List<FileFolderMode> DirectoryList = new List<FileFolderMode>();
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
                    FolderMode.FolderSize = FolderSize.ToString();
                    FolderMode.FolderDate = dir.CreationTimeUtc.ToShortDateString();

                    DirectoryList.Add(FolderMode);
                }
            }
            return DirectoryList;
        }
        #endregion

        #region FilesRemoveFromDirectory
        public bool FileRemove(string UserId, string FolderName,string FileName)
        {
            bool result = false;
            FilesMode FilesMode = null;
            var FilesList = this.FilesListByDirectory(UserId, FolderName);
            if (FilesList != null)
            {
                FilesMode = FilesList.Where(o => o.FileName == FileName).SingleOrDefault();
            }

            if (FilesMode != null)
            {
                var filePath = FilesMode.FileFullName;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    if (!File.Exists(filePath))
                    {
                        result = true;
                    }
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
