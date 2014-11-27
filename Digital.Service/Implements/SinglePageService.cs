using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;

namespace Digital.Service.Implements
{
    public partial class Service : ISinglePageService
    {
        public bool SinglePageInsert(SinglePageModel Model)
        {
            throw new NotImplementedException();
        }

        public SinglePageModel SinglePageUpdate(SinglePageModel Model)
        {
            throw new NotImplementedException();
        }

        public SinglePageModel SinglePageQueryById(int SinglePageId)
        {
            throw new NotImplementedException();
        }

        public SinglePageModel SinglePageQueryByName(string SinglePageName)
        {
            throw new NotImplementedException();
        }

        public bool SinglePageDeleteById(int SinglePageId)
        {
            throw new NotImplementedException();
        }

        public bool SinglePageDeleteByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public List<SinglePageModel> SinglePageQueryList()
        {
            throw new NotImplementedException();
        }

        public List<SinglePageModel> SinglePageQueryListByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }
    }
}
