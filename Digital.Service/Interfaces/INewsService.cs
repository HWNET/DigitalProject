using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "INewsService", SessionMode = SessionMode.Required)]
    public interface INewsService
    {
        [OperationContract]
        NewsModel NewsInsert(NewsModel Model);
        [OperationContract]
        NewsModel NewsUpdate(NewsModel Model);
        [OperationContract]
        NewsModel NewsQueryById(int NewsId);
        [OperationContract]
        NewsModel NewsQueryByTitle(string NewsTitle);

        [OperationContract]
        bool NewsDeleteById(int NewsId);
        [OperationContract]
        bool NewsDeleteByCategory(int NewsCategoryId);
        [OperationContract]
        bool NewsDeleteByCompany(int CompanyId);

        [OperationContract]
        List<NewsModel> NewsQueryList();
        [OperationContract]
        List<NewsModel> NewsQueryListByCategory(int NewsCategoryId);
        [OperationContract]
        List<NewsModel> NewsQueryListByCompany(int CompanyId);
    }
}
