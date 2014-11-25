using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class CompanyModel
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; } //公司名称
        public string CompanyRegisteredNO { get; set; } //工商注册号
        public string CompanyTypeNO { get; set; } //企业类型
        public string CompanyMembers { get; set; } //企业人数
        public string CompanyBusinessModel { get; set; } //经营模式
        public bool IsProvideOEM { get; set; } //是否提供 OEM,ODM
        public string PrimaryBusiness { get; set; } //主营行业
        public string PrimaryProduct { get; set; } //主营产品
        public string PrimarySalesArea { get; set; } //主要销售区域
        public string CompanyBusinessAddress { get; set; } //经营地址
        public string CompanyIntro { get; set; } //公司简介

        public string ProductionForm { get; set; } //生产形式
        public string ServicesDomain { get; set; } //服务领域
        public string ProcessingMethod { get; set; } //加工方式
        public string ProcessingCraft { get; set; } //加工工艺
        public string EquipmentIntro { get; set; } //设备介绍
        public string ResearchDepartMembers { get; set; } //研发部门人数
        public string CapacityIntro { get; set; } //产能介绍
        public string AnnualBusinessVolume {get;set;} //年营业额
        public string AnnualExportsVolume { get; set; } //年出口额
        public string ManagementSystemCertification {get;set;} //管理体系认证
        public string ProductQualityCertification { get; set; } //产品质量认证
        public string QualityAssurance { get; set; } //质量控制
        public string FactoryArea { get; set; } //厂房面积
        public string PrimaryEquipments { get; set; } //主要设备

        public int CompanyYearEstablished { get; set; } //成立年份
        public string CompanyWebsite { get; set; } //企业网址
        public string CompanyRegisteredAssets { get; set; } //注册资本
        public string CompanyRegisteredAddress { get; set; } //注册地址
        public string CompanyCorporateRepresentative { get; set; } //法人代表
        public string CompanyBankDeposit { get; set; } //开户行
        public string CompanyBankAccount { get; set; } //开户行 账户

        /// <summary>
        /// 企业用户类型 1 方案商  2 成品商  3  供应商
        /// </summary>
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

        public virtual ICollection<DepartmentModel> DepartmentModels { get; set; }
        public virtual ICollection<CasesCategoryModel> CasesCategoryModels { get; set; } //企业案例 类别集合
        public virtual ICollection<SinglePageModel> SinglePageModels { get; set; } //企业单页集合
        public virtual ICollection<PatentModel> PatentModels { get; set; } //企业专利集合
        public virtual ICollection<NewsCategoryModel> NewsCategoryModels { get; set; } //企业播报 类别集合
    }
}
