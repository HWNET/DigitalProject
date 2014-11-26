using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;

namespace Digital.Service.Implements
{
    public partial class Service : ICompanyService
    {

        public bool CompanyInsert(CompanyModel Model)
        {
            throw new NotImplementedException();
        }

        public CompanyModel CompanyUpdate(CompanyModel Model)
        {
            throw new NotImplementedException();
        }

        public CompanyModel CompanyQueryById(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public CompanyModel CompanyQueryByName(string CompanyName)
        {
            throw new NotImplementedException();
        }

        public bool CompanyDeleteById(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public bool CompanyDisposeByNo(string CompanyRegisteredNO)
        {
            throw new NotImplementedException();
        }

        public List<CompanyModel> CompanyQueryList()
        {
            throw new NotImplementedException();
        }
    }
}
