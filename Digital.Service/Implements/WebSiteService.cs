using Digital.Contact.BLL;
using Digital.Contact.Models;
using Digital.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Service.Implements
{
    public partial class Service : IWebSiteService
    {
        public WebSiteModel GetIndexModel(int CompanyId)
        {
            WebSiteModel Model = new WebSiteModel();
            try
            {
                WebSiteService IndexSvr = new WebSiteService();
                Model = IndexSvr.GetWebSiteModel(CompanyId);
            }
            catch (Exception ex)
            {

            }
            return Model;
        }
    }
}
