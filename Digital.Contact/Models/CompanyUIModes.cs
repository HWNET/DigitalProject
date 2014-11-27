using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.Models
{
    /// <summary>
    /// UI Modes for Company Base Infos
    /// </summary>
    public class BaseMode
    {
        public string Name { get; set; }
    }

    public class BaseNameValueMode
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    //---------------1.Tab Base Infos
    //for dropdown CompanyType
    public class CompanyTypeMode : BaseNameValueMode { }
    //for dropdown CompanyMembers
    public class CompanyMemberMode : BaseNameValueMode { }
    //for radiogroup CompanyBusiness
    public class CompanyBusinessMode : BaseNameValueMode { }
    //for dropdown1 PrimaryBusinessCategory
    public class PrimaryBusinessCategoryMode : BaseNameValueMode { }
    //for dropdown2 PrimaryBusiness
    public class PrimaryBusinessMode : BaseNameValueMode { }
    //for selectlist PrimarySalesArea
    public class PrimarySalesAreaMode : BaseNameValueMode { }
    //--------CompanyBusinessAddress
    //for dropdown1 CompanyBusinessProvince
    public class CompanyBusinessProvinceMode : BaseNameValueMode { }
    //for dropdown2 CompanyBusinessCity
    public class CompanyBusinessCityMode : BaseNameValueMode { }
    //for dropdown3 CompanyBusinessArea
    public class CompanyBusinessAreaMode : BaseNameValueMode { }

    //----------------2.Tab Production Capacity
    //for dropdown ProductionForm
    public class ProductionFormMode : BaseNameValueMode { }
    //for dropdown ServicesDomain
    public class ServicesDomainMode : BaseNameValueMode { }
    //for dropdown ProcessingMethod
    public class ProcessingMethodMode : BaseNameValueMode { }
    //for dropdown ProcessingCraft
    public class ProcessingCraftMode : BaseNameValueMode { }
    //for dropdown EquipmentIntro
    public class EquipmentIntroMode : BaseNameValueMode { }
    //for dropdown CapacityIntro
    public class CapacityUnitMode : BaseNameValueMode { }
    //for dropdown AnnualBusinessVolume
    public class AnnualBusinessVolumeMode : BaseNameValueMode { }
    //for dropdown AnnualExportsVolume
    public class AnnualExportsVolumeMode : BaseNameValueMode { }
    //for dropdown ManagementSystemCertification
    public class ManagementSystemCertificationMode : BaseNameValueMode { }
    //for dropdown ProductQualityCertification
    public class ProductQualityCertificationMode : BaseNameValueMode { }
    //for dropdown QualityAssurance
    public class QualityAssuranceMode : BaseNameValueMode { }

    //------------------3.Tab More Infos
    //for dropdown CompanyYearEstablished
    public class CompanyYearEstablishedMode : BaseNameValueMode { }
    //for dropdown CompanyRegisteredAssets
    public class CompanyRegisteredAssetsUnitMode : BaseNameValueMode { }

}
