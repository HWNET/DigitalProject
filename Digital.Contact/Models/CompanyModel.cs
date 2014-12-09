using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("Company",Schema="dbo")]
    public class CompanyModel
    {
        [Key]
        public int CompanyID { get; set; }

        //[Required(ErrorMessage = "必填")]
        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "公司名称")]
        public string CompanyName { get; set; } //公司名称

        //[Required(ErrorMessage = "必填")]
        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "工商注册号")]
        public string CompanyRegisteredNO { get; set; } //工商注册号

        public int CompanyTypeNO { get; set; } //企业类型
        [NotMapped]
        public CompanyTypeMode CompanyTypeMode { get; set; }

        public int CompanyMembers { get; set; } //企业人数
        [NotMapped]
        public CompanyMemberMode CompanyMemberMode { get; set; }

        public int CompanyBusinessModel { get; set; } //经营模式
        [NotMapped]
        public CompanyBusinessMode CompanyBusinessMode { get; set; }
        
        public bool IsProvideOEM { get; set; } //是否提供 OEM,ODM
        
        public int PrimaryBusinessCategory { get; set; } //主营行业
        [NotMapped]
        public PrimaryBusinessCategoryMode PrimaryBusinessCategoryMode { get; set; }
        public int PrimaryBusiness { get; set; } //主营行业
        [NotMapped]
        public PrimaryBusinessMode PrimaryBusinessMode { get; set; }

        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "主营产品")]
        public string PrimaryProduct { get; set; } //主营产品,5个input value 拼接

        public int PrimarySalesArea { get; set; } //主要销售区域
        [NotMapped]
        public PrimarySalesAreaMode PrimarySalesAreaMode { get; set; }

        public int CompanyBusinessProvince { get; set; } //经营地址,省份
        [NotMapped]
        public CompanyBusinessProvinceMode CompanyBusinessProvinceMode { get; set; }
        public int CompanyBusinessCity { get; set; } //经营地址,市份
        [NotMapped]
        public CompanyBusinessCityMode CompanyBusinessCityMode { get; set; }

        //[Required(ErrorMessage = "必填")]
        //[DataType(DataType.Text)]
        //[Display(Name = "公司简介")]
        public string CompanyIntro { get; set; } //公司简介

        public int ProductionForm { get; set; } //生产形式
        [NotMapped]
        public ProductionFormMode ProductionFormMode { get; set; }

        public int ServicesDomain { get; set; } //服务领域
        [NotMapped]
        public ServicesDomainMode ServicesDomainMode { get; set; }

        public int ProcessingMethod { get; set; } //加工方式
        [NotMapped]
        public ProcessingMethodMode ProcessingMethodMode { get; set; }

        public int ProcessingCraft { get; set; } //加工工艺
        [NotMapped]
        public ProcessingCraftMode ProcessingCraftMode { get; set; }

        public int EquipmentIntro { get; set; } //设备介绍
        [NotMapped]
        public EquipmentIntroMode EquipmentIntroMode { get; set; }

        public int ResearchDepartMembers { get; set; } //研发部门人数
        [NotMapped]
        public CompanyMemberMode ResearchDepartMemberMode { get; set; }

        public int CapacityIntro { get; set; } //产能介绍
        public int CapacityIntroUnit { get; set; } //产能介绍,单位
        [NotMapped]
        public CapacityUnitMode CapacityUnitMode { get; set; }

        public int AnnualBusinessVolume {get;set;} //年营业额
        [NotMapped]
        public AnnualBusinessVolumeMode AnnualBusinessVolumeMode { get; set; }

        public int AnnualExportsVolume { get; set; } //年出口额
        [NotMapped]
        public AnnualExportsVolumeMode AnnualExportsVolumeMode { get; set; }

        public int ManagementSystemCertification { get; set; } //管理体系认证
        [NotMapped]
        public ManagementSystemCertificationMode ManagementSystemCertificationMode { get; set; }

        public int ProductQualityCertification { get; set; } //产品质量认证
        [NotMapped]
        public ProductQualityCertificationMode ProductQualityCertificationMode { get; set; }

        public int QualityAssurance { get; set; } //质量控制
        [NotMapped]
        public QualityAssuranceMode QualityAssuranceMode { get; set; }

        public float FactoryArea { get; set; } //厂房面积
        //[Display(Name = "主要设备")]
        public string PrimaryEquipments { get; set; } //主要设备,5个input value 拼接

        public int CompanyYearEstablished { get; set; } //成立年份
        //[Display(Name = "企业网址")]
        public string CompanyWebsite { get; set; } //企业网址
        
        public float CompanyRegisteredAssets { get; set; } //注册资本
        public int CompanyRegisteredAssetsUnit { get; set; }
        [NotMapped]
        public CompanyRegisteredAssetsUnitMode CompanyRegisteredAssetsUnitMode { get; set; } //注册资本,单位

        public int CompanyRegisteredProvince { get; set; } //注册地址,省份
        [NotMapped]
        public CompanyBusinessProvinceMode CompanyRegisteredProvinceMode { get; set; }
        public int CompanyRegisteredCity { get; set; } //注册地址,市份
        [NotMapped]
        public CompanyBusinessCityMode CompanyRegisteredCityMode { get; set; }

        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "法人代表")]
        public string CompanyCorporateRepresentative { get; set; } //法人代表

        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "开户行")]
        public string CompanyBankDeposit { get; set; } //开户行

        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "账户")]
        public string CompanyBankAccount { get; set; } //开户行 账户

        //企业用户类型 1 方案商  2 成品商  3  供应商
        //public int CompanyUserTypeID { get; set; }
        //public int CompanyUserLevelID { get; set; } //企业 总账户 : 0 即企业Admin, 子账户 : 1 即只能在所属部门进行操作
        //public bool IsHasModules { get; set; }

        //public int ParentCompanyID { get; set; } //企业 总账户 : ParentCompanyID=0  子账户 : ParentCompanyID=CompanyID 
        //public int DepartmentID { get; set; } //子账户所属部门ID
        //public bool IsHeader { get; set; } //子账户是否部门负责人
        //public int HeaderTitle { get; set; } //部门负责人头衔 ： 1 , 2 , 3 
        //public bool IsDisabled { get; set; }//子账户 是否停用

        //public string SendAddress { get; set; } //???部门子账户 发货地址 , 最多保存 5 个有效地址；允许设置一个默认地址
        //public string ReceiveAddress { get; set; } //???部门子账户 收货地址

        public int UpdateStatus { get; set; } //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 

        public virtual List<DepartmentModel> DepartmentModels { get; set; }
        public virtual List<CasesCategoryModel> CasesCategoryModels { get; set; } //企业案例 类别集合
        public virtual List<SinglePageModel> SinglePageModels { get; set; } //企业单页集合
        public virtual List<PatentModel> PatentModels { get; set; } //企业专利集合
        public virtual List<NewsCategoryModel> NewsCategoryModels { get; set; } //企业播报 类别集合
    }
}
