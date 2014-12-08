using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Digital.WCFClient;
using Digital.WCFClient.ConfigService;
using Microsoft.AspNet.Identity;

namespace Digital.Web.Controllers
{
    public class CompanyController : BaseController
    {
        //
        // GET: /Company/Index  这个地方就是他的地址  下面的INDEX就是你页面名  Company就是文件夹的名字 你要现实页面 下面代码必须有  如果你的页面叫LIST   
        public ActionResult Index()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            //this code must be there  ,this use masterpage in this view  so  must  use this code
            return View();
        }


        /// <summary>
        /// 账户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountInfo()
        {
            ViewBag.MenuModel = base.GetMenu(101);
            return View();
        }

        /// <summary>
        /// 账户安全
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountSecurity()
        {
            ViewBag.MenuModel = base.GetMenu(103);
            return View();
        }

        /// <summary>
        /// 交易资料
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountTransactionInfo()
        {
            ViewBag.MenuModel = base.GetMenu(104);
            return View();
        }


        //AccountSubUserManage
        /// <summary>
        /// 子账户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SubUserManage()
        {
            ViewBag.MenuModel = base.GetMenu(102);
            return View();
        }


        /// <summary>
        /// 企业信息中心/基础信息
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyBaseInfo()
        {
            ViewBag.MenuModel = base.GetMenu(151);

            //drop down 企业类型
            //drop down 企业人数,研发部门人数
            //radio group 经营模式
            //drop down cascading 主营行业
            //drop down 销售区域
            SetCompanyTabOne();

            //drop down 生产形式
            //drop down 服务领域
            //drop down 加工方式
            //drop down 加工工艺
            //drop down 设备介绍
            //drop down 研发部门人数
            //drop down 产能介绍
            //drop down 年营业额
            //drop down 年出口额
            //drop down 管理体系认证
            //drop down 产品质量认证
            //radio group 质量控制
            SetCompanyTabTwo();

            //drop down 企业成立年份
            //drop down 注册资本
            SetCompanyTabThree();

            //drop down cascading 经营地址,企业注册地

            //currnet log on user
            var CurrentUser = User.Identity.Name;
            ViewBag.CurrentUser = CurrentUser;
            if (!string.IsNullOrEmpty(CurrentUser))
            {
                var CompanyModel = new CompanyModel
                {
                    CompanyName = "CompanyName1111",
                    CompanyRegisteredNO = "CompanyRegisteredNO1111",
                    IsProvideOEM = true,
                    //CompanyBusinessAddress = "CompanyBusinessAddress1111",
                    CompanyIntro = "CompanyIntro111111"
                };
                //ViewBag.BusinessAddress = CompanyModel.CompanyBusinessProvinceMode.Name + CompanyModel.CompanyBusinessCityMode.Name;
                ViewBag.BusinessAddress = "BusinessAddress1111";
                return View(CompanyModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        #region CompanyBaseInfo -- FOR TAB ONE
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyBaseInfo(int CompanyID, int IsInsert, string CompanyName, string CompanyRegisteredNO, int CompanyTypeNO, int CompanyMembers,
            int CompanyBusinessModel, int IsProvideOEM, int PrimaryBusinessCategory, int PrimaryBusiness, string PrimaryProduct,
            int PrimarySalesArea, int CompanyBusinessProvince, int CompanyBusinessCity, string CompanyIntro)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            var ReturnResult = string.Empty;

            if (!string.IsNullOrEmpty(CurrentUser))
            {
                var NewModel = new CompanyModel
                {
                    CompanyID=CompanyID,
                    CompanyName = CompanyName,
                    CompanyRegisteredNO = CompanyRegisteredNO,
                    CompanyTypeNO = CompanyTypeNO,
                    CompanyMembers = CompanyMembers,
                    CompanyBusinessModel = CompanyBusinessModel,
                    IsProvideOEM = IsProvideOEM==1?true:false,
                    PrimaryBusinessCategory = PrimaryBusinessCategory,
                    PrimaryBusiness = PrimaryBusiness,
                    PrimaryProduct = PrimaryProduct,
                    PrimarySalesArea = PrimarySalesArea,
                    CompanyBusinessProvince = CompanyBusinessProvince,
                    CompanyBusinessCity = CompanyBusinessCity,
                    CompanyIntro = CompanyIntro,
                };

                if (IsInsert == 1) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.CompanyInsert(NewModel); // update DB
                    if (ResultModel!=null&&ResultModel.CompanyID > 0) // update web cache
                    {
                        OperatorFactory.UpdateCompanyCache(User.Identity.GetUserId(), ResultModel);

                        //do user upgrade
                        var UserUpgradeStatus = UpgradeUserToCompanyMember(ResultModel.CompanyID);
                        ReturnResult = UserUpgradeStatus?"OK":"NOK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                    #endregion
                }
                else // old existing model -- do update
                {
                    #region old existing model -- do update
                    var CompanyModel = client.CompanyUpdate(NewModel); // update DB
                    if (CompanyModel!=null&&CompanyModel.CompanyID > 0) // update web cache
                    {
                        OperatorFactory.UpdateCompanyCache(User.Identity.GetUserId(), CompanyModel);
                        ReturnResult = "OK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                    #endregion
                }
            }
            else
            {
                ReturnResult = "NOK";
            }
            client.Close();
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyBaseInfoCapacity -- FOR TAB TWO
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyBaseInfoCapacity(int CompanyID,int IsInsert, int ProductionForm,
            int ServicesDomain, int ProcessingMethod, int ProcessingCraft, int EquipmentIntro, int ResearchDepartMembers, int CapacityIntro, int CapacityIntroUnit,
            int AnnualBusinessVolume, int AnnualExportsVolume, int ManagementSystemCertification, int ProductQualityCertification,
            int QualityAssurance, string FactoryArea, string PrimaryEquipments)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var ReturnResult = string.Empty;
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            if (!string.IsNullOrEmpty(CurrentUser))
            {
                var NewModel = new CompanyModel
                {
                    CompanyID = CompanyID,
                    ProductionForm = ProductionForm,
                    ServicesDomain = ServicesDomain,
                    ProcessingMethod = ProcessingMethod,
                    ProcessingCraft = ProcessingCraft,
                    EquipmentIntro = EquipmentIntro,
                    ResearchDepartMembers = ResearchDepartMembers,
                    CapacityIntro = CapacityIntro,
                    CapacityIntroUnit = CapacityIntroUnit,
                    AnnualBusinessVolume = AnnualBusinessVolume,
                    AnnualExportsVolume = AnnualExportsVolume,
                    ManagementSystemCertification = ManagementSystemCertification,
                    ProductQualityCertification = ProductQualityCertification,
                    QualityAssurance = QualityAssurance,
                    FactoryArea =float.Parse(FactoryArea),
                    PrimaryEquipments = PrimaryEquipments,
                };
                if (IsInsert == 1) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.CompanyInsert(NewModel); // update DB
                    if (ResultModel !=null&& ResultModel.CompanyID > 0) // update web cache
                    {
                        OperatorFactory.UpdateCompanyCache(User.Identity.GetUserId(), ResultModel);

                        //do user upgrade
                        var UserUpgradeStatus = UpgradeUserToCompanyMember(ResultModel.CompanyID);
                        ReturnResult = UserUpgradeStatus ? "OK" : "NOK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                    #endregion
                }
                else // old existing model -- do update
                {
                    #region old existing model -- do update
                    var CompanyModel = client.CompanyUpdate(NewModel); // update DB
                    if (CompanyModel != null && CompanyModel.CompanyID > 0) // update web cache
                    {
                        OperatorFactory.UpdateCompanyCache(User.Identity.GetUserId(), CompanyModel);
                        ReturnResult = "OK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                    #endregion
                }
            }
            else
            {
                ReturnResult = "NOK";
            }
            client.Close();
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyBaseInfoMore -- FOR TAB THREE
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyBaseInfoMore(int CompanyID, int IsInsert, int CompanyYearEstablished, string CompanyWebsite, string CompanyRegisteredAssets,
            int CompanyRegisteredAssetsUnit, int CompanyRegisteredProvince, int CompanyRegisteredCity, string CompanyCorporateRepresentative,
            string CompanyBankDeposit, string CompanyBankAccount)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var ReturnResult = string.Empty;
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            if (!string.IsNullOrEmpty(CurrentUser))
            {
                var NewModel = new CompanyModel
                {
                    CompanyID = CompanyID,
                    CompanyYearEstablished = CompanyYearEstablished,
                    CompanyWebsite = CompanyWebsite,
                    CompanyRegisteredAssets = float.Parse(CompanyRegisteredAssets),
                    CompanyRegisteredAssetsUnit = CompanyRegisteredAssetsUnit,
                    CompanyRegisteredProvince = CompanyRegisteredProvince,
                    CompanyRegisteredCity = CompanyRegisteredCity,
                    CompanyCorporateRepresentative = CompanyCorporateRepresentative,
                    CompanyBankDeposit = CompanyBankDeposit,
                    CompanyBankAccount = CompanyBankAccount
                };

                if (IsInsert==1) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.CompanyInsert(NewModel); // update DB
                    if (ResultModel !=null&& ResultModel.CompanyID > 0) // update web cache
                    {
                        OperatorFactory.UpdateCompanyCache(User.Identity.GetUserId(), ResultModel);

                        //do user upgrade
                        var UserUpgradeStatus = UpgradeUserToCompanyMember(ResultModel.CompanyID);
                        ReturnResult = UserUpgradeStatus ? "OK" : "NOK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                    #endregion
                }
                else // old existing model -- do update
                {
                    #region old existing model -- do update
                    var CompanyModel = client.CompanyUpdate(NewModel); // update DB
                    if (CompanyModel != null && CompanyModel.CompanyID > 0) // update web cache
                    {
                        OperatorFactory.UpdateCompanyCache(User.Identity.GetUserId(), CompanyModel);
                        ReturnResult = "OK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                    #endregion
                }
            }
            else
            {
                ReturnResult = "NOK";
            }

            client.Close();
            return Content(ReturnResult);
        }
        #endregion

        #region UI Modes For Company Base Informations
        #region TAB ONE
        public void SetCompanyTabOne()
        {
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            var CompanyTypeList = client.GetCompanyTypeList();
            ViewBag.CompanyTypeList = CompanyTypeList;

            var CompanyMemberList = client.GetCompanyMemberList();
            ViewBag.CompanyMemberList = CompanyMemberList;

            var CompanyBusinessList = client.GetCompanyBusinessList();
            ViewBag.CompanyBusinessList = CompanyBusinessList;

            var PrimarySalesAreaList = client.GetPrimarySalesAreaList();
            ViewBag.PrimarySalesAreaList = PrimarySalesAreaList;

            client.Close();
        }
        public void SetPrimaryBusinessList()
        {
            ViewBag.PrimaryBusinessList = null;
        }
        #endregion

        #region TAB TWO
        public void SetCompanyTabTwo()
        {
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            var ProductionFormList = client.GetProductionFormList();
            ViewBag.ProductionFormList = ProductionFormList;

            var ServicesDomainList = client.GetServicesDomainList();
            ViewBag.ServicesDomainList = ServicesDomainList;

            var ProcessingMethodList = client.GetProcessingMethodList();
            ViewBag.ProcessingMethodList = ProcessingMethodList;

            var ProcessingCraftList = client.GetProcessingCraftList();
            ViewBag.ProcessingCraftList = ProcessingCraftList;

            var EquipmentIntroList = client.GetEquipmentIntroList();
            ViewBag.EquipmentIntroList = EquipmentIntroList;

            var CapacityUnitList = client.GetCapacityUnitList();
            ViewBag.CapacityUnitList = CapacityUnitList;

            var AnnualBusinessVolumeList = client.GetAnnualBusinessVolumeList();
            ViewBag.AnnualBusinessVolumeList = AnnualBusinessVolumeList;

            var AnnualExportsVolumeList = client.GetAnnualExportsVolumeList();
            ViewBag.AnnualExportsVolumeList = AnnualExportsVolumeList;

            var ManagementSystemCertificationList = client.GetManagementSystemCertificationList();
            ViewBag.ManagementSystemCertificationList = ManagementSystemCertificationList;

            var ProductQualityCertificationList = client.GetProductQualityCertificationList();
            ViewBag.ProductQualityCertificationList = ProductQualityCertificationList;

            var QualityAssuranceList = client.GetQualityAssuranceList();
            ViewBag.QualityAssuranceList = QualityAssuranceList;

            client.Close();
        }
        #endregion

        #region TAB THREE
        public void SetCompanyTabThree()
        {
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            var CompanyYearEstablishedList = client.GetCompanyYearEstablishedList();
            ViewBag.CompanyYearEstablishedList = CompanyYearEstablishedList;

            var RegisteredAssetsUnitList = client.GetCompanyRegisteredAssetsUnitList();
            ViewBag.RegisteredAssetsUnitList = RegisteredAssetsUnitList;
            client.Close();
        }
        #endregion

        #endregion

        public bool UpgradeUserToCompanyMember(int CompanyID)
        {
            bool Result = false;
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
            if (UserModel != null)
            {
                if (UserModel.CompanyID == null || (UserModel.CompanyID != null && string.IsNullOrEmpty(UserModel.CompanyID.Value.ToString())))
                {
                    //update web cache for UserModel
                    UserModel.CompanyID=CompanyID;
                    OperatorFactory.UpdateUserModelCache(User.Identity.GetUserId(), UserModel);

                    //update db cache for UserModel
                    client.UpdateUsersModel(UserModel);
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }
            else
            {
                Result = false;
            }
            client.Close();
            return Result;
        }

        public ActionResult CompanyBusinessDemand()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        /// <summary>
        /// 添加企业案例
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCasesAdd()
        {
            ViewBag.MenuModel = base.GetMenu(156);
            return View();
        }

        /// <summary>
        /// 企业案例分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCasesClassManage()
        {
            ViewBag.MenuModel = base.GetMenu(157);
            return View();
        }

        /// <summary>
        /// 企业案例列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCasesList()
        {
            ViewBag.MenuModel = base.GetMenu(158);
            return View();
        }

        /// <summary>
        /// 企业信誉
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCredibility()
        {
            ViewBag.MenuModel = base.GetMenu(153);
            return View();
        }

        /// <summary>
        /// 企业文件柜
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyFiles()
        {
            ViewBag.MenuModel = base.GetMenu(154);
            return View();
        }

        /// <summary>
        /// 添加企业播报
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyNewsAdd()
        {
            ViewBag.MenuModel = base.GetMenu(163);
            return View();
        }

        /// <summary>
        /// 企业播报分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyNewsClassManage()
        {
            ViewBag.MenuModel = base.GetMenu(164);
            return View();
        }

        /// <summary>
        /// 企业播报列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyNewsList()
        {
            ViewBag.MenuModel = base.GetMenu(165);
            return View();
        }


        /// <summary>
        /// 添加企业案例
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyPatentAdd()
        {
            ViewBag.MenuModel = base.GetMenu(162);
            return View();
        }

        /// <summary>
        /// 案例列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyPatentList()
        {
            ViewBag.MenuModel = base.GetMenu(161);
            return View();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyPictureUpload()
        {
            ViewBag.MenuModel = base.GetMenu(155);
            return View();
        }

        /// <summary>
        /// 资质介绍
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyQualificationCertificateList()
        {
            ViewBag.MenuModel = base.GetMenu(152);
            return View();
        }

        /// <summary>
        /// 单页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanySinglePage()
        {
            ViewBag.MenuModel = base.GetMenu(160);
            return View();
        }

        /// <summary>
        /// 添加单页
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanySinglePageAdd()
        {
            ViewBag.MenuModel = base.GetMenu(159);
            return View();
        }

        /// <summary>
        /// 水印设置
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyWatermarkManage()
        {
            ViewBag.MenuModel = base.GetMenu(166);
            return View();
        }

    }
}