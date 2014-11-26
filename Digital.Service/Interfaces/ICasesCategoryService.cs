using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "ICasesCategoryService", SessionMode = SessionMode.Required)]
    public interface ICasesCategoryService
    {
        [OperationContract]
        bool CasesCategoryInsert(CasesCategoryModel Model);
        [OperationContract]
        CasesCategoryModel CasesCategoryUpdate(CasesCategoryModel Model);
        [OperationContract]
        CasesCategoryModel CasesCategoryQueryById(int CasesCategoryId);
        [OperationContract]
        CasesCategoryModel CasesCategoryQueryByName(string CasesCategoryName);

        [OperationContract]
        bool CasesCategoryDeleteById(int CasesCategoryId);
        [OperationContract]
        bool CasesCategoryDeleteByCompany(int CompanyId);

        [OperationContract]
        List<CasesCategoryModel> CasesCategoryQueryList();
        [OperationContract]
        List<CasesCategoryModel> CasesCategoryQueryListByCompany(int CompanyId);
    }
}
