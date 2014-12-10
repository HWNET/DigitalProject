using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "ICasesService", SessionMode = SessionMode.Required)]
    public interface ICasesService
    {
        [OperationContract]
        CasesModel CasesInsert(CasesModel Model);
        [OperationContract]
        CasesModel CasesUpdate(CasesModel Model);
        [OperationContract]
        CasesModel CasesQueryById(int CasesId);
        [OperationContract]
        CasesModel CasesQueryByName(string CasesName);
        
        [OperationContract]
        bool CasesDeleteById(int CasesId);
        [OperationContract]
        bool CasesDeleteByCategory(int CasesCategoryId);
        [OperationContract]
        bool CasesDeleteByCompany(int CompanyId);

        [OperationContract]
        List<CasesModel> CasesQueryList();
        [OperationContract]
        List<CasesModel> CasesQueryListByCategory(int CasesCategoryId);
        [OperationContract]
        List<CasesModel> CasesQueryListByCompany(int CompanyId);
    }
}
