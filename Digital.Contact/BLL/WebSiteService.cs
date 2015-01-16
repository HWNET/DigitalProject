using Digital.Contact.DAL;
using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.BLL
{
    public class WebSiteService
    {
        public WebSiteModel GetWebSiteModel(int CompanyId)
        {
            WebSiteModel Indexs = new WebSiteModel();
            Indexs.Description = "Test";
            Indexs.CompanyId = CompanyId;
            Indexs.KeyWords = "testKeywords";
            Indexs.WebTitle = "testTitle";
            Indexs.TemplatePath = "/Company/Template1";
            return Indexs;
        }
    }
}
