using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "IPatentService", SessionMode = SessionMode.Required)]
    public interface IPatentService
    {
        [OperationContract]
        PatentModel PatentInsert(PatentModel Model);
        [OperationContract]
        PatentModel PatentUpdate(PatentModel Model);
        [OperationContract]
        PatentModel PatentQueryById(int PatentId);
        [OperationContract]
        PatentModel PatentQueryByName(string PatentName);
        [OperationContract]
        bool PatentDeleteById(int PatentId);
        [OperationContract]
        bool PatentDeleteByCompany(int CompanyId);
        [OperationContract]
        List<PatentModel> PatentQueryList();
        [OperationContract]
        List<PatentModel> PatentQueryListByCompany(int CompanyId);

        [OperationContract]
        bool PatentDisable(PatentModel Model); //专利停用 
        [OperationContract]
        bool PatentTransfer(PatentModel Model); //专利转让
    }
}
