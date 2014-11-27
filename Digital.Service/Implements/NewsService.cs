using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;

namespace Digital.Service.Implements
{
    public partial class Service : INewsService
    {
        public bool NewsInsert(NewsModel Model)
        {
            throw new NotImplementedException();
        }

        public NewsModel NewsUpdate(NewsModel Model)
        {
            throw new NotImplementedException();
        }

        public NewsModel NewsQueryById(int NewsId)
        {
            throw new NotImplementedException();
        }

        public NewsModel NewsQueryByTitle(string NewsTitle)
        {
            throw new NotImplementedException();
        }

        public bool NewsDeleteById(int NewsId)
        {
            throw new NotImplementedException();
        }

        public bool NewsDeleteByCategory(int NewsCategoryId)
        {
            throw new NotImplementedException();
        }

        public bool NewsDeleteByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public List<NewsModel> NewsQueryList()
        {
            throw new NotImplementedException();
        }

        public List<NewsModel> NewsQueryListByCategory(int NewsCategoryId)
        {
            throw new NotImplementedException();
        }

        public List<NewsModel> NewsQueryListByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }
    }
}
