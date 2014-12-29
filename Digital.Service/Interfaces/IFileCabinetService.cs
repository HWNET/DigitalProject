using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "IFileCabinetService", SessionMode = SessionMode.Required)]
    public interface IFileCabinetService
    {
        [OperationContract]
        bool VerifyUploadPath(string UploadPath);
        [OperationContract]
        bool FileDirectoryCreate(string UserId, string SubDirectoryName);
        [OperationContract]
        string FileDirectoryPathByName(string UserId, string SubDirectoryName);
        [OperationContract]
        List<FileFolderMode> FileDirectoryList(string UserId);
        [OperationContract]
        List<FilesMode> FilesList(string UserId);
        [OperationContract]
        List<FilesMode> FilesListByDirectory(string UserId, string FolderName);
        [OperationContract]
        long FileDirectorySize(string UserId, string SubDirectoryName);
        [OperationContract]
        bool FileRemove(string UserId, string SubDirectoryName, string SubFolder, string FileName);
    }
}
