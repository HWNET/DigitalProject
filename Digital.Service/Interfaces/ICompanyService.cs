using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "ICompanyService", SessionMode = SessionMode.Required)]
    public interface ICompanyService
    {
        [OperationContract]
        CompanyModel CompanyInsert(CompanyModel Model);
        [OperationContract]
        CompanyModel CompanyUpdate(CompanyModel Model,int TabIndex);
        [OperationContract]
        CompanyModel CompanyQueryById(int CompanyId);
        [OperationContract]
        CompanyModel CompanyQueryByName(string CompanyName);
        [OperationContract]
        bool CompanyDeleteById(int CompanyId);
        [OperationContract]
        bool CompanyDisposeByNo(string CompanyRegisteredNO);
        [OperationContract]
        List<CompanyModel> CompanyQueryList();

        [OperationContract]
        List<CompanyTypeMode> GetCompanyTypeList();
        [OperationContract]
        List<CompanyMemberMode> GetCompanyMemberList();
        [OperationContract]
        List<CompanyBusinessMode> GetCompanyBusinessList();
        [OperationContract]
        List<PrimaryBusinessCategoryMode> GetPrimaryBusinessList();
        [OperationContract]
        List<PrimarySalesAreaMode> GetPrimarySalesAreaList();

        [OperationContract]
        List<ProductionFormMode> GetProductionFormList();
        [OperationContract]
        List<ServicesDomainMode> GetServicesDomainList();
        [OperationContract]
        List<ProcessingMethodMode> GetProcessingMethodList();
        [OperationContract]
        List<ProcessingCraftMode> GetProcessingCraftList();
        [OperationContract]
        List<EquipmentIntroMode> GetEquipmentIntroList();

        [OperationContract]
        List<CapacityUnitMode> GetCapacityUnitList();
        [OperationContract]
        List<AnnualBusinessVolumeMode> GetAnnualBusinessVolumeList();
        [OperationContract]
        List<AnnualExportsVolumeMode> GetAnnualExportsVolumeList();
        [OperationContract]
        List<ManagementSystemCertificationMode> GetManagementSystemCertificationList();
        [OperationContract]
        List<ProductQualityCertificationMode> GetProductQualityCertificationList();
        [OperationContract]
        List<QualityAssuranceMode> GetQualityAssuranceList();

        [OperationContract]
        List<CompanyYearEstablishedMode> GetCompanyYearEstablishedList();
        [OperationContract]
        List<CompanyRegisteredAssetsUnitMode> GetCompanyRegisteredAssetsUnitList();

    }
}
