using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "INewsCategoryService", SessionMode = SessionMode.Required)]
    public interface INewsCategoryService
    {
        [OperationContract]
        NewsCategoryModel NewsCategoryInsert(NewsCategoryModel Model);
        [OperationContract]
        NewsCategoryModel NewsCategoryUpdate(NewsCategoryModel Model);
        [OperationContract]
        NewsCategoryModel NewsCategoryQueryById(int NewsCategoryId);
        [OperationContract]
        NewsCategoryModel NewsCategoryQueryByName(string NewsCategoryName);

        [OperationContract]
        bool NewsCategoryDeleteById(int NewsCategoryId);
        [OperationContract]
        bool NewsCategoryDeleteByCompany(int CompanyId);

        [OperationContract]
        List<NewsCategoryModel> NewsCategoryQueryList();
        [OperationContract]
        List<NewsCategoryModel> NewsCategoryQueryListByCompany(int CompanyId);
    }
}
