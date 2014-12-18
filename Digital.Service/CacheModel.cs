using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
namespace Digital.Service
{
    public  class CacheModel
    {
        /// <summary>
        /// ModelName+"list"
        /// </summary>
        public  List<Digital.Contact.Models.MenuModel> MenuModellist { get; set; }
        public Dictionary<int ,Digital.Contact.Models.UsersModel> UserModellist { get; set; }
        public  List<Digital.Contact.Models.SkillsModel> SkillsModellist { get; set; }
        public List<Digital.Contact.Models.ProvinceModel> ProvinceModellist { get; set; }
        public List<Digital.Contact.Models.CommonRightModel> CommonRightModellist { get; set; }


        public List<CompanyTypeMode> CompanyTypeModelist { get; set; }
        public List<CompanyMemberMode> CompanyMemberModelist { get; set; }
        public List<CompanyBusinessMode> CompanyBusinessModelist { get; set; }
        public List<PrimaryBusinessCategoryMode> PrimaryBusinessCategoryModelist { get; set; }
        public List<PrimarySalesAreaMode> PrimarySalesAreaModelist { get; set; }
        public List<CompanyBusinessProvinceMode> CompanyBusinessProvinceModelist { get; set; }
        public List<CompanyBusinessCityMode> CompanyBusinessCityModelist { get; set; }

        public List<ProductionFormMode> ProductionFormModelist { get; set; }
        public List<ServicesDomainMode> ServicesDomainModelist { get; set; }
        public List<ProcessingMethodMode> ProcessingMethodModelist { get; set; }
        public List<ProcessingCraftMode> ProcessingCraftModelist { get; set; }
        public List<EquipmentIntroMode> EquipmentIntroModelist { get; set; }
        public List<CapacityUnitMode> CapacityUnitModelist { get; set; }
        public List<AnnualBusinessVolumeMode> AnnualBusinessVolumeModelist { get; set; }
        public List<AnnualExportsVolumeMode> AnnualExportsVolumeModelist { get; set; }
        public List<ManagementSystemCertificationMode> ManagementSystemCertificationModelist { get; set; }
        public List<ProductQualityCertificationMode> ProductQualityCertificationModelist { get; set; }
        public List<QualityAssuranceMode> QualityAssuranceModelist { get; set; }

        public List<CompanyYearEstablishedMode> CompanyYearEstablishedModelist { get; set; }
        public List<CompanyRegisteredAssetsUnitMode> CompanyRegisteredAssetsUnitModelist { get; set; }
        public List<CompanyBusinessProvinceMode> CompanyRegisteredProvinceModelist { get; set; }
        public List<CompanyBusinessCityMode> CompanyRegisteredCityModelist { get; set; }

        public List<CompanyModel> CompanyModellist { get; set; }

        public List<CasesCategoryModel> CasesCategoryModellist { get; set; }
        public List<CasesModel> CasesModellist { get; set; }

        public List<Digital.Contact.Models.NewsCategoryModel> NewsCategoryModelList { get; set; }
        public List<Digital.Contact.Models.NewsModel> NewsModelList { get; set; }

        public List<TechnologyDomainMode> TechnologyDomainModelist { get; set; }
        public List<DevelopmentStatusMode> DevelopmentStatusModelist { get; set; }
        public List<Digital.Contact.Models.PatentModel> PatentModelList { get; set; }
        
        public List<Digital.Contact.Models.SinglePageModel> SinglePageModelList { get; set; }

    }
}
