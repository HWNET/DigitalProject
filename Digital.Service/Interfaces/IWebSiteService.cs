using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "IWebSiteService", SessionMode = SessionMode.Required)]
    public interface IWebSiteService
    {
        [OperationContract]
        WebSiteModel GetIndexModel(int CompanyId);
        [OperationContract]
        List<PageModel> GetPageList(int TemplateId, int CompanyId);
    }
}
