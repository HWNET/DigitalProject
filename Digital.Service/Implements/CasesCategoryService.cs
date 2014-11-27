using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;

namespace Digital.Service.Implements
{
    public partial class Service : ICasesCategoryService
    {

        public bool CasesCategoryInsert(CasesCategoryModel Model)
        {
            throw new NotImplementedException();
        }

        public CasesCategoryModel CasesCategoryUpdate(CasesCategoryModel Model)
        {
            throw new NotImplementedException();
        }

        public CasesCategoryModel CasesCategoryQueryById(int CasesCategoryId)
        {
            throw new NotImplementedException();
        }

        public CasesCategoryModel CasesCategoryQueryByName(string CasesCategoryName)
        {
            throw new NotImplementedException();
        }

        public bool CasesCategoryDeleteById(int CasesCategoryId)
        {
            throw new NotImplementedException();
        }

        public bool CasesCategoryDeleteByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public List<CasesCategoryModel> CasesCategoryQueryList()
        {
            throw new NotImplementedException();
        }

        public List<CasesCategoryModel> CasesCategoryQueryListByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }
    }
}
