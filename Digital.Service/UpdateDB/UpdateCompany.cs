using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Contact.BLL;

namespace Digital.Service
{
    public class UpdateCompany
    {
        public static void UpdateCompanyModel(CompanyModel Model)
        {
            if (Model != null && Model.UpdateStatus == 2) //2:update
            {
                Model.UpdateStatus = 0;
                //update DB
                CompanyService CompanyService = new CompanyService();
                var CompanyModelUpdated = CompanyService.CompanyUpdate(Model);
            }
        }

        public static void DeleteCompanyModel(CompanyModel Model)
        {
            if (Model!=null&&Model.UpdateStatus==3) //3:delete
            {
                Model.UpdateStatus = 0;
                //update DB
                CompanyService CompanyService = new CompanyService();
                CompanyService.CompanyDeleteById(Model.CompanyID);
            }
        }
    }
}
