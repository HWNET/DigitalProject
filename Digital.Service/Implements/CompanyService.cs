using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;
using Digital.Contact.BLL;

namespace Digital.Service.Implements
{
    public partial class Service : ICompanyService
    {
        #region CompanyInsert
        public CompanyModel CompanyInsert(CompanyModel Model)
        {
            Model.UpdateStatus = 1;
            CompanyService CompanyService = new CompanyService();
            //Insert DB
            var Result=CompanyService.CompanyInsert(Model);
            //Insert Cache
            GenericList.CacheModelObj.CompanyModellist.Add(Model);
            return Result;
        }
        #endregion
        
        #region CompanyUpdate
        public CompanyModel CompanyUpdate(CompanyModel Model,int TabIndex)
        {
            var CompanyModel = CompanyQueryById(Model.CompanyID);
            /**/
            #region Instance Model
            if (TabIndex==1)
            {
                #region TAB ONE
                CompanyModel.CompanyName = Model.CompanyName; //公司名称
                CompanyModel.CompanyRegisteredNO = Model.CompanyRegisteredNO;  //工商注册号
                CompanyModel.CompanyTypeNO = Model.CompanyTypeNO;  //企业类型
                CompanyModel.CompanyMembers = Model.CompanyMembers;  //企业人数
                CompanyModel.CompanyBusinessModel = Model.CompanyBusinessModel;  //经营模式
                CompanyModel.IsProvideOEM = Model.IsProvideOEM;  //是否提供 OEM,ODM
                CompanyModel.PrimaryBusiness = Model.PrimaryBusiness;  //主营行业
                CompanyModel.PrimaryProduct = Model.PrimaryProduct;  //主营产品
                CompanyModel.PrimarySalesArea = Model.PrimarySalesArea;  //主要销售区域
                CompanyModel.CompanyBusinessProvince = Model.CompanyBusinessProvince;  //经营地址,省
                CompanyModel.CompanyBusinessCity = Model.CompanyBusinessCity;  //经营地址,市
                CompanyModel.CompanyIntro = Model.CompanyIntro; //公司简介
                #endregion

                #region TAB ONE -- UI Models
                CompanyModel.CompanyTypeMode = new CompanyTypeMode { Id = CompanyModel.CompanyTypeNO, Name = GenericList.CacheModelObj.CompanyTypeModelist.Where(o => o.Id == CompanyModel.CompanyTypeNO).SingleOrDefault().Name };
                CompanyModel.CompanyMemberMode = new CompanyMemberMode { Id = CompanyModel.CompanyMembers, Name = GenericList.CacheModelObj.CompanyMemberModelist.Where(o => o.Id == CompanyModel.CompanyMembers).SingleOrDefault().Name };
                CompanyModel.CompanyBusinessMode = new CompanyBusinessMode { Id = CompanyModel.CompanyBusinessModel, Name = GenericList.CacheModelObj.CompanyBusinessModelist.Where(o => o.Id == CompanyModel.CompanyBusinessModel).SingleOrDefault().Name };

                CompanyModel.PrimaryBusinessCategoryMode = new PrimaryBusinessCategoryMode { Id = CompanyModel.PrimaryBusinessCategory, Name = GenericList.CacheModelObj.PrimaryBusinessCategoryModelist.Where(o => o.Id == CompanyModel.PrimaryBusinessCategory).SingleOrDefault().Name };
                var PrimaryBusinessName = string.Empty;
                foreach (var item in GenericList.CacheModelObj.PrimaryBusinessCategoryModelist)
                {
                    var mode = item.PrimaryBusinessList.Where(o => o.Id == CompanyModel.PrimaryBusiness).SingleOrDefault();
                    if (mode != null && mode.Id > 0)
                    {
                        PrimaryBusinessName = mode.Name;
                        break;
                    }
                }
                CompanyModel.PrimaryBusinessMode = new PrimaryBusinessMode { Id = CompanyModel.PrimaryBusiness, Name = PrimaryBusinessName };

                CompanyModel.PrimarySalesAreaMode = new PrimarySalesAreaMode { Id = CompanyModel.PrimarySalesArea, Name = GenericList.CacheModelObj.PrimarySalesAreaModelist.Where(o => o.Id == CompanyModel.PrimarySalesArea).SingleOrDefault().Name };
                CompanyModel.CompanyBusinessProvinceMode = new CompanyBusinessProvinceMode { Id = CompanyModel.CompanyBusinessProvince, Name = GenericList.CacheModelObj.CompanyBusinessProvinceModelist.Where(o => o.Id == CompanyModel.CompanyBusinessProvince).SingleOrDefault().Name };
                var BusinessCityName = string.Empty;
                foreach (var item in GenericList.CacheModelObj.CompanyBusinessProvinceModelist)
                {
                    var mode = item.CompanyBusinessCityModeList.Where(o => o.Id == CompanyModel.CompanyBusinessCity).SingleOrDefault();
                    if (mode != null && mode.Id > 0)
                    {
                        BusinessCityName = mode.Name;
                        break;
                    }
                }
                CompanyModel.CompanyBusinessCityMode = new CompanyBusinessCityMode { Id = CompanyModel.CompanyBusinessCity, Name = BusinessCityName };
                #endregion

            }
            else if (TabIndex==2)
            {
                #region TAB TWO
                CompanyModel.ProductionForm = Model.ProductionForm;  //生产形式
                CompanyModel.ServicesDomain = Model.ServicesDomain;  //服务领域
                CompanyModel.ProcessingMethod = Model.ProcessingMethod;  //加工方式
                CompanyModel.ProcessingCraft = Model.ProcessingCraft;  //加工工艺
                CompanyModel.EquipmentIntro = Model.EquipmentIntro;  //设备介绍
                CompanyModel.ResearchDepartMembers = Model.ResearchDepartMembers;  //研发部门人数
                CompanyModel.CapacityIntro = Model.CapacityIntro;  //产能介绍
                CompanyModel.CapacityIntroUnit = Model.CapacityIntroUnit;
                CompanyModel.AnnualBusinessVolume = Model.AnnualBusinessVolume;  //年营业额
                CompanyModel.AnnualExportsVolume = Model.AnnualExportsVolume;  //年出口额
                CompanyModel.ManagementSystemCertification = Model.ManagementSystemCertification;  //管理体系认证
                CompanyModel.ProductQualityCertification = Model.ProductQualityCertification;  //产品质量认证
                CompanyModel.QualityAssurance = Model.QualityAssurance; //质量控制
                CompanyModel.FactoryArea = Model.FactoryArea; //厂房面积
                CompanyModel.PrimaryEquipments = Model.PrimaryEquipments;//主要设备
                #endregion

                #region TAB TWO -- UI Models
                CompanyModel.ProductionFormMode = new ProductionFormMode { Id = CompanyModel.ProductionForm, Name = GenericList.CacheModelObj.ProductionFormModelist.Where(o => o.Id == CompanyModel.ProductionForm).SingleOrDefault().Name };
                CompanyModel.ServicesDomainMode = new ServicesDomainMode { Id = CompanyModel.ServicesDomain, Name = GenericList.CacheModelObj.ServicesDomainModelist.Where(o => o.Id == CompanyModel.ServicesDomain).SingleOrDefault().Name };
                CompanyModel.ProcessingMethodMode = new ProcessingMethodMode { Id = CompanyModel.ProcessingMethod, Name = GenericList.CacheModelObj.ProcessingMethodModelist.Where(o => o.Id == CompanyModel.ProcessingMethod).SingleOrDefault().Name };
                CompanyModel.ProcessingCraftMode = new ProcessingCraftMode { Id = CompanyModel.ProcessingCraft, Name = GenericList.CacheModelObj.ProcessingCraftModelist.Where(o => o.Id == CompanyModel.ProcessingCraft).SingleOrDefault().Name };
                CompanyModel.EquipmentIntroMode = new EquipmentIntroMode { Id = CompanyModel.EquipmentIntro, Name = GenericList.CacheModelObj.EquipmentIntroModelist.Where(o => o.Id == CompanyModel.EquipmentIntro).SingleOrDefault().Name };
                CompanyModel.ResearchDepartMemberMode = new CompanyMemberMode { Id = CompanyModel.ResearchDepartMembers, Name = GenericList.CacheModelObj.CompanyMemberModelist.Where(o => o.Id == CompanyModel.ResearchDepartMembers).SingleOrDefault().Name };
                CompanyModel.CapacityUnitMode = new CapacityUnitMode { Id = CompanyModel.CapacityIntroUnit, Name = GenericList.CacheModelObj.CapacityUnitModelist.Where(o => o.Id == CompanyModel.CapacityIntroUnit).SingleOrDefault().Name };
                CompanyModel.AnnualBusinessVolumeMode = new AnnualBusinessVolumeMode { Id = CompanyModel.AnnualBusinessVolume, Name = GenericList.CacheModelObj.AnnualBusinessVolumeModelist.Where(o => o.Id == CompanyModel.AnnualBusinessVolume).SingleOrDefault().Name };
                CompanyModel.AnnualExportsVolumeMode = new AnnualExportsVolumeMode { Id = CompanyModel.AnnualExportsVolume, Name = GenericList.CacheModelObj.AnnualExportsVolumeModelist.Where(o => o.Id == CompanyModel.AnnualExportsVolume).SingleOrDefault().Name };
                CompanyModel.ManagementSystemCertificationMode = new ManagementSystemCertificationMode { Id = CompanyModel.ManagementSystemCertification, Name = GenericList.CacheModelObj.ManagementSystemCertificationModelist.Where(o => o.Id == CompanyModel.ManagementSystemCertification).SingleOrDefault().Name };
                CompanyModel.ProductQualityCertificationMode = new ProductQualityCertificationMode { Id = CompanyModel.ProductQualityCertification, Name = GenericList.CacheModelObj.ProductQualityCertificationModelist.Where(o => o.Id == CompanyModel.ProductQualityCertification).SingleOrDefault().Name };
                CompanyModel.QualityAssuranceMode = new QualityAssuranceMode { Id = CompanyModel.QualityAssurance, Name = GenericList.CacheModelObj.QualityAssuranceModelist.Where(o => o.Id == CompanyModel.QualityAssurance).SingleOrDefault().Name };
                #endregion
            }
            else if (TabIndex==3)
            {
                #region TAB THREE
                CompanyModel.CompanyYearEstablished = Model.CompanyYearEstablished;//成立年份
                CompanyModel.CompanyWebsite = Model.CompanyWebsite;//企业网址
                CompanyModel.CompanyRegisteredAssets = Model.CompanyRegisteredAssets; //注册资本
                CompanyModel.CompanyRegisteredAssetsUnit = Model.CompanyRegisteredAssetsUnit;//注册资本,单位
                CompanyModel.CompanyRegisteredProvince = Model.CompanyRegisteredProvince;//注册地址,省
                CompanyModel.CompanyRegisteredCity = Model.CompanyRegisteredCity;//注册地址,市
                CompanyModel.CompanyCorporateRepresentative = Model.CompanyCorporateRepresentative;//法人代表
                CompanyModel.CompanyBankDeposit = Model.CompanyBankDeposit; //开户行
                CompanyModel.CompanyBankAccount = Model.CompanyBankAccount;  //开户行 账户
                #endregion

                #region TAB THREE -- UI Models
                CompanyModel.CompanyRegisteredAssetsUnitMode = new CompanyRegisteredAssetsUnitMode { Id = CompanyModel.CompanyRegisteredAssetsUnit, Name = GenericList.CacheModelObj.CompanyRegisteredAssetsUnitModelist.Where(o => o.Id == CompanyModel.CompanyRegisteredAssetsUnit).SingleOrDefault().Name };

                CompanyModel.CompanyRegisteredProvinceMode = new CompanyBusinessProvinceMode { Id = CompanyModel.CompanyRegisteredProvince, Name = GenericList.CacheModelObj.CompanyRegisteredProvinceModelist.Where(o => o.Id == CompanyModel.CompanyRegisteredProvince).SingleOrDefault().Name };
                var RegisteredCityName = string.Empty;
                foreach (var item in GenericList.CacheModelObj.CompanyRegisteredProvinceModelist)
                {
                    var mode = item.CompanyBusinessCityModeList.Where(o => o.Id == CompanyModel.CompanyRegisteredCity).SingleOrDefault();
                    if (mode != null && mode.Id > 0)
                    {
                        RegisteredCityName = mode.Name;
                        break;
                    }
                }
                CompanyModel.CompanyRegisteredCityMode = new CompanyBusinessCityMode { Id = CompanyModel.CompanyRegisteredCity, Name = RegisteredCityName };
                #endregion
            }
            #endregion
            //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
            CompanyModel.UpdateStatus = 2;

            //Update Cache
            var CompanyModelCache = GenericList.CacheModelObj.CompanyModellist.Where(o => o.CompanyID == CompanyModel.CompanyID).SingleOrDefault();
            GenericList.CacheModelObj.CompanyModellist.Remove(CompanyModelCache);
            GenericList.CacheModelObj.CompanyModellist.Add(CompanyModel);

            //Insert Buffer
            GenericList.InsertBuffer(null, CompanyModel);
            return CompanyModel;
        }
        #endregion

        #region CompanyQueryById
        public CompanyModel CompanyQueryById(int CompanyId)
        {
            var CompanyModelCache = GenericList.CacheModelObj.CompanyModellist.Where(o => o.CompanyID == CompanyId).SingleOrDefault();
            if (CompanyModelCache != null) // can found in cache
            {
                return CompanyModelCache;
            }
            else // can not found in cache
            {
                CompanyService CompanyService = new CompanyService();
                var CompanyModel=CompanyService.CompanyQueryById(CompanyId);

                //Cache Company Model
                if (CompanyModel!=null) // insert data into cache
                {
                    GenericList.CacheModelObj.CompanyModellist.Add(CompanyModel);
                }
                return CompanyModel;
            }
        }
        #endregion

        #region CompanyQueryByName
        public CompanyModel CompanyQueryByName(string CompanyName)
        {
            var CompanyModelCache = GenericList.CacheModelObj.CompanyModellist.Where(o => o.CompanyName == CompanyName).SingleOrDefault();
            if (CompanyModelCache!=null)
            {
                return CompanyModelCache;
            }
            else
            {
                //get model from DB
                CompanyService CompanyService = new CompanyService();
                var CompanyModel = CompanyService.CompanyQueryByName(CompanyName);
                //insert cache
                if (CompanyModel!=null)
                {
                    GenericList.CacheModelObj.CompanyModellist.Add(CompanyModel);
                }
                return CompanyModel;
            }
        }
        #endregion

        #region CompanyDeleteById
        public bool CompanyDeleteById(int CompanyId)
        {
            var CompanyModel = CompanyQueryById(CompanyId);
            //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
            CompanyModel.UpdateStatus = 3;

            //update cache
            var CompanyModelCache = GenericList.CacheModelObj.CompanyModellist.Where(o => o.CompanyID == CompanyModel.CompanyID).SingleOrDefault();
            GenericList.CacheModelObj.CompanyModellist.Remove(CompanyModelCache);
            //insert buffer
            GenericList.InsertBuffer(null, CompanyModel);
            return true;
        }
        #endregion

        #region CompanyDisposeByNo
        public bool CompanyDisposeByNo(string CompanyRegisteredNO)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CompanyQueryList
        public List<CompanyModel> CompanyQueryList()
        {
            var CompanyListCache = GenericList.CacheModelObj.CompanyModellist;
            if (CompanyListCache!=null)
            {
                return CompanyListCache;
            }
            else
            {
                CompanyService CompanyService = new CompanyService();
                var CompanyList = CompanyService.CompanyQueryList();

                //insert cache
                if (CompanyList!=null)
                {
                    GenericList.CacheModelObj.CompanyModellist = CompanyList;
                }
                return CompanyList;
            }
        }
        #endregion

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
