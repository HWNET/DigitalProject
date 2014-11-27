using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;

namespace Digital.Service.Implements
{
    public partial class Service : IPatentService
    {
        public bool PatentInsert(PatentModel Model)
        {
            throw new NotImplementedException();
        }

        public PatentModel PatentUpdate(PatentModel Model)
        {
            throw new NotImplementedException();
        }

        public PatentModel PatentQueryById(int PatentId)
        {
            throw new NotImplementedException();
        }

        public PatentModel PatentQueryByName(string PatentName)
        {
            throw new NotImplementedException();
        }

        public bool PatentDeleteById(int PatentId)
        {
            throw new NotImplementedException();
        }

        public bool PatentDeleteByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public List<PatentModel> PatentQueryList()
        {
            throw new NotImplementedException();
        }

        public List<PatentModel> PatentQueryListByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public bool PatentDisable(PatentModel Model)
        {
            throw new NotImplementedException();
        }

        public bool PatentTransfer(PatentModel Model)
        {
            throw new NotImplementedException();
        }
    }
}
