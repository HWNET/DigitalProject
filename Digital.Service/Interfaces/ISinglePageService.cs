using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "ISinglePageService", SessionMode = SessionMode.Required)]
    public interface ISinglePageService
    {
        [OperationContract]
        SinglePageModel SinglePageInsert(SinglePageModel Model);
        [OperationContract]
        SinglePageModel SinglePageUpdate(SinglePageModel Model);
        [OperationContract]
        SinglePageModel SinglePageQueryById(int SinglePageId);
        [OperationContract]
        SinglePageModel SinglePageQueryByName(string SinglePageName);
        [OperationContract]
        bool SinglePageDeleteById(int SinglePageId);
        [OperationContract]
        bool SinglePageDeleteByCompany(int CompanyId);
        [OperationContract]
        List<SinglePageModel> SinglePageQueryList();
        [OperationContract]
        List<SinglePageModel> SinglePageQueryListByCompany(int CompanyId);
    }
}
