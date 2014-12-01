using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Service.Interfaces
{
   
    [ServiceContract(Name = "IConfigService", SessionMode = SessionMode.Required)]
    public interface IConfigService
    {
        [OperationContract]
        List<Digital.Contact.Models.MenuModel> GetMenuList();

        [OperationContract]
        List<CommonRightModel> GetCommonRightList();
    }
}
