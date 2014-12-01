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
        #region TAB ONE
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

        #region TAB TWO
        public List<ProductionFormMode> GetProductionFormList()
        {
            var modes = GenericList.CacheModelObj.ProductionFormModelist;
            if (modes!=null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<ServicesDomainMode> GetServicesDomainList()
        {
            var modes = GenericList.CacheModelObj.ServicesDomainModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<ProcessingMethodMode> GetProcessingMethodList()
        {
            var modes = GenericList.CacheModelObj.ProcessingMethodModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<ProcessingCraftMode> GetProcessingCraftList()
        {
            var modes = GenericList.CacheModelObj.ProcessingCraftModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<EquipmentIntroMode> GetEquipmentIntroList()
        {
            var modes = GenericList.CacheModelObj.EquipmentIntroModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<CapacityUnitMode> GetCapacityUnitList()
        {
            var modes = GenericList.CacheModelObj.CapacityUnitModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<AnnualBusinessVolumeMode> GetAnnualBusinessVolumeList()
        {
            var modes = GenericList.CacheModelObj.AnnualBusinessVolumeModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<AnnualExportsVolumeMode> GetAnnualExportsVolumeList()
        {
            var modes = GenericList.CacheModelObj.AnnualExportsVolumeModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<ManagementSystemCertificationMode> GetManagementSystemCertificationList()
        {
            var modes = GenericList.CacheModelObj.ManagementSystemCertificationModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<ProductQualityCertificationMode> GetProductQualityCertificationList()
        {
            var modes = GenericList.CacheModelObj.ProductQualityCertificationModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<QualityAssuranceMode> GetQualityAssuranceList()
        {
            var modes = GenericList.CacheModelObj.QualityAssuranceModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region TAB THREE
        public List<CompanyYearEstablishedMode> GetCompanyYearEstablishedList()
        {
            var modes = GenericList.CacheModelObj.CompanyYearEstablishedModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }

        public List<CompanyRegisteredAssetsUnitMode> GetCompanyRegisteredAssetsUnitList()
        {
            var modes = GenericList.CacheModelObj.CompanyRegisteredAssetsUnitModelist;
            if (modes != null)
            {
                return modes;
            }
            else
            {
                return null;
            }
        }
        #endregion
        
        #endregion





    }
}
