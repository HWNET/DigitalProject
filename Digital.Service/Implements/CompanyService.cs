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

        #region UI Modes For Company Base Informations
        public List<CompanyTypeMode> GetCompanyTypeList()
        {
            var modes = GenericList.CacheModelObj.CompanyTypeModelist;
            if (modes!=null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<CompanyMemberMode> GetCompanyMemberList()
        {
            var modes = GenericList.CacheModelObj.CompanyMemberModelist;
            if (modes!=null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<CompanyBusinessMode> GetCompanyBusinessList()
        {
            var modes = GenericList.CacheModelObj.CompanyBusinessModelist;
            if (modes!=null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<PrimaryBusinessCategoryMode> GetPrimaryBusinessList()
        {
            var modes = GenericList.CacheModelObj.PrimaryBusinessCategoryModelist;
            if (modes!=null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<PrimarySalesAreaMode> GetPrimarySalesAreaList()
        {
            var modes = GenericList.CacheModelObj.PrimarySalesAreaModelist;
            if (modes!=null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
