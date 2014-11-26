using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Common.Logging;
using Digital.Contact.DAL;
using Digital.Contact.Models;

namespace Digital.Contact.BLL
{
    public class CompanyService
    {
        private bool IsModelExist(CompanyModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.CompanyModels.Where(o => o.CompanyID == Model.CompanyID).FirstOrDefault();
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        public bool CompanyInsert(CompanyModel Model)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    if (!IsModelExist(Model))
                    {
                        db.CompanyModels.Add(Model);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return false;
                }
            }
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
