using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;

namespace Digital.Service.Implements
{
    public partial class Service : INewsCategoryService
    {

        public bool NewsCategoryInsert(NewsCategoryModel Model)
        {
            throw new NotImplementedException();
        }

        public NewsCategoryModel NewsCategoryUpdate(NewsCategoryModel Model)
        {
            throw new NotImplementedException();
        }

        public NewsCategoryModel NewsCategoryQueryById(int NewsCategoryId)
        {
            throw new NotImplementedException();
        }

        public NewsCategoryModel NewsCategoryQueryByName(string NewsCategoryName)
        {
            throw new NotImplementedException();
        }

        public bool NewsCategoryDeleteById(int NewsCategoryId)
        {
            throw new NotImplementedException();
        }

        public bool NewsCategoryDeleteByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public List<NewsCategoryModel> NewsCategoryQueryList()
        {
            throw new NotImplementedException();
        }

        public List<NewsCategoryModel> NewsCategoryQueryListByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }
    }
}
