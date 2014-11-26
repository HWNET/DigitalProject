using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "ICompanyService", SessionMode = SessionMode.Required)]
    public interface ICompanyService
    {
        [OperationContract]
        bool CompanyInsert(CompanyModel Model);
        [OperationContract]
        CompanyModel CompanyUpdate(CompanyModel Model);
        [OperationContract]
        CompanyModel CompanyQueryById(int CompanyId);
        [OperationContract]
        CompanyModel CompanyQueryByName(string CompanyName);
        [OperationContract]
        bool CompanyDeleteById(int CompanyId);
        [OperationContract]
        bool CompanyDisposeByNo(string CompanyRegisteredNO);
        [OperationContract]
        List<CompanyModel> CompanyQueryList();
    }
}
