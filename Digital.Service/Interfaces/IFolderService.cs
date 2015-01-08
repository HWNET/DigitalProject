using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "IFolderService", SessionMode = SessionMode.Required)]
    public interface IFolderService
    {
        [OperationContract]
        FolderModel FolderInsert(FolderModel Model);
        [OperationContract]
        FolderModel FolderUpdate(FolderModel Model);
        [OperationContract]
        FolderModel FolderQueryById(int FolderId);
        [OperationContract]
        FolderModel FolderQueryByName(string FolderName);
        [OperationContract]
        FolderModel FolderQueryByNameCode(string FolderNameCode);
        [OperationContract]
        bool FolderDeleteById(int FolderId);
        [OperationContract]
        bool FolderDeleteByIds(string FolderIds);
        [OperationContract]
        bool FolderDeleteByCompany(int CompanyId);
        [OperationContract]
        List<FolderModel> FolderQueryList();
        [OperationContract]
        List<FolderModel> FolderQueryListByCompany(int CompanyId);
    }
}
