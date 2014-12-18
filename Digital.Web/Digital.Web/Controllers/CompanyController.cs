using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Digital.WCFClient;
using Digital.WCFClient.ConfigService;
using Microsoft.AspNet.Identity;
using Digital.Common.Utilities;

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

        #region CompanyBaseInfo
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
            //var CurrentUser = User.Identity.Name;
            //ViewBag.CurrentUser = CurrentUser;
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            var CompanyID = 0;
            CompanyModel CompanyModel = null;

            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                CompanyID = UserModel.CompanyID.Value;
                CompanyModel=client.CompanyQueryById(CompanyID);
                if (CompanyModel.CompanyBusinessProvinceMode != null && CompanyModel.CompanyBusinessCityMode!=null)
                {
                    ViewBag.BusinessAddress = CompanyModel.CompanyBusinessProvinceMode.Name + CompanyModel.CompanyBusinessCityMode.Name;
                }
                else
                {
                    ViewBag.BusinessAddress = "BusinessAddress1111";
                }
            }
            else
            {
                CompanyModel = new CompanyModel
                {
                    CompanyName = "CompanyName1111",
                    CompanyRegisteredNO = "CompanyRegisteredNO1111",
                    IsProvideOEM = true,
                    //CompanyBusinessAddress = "CompanyBusinessAddress1111",
                    CompanyIntro = "CompanyIntro111111"
                };
                ViewBag.BusinessAddress = "BusinessAddress1111";
                //return HttpNotFound();
            }
            
            client.Close();
            return View(CompanyModel);
        }
        #endregion

        #region CompanyBaseInfo -- FOR TAB ONE
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyBaseInfoTEST(string CompanyID)
        {
            return Content(CompanyID);
        }



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
                    var CompanyModel = client.CompanyUpdate(NewModel,1); // update DB
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
                    var CompanyModel = client.CompanyUpdate(NewModel,2); // update DB
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
            if (IsInsert == 1 && ReturnResult == "OK")
            {
                return Redirect("../Users/Login");
            }
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
                    var CompanyModel = client.CompanyUpdate(NewModel,3); // update DB
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
            if (IsInsert == 1 && ReturnResult == "OK")
            {
                return Redirect("../Users/Login");
            }
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

        #region Upgrade UserToCompanyMember
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
        #endregion

        public ActionResult CompanyBusinessDemand()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        #region CompanyCasesAdd
        /// <summary>
        /// 添加企业案例
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCasesAdd()
        {
            ViewBag.MenuModel = base.GetMenu(156);

            CasesModel CategoryModel = new CasesModel {
                CasesName = "CasesName1111",
                CasesAbstract = "CasesAbstract1111",
                CasesDate=DateTime.Now,
                CasesOrderBy="1",
                CasesLabels="CasesLabels1111",
                CasesDetails="CasesDetails1111",
            };
            return View(CategoryModel);
        }
        #endregion

        #region CompanyCasesClassManage
        /// <summary>
        /// 企业案例分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCasesClassManage()
        {
            ViewBag.MenuModel = base.GetMenu(157);

            var CompanyID = 0;
            List<CasesCategoryModel> CategoryList = null;
            var client = ServiceHub.GetCommonServiceClient<CasesCategoryServiceClient>();

            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) 
            {
                CompanyID = UserModel.CompanyID.Value;
                CategoryList = client.CasesCategoryQueryListByCompany(CompanyID).ToList();

                ViewBag.CategoryList = CategoryList;
            }
            
            return View();
        }
        #endregion

        #region CompanyCasesList
        /// <summary>
        /// 企业案例列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCasesList()
        {
            ViewBag.MenuModel = base.GetMenu(158);
            return View();
        }
        #endregion

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

        #region CompanyNewsAdd
        /// <summary>
        /// 添加企业播报
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult CompanyNewsAdd()
        {
            ViewBag.MenuModel = base.GetMenu(163);

            NewsModel NewsModel = new NewsModel {
                NewsTitle = "NewsTitle0000",
                NewsAbstract = "NewsAbstract0000",
                NewsThumbnail = "NewsThumbnail0000",
                NewsCategoryID=0,
                NewsOrderID=0,
                NewsKeywords = "NewsKeywords0000",
                NewsLabels = "NewsLabels0000",
                NewsBody = "NewsBody0000",
                ReleaseTime=DateTime.Now,
            };

            var client = ServiceHub.GetCommonServiceClient<NewsCategoryServiceClient>();
            var CompanyID = 0;
            List<NewsCategoryModel> CategoryList = null;

            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                CompanyID = UserModel.CompanyID.Value;
                CategoryList = client.NewsCategoryQueryListByCompany(CompanyID).ToList();
            }
            ViewBag.CategoryList = CategoryList;

            return View(NewsModel);
        }
        #endregion

        #region CompanyNewsSave
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CompanyNewsSave(int CompanyID, int IsInsert, string NewsTitle, string NewsAbstract,
            string NewsThumbnail, int NewsCategoryID, int NewsOrderID, string NewsKeywords, string NewsLabels,
            string NewsBody)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var client = ServiceHub.GetCommonServiceClient<NewsServiceClient>();
            var ReturnResult = string.Empty;
            if (!string.IsNullOrEmpty(CurrentUser) && CompanyID>0)
            {
                var NewsModel = new NewsModel
                { 
                    NewsTitle=NewsTitle,
                    NewsAbstract=NewsAbstract,
                    NewsThumbnail = NewsThumbnail,
                    NewsCategoryID = NewsCategoryID,
                    NewsOrderID = NewsOrderID,
                    NewsKeywords = NewsKeywords,
                    NewsLabels = NewsLabels,
                    NewsBody = NewsBody,
                    ReleaseTime=DateTime.Now,
                };
                if (IsInsert == 1) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.NewsInsert(NewsModel); // update DB
                    if (ResultModel != null && ResultModel.NewsID > 0) // update web cache
                    {
                        //OperatorFactory.UpdateNewsCategoryCache(User.Identity.GetUserId(), ResultModel);
                        ReturnResult = "OK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                    #endregion
                }
                else // old existing model -- do update
                {
                    ReturnResult = "NOK";
                }
            }
            else
            {
                ReturnResult = "NOK";
            }
            client.Close();

            if (ReturnResult=="OK")
            {
                return RedirectToAction("CompanyNewsAdd", "Company");
            }
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyNewsClassManage
        /// <summary>
        /// 企业播报分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyNewsClassManage()
        {
            ViewBag.MenuModel = base.GetMenu(164);

            var client = ServiceHub.GetCommonServiceClient<NewsCategoryServiceClient>();
            var CompanyID = 0;
            List<NewsCategoryModel> CategoryList = null;

            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                CompanyID = UserModel.CompanyID.Value;
                CategoryList = client.NewsCategoryQueryListByCompany(CompanyID).ToList();
            }
            ViewBag.CategoryList = CategoryList;


            NewsCategoryModel CategoryModel = new NewsCategoryModel {
                NewsCategoryOrderID = 0,
                NewsCategoryName = "NewsCategoryName0000",
                NewsCategoryPicture = "NewsCategoryPicture0000",
                NewsCategoryContent = "NewsCategoryContent0000",
            };
            return View(CategoryModel);
        }
        #endregion

        #region CompanyNewsCategoryByID
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyNewsCategoryByID(int CategoryID)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var client = ServiceHub.GetCommonServiceClient<NewsCategoryServiceClient>();
            NewsCategoryModel CategoryModel = null;
            if (!string.IsNullOrEmpty(CurrentUser))
            {
                CategoryModel=client.NewsCategoryQueryById(CategoryID);
                ViewBag.CategoryID = CategoryID;
            }
            else
            {
                CategoryModel = new NewsCategoryModel();
                ViewBag.CategoryID = 0;
            }

            client.Close();
            return Json(CategoryModel);
        }
        #endregion

        #region CompanyNewsCategoryEdit
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyNewsCategoryEdit(int CategoryID,int CompanyID, int IsInsert, string NewsCategoryName, string NewsCategoryPicture, string NewsCategoryContent,
            int NewsCategoryParentID, int NewsCategoryOrderID)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var client = ServiceHub.GetCommonServiceClient<NewsCategoryServiceClient>();
            var ReturnResult = string.Empty;

            if (!string.IsNullOrEmpty(CurrentUser))
            {
                var CategoryModel = new NewsCategoryModel
                {
                    NewsCategoryID=CategoryID,
                    CompanyID = CompanyID,
                    NewsCategoryName=NewsCategoryName,
                    NewsCategoryPicture=NewsCategoryPicture,
                    NewsCategoryContent=NewsCategoryContent,
                    NewsCategoryParentID=NewsCategoryParentID,
                    NewsCategoryOrderID=NewsCategoryOrderID, 
                };

                if (IsInsert == 1) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.NewsCategoryInsert(CategoryModel); // update DB
                    if (ResultModel != null && ResultModel.CompanyID > 0) // update web cache
                    {
                        OperatorFactory.UpdateNewsCategoryCache(User.Identity.GetUserId(), ResultModel);

                        ReturnResult = "OK";
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
                    var ResultModel = client.NewsCategoryUpdate(CategoryModel); // update DB
                    if (ResultModel != null && ResultModel.CompanyID > 0) // update web cache
                    {
                        OperatorFactory.UpdateNewsCategoryCache(User.Identity.GetUserId(), ResultModel);
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

        #region CompanyNewsCategoryDelete
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyNewsCategoryDelete(int CategoryID)
        { 
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var client = ServiceHub.GetCommonServiceClient<NewsCategoryServiceClient>();
            var ReturnResult = string.Empty;

            if (!string.IsNullOrEmpty(CurrentUser))
            {
                var result=client.NewsCategoryDeleteById(CategoryID); // update DB
                if (result)
                {
                    // ??? consider whether remove the related news under newscategory
                    // update web cache
                    OperatorFactory.RemoveNewsCategoryCache(User.Identity.GetUserId());
                    ReturnResult = "OK";
                }
                else
                {
                    ReturnResult = "NOK";
                }
            }
            else
            {
                ReturnResult = "NOK";
            }
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyNewsList
        /// <summary>
        /// 企业播报列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyNewsList()
        {
            ViewBag.MenuModel = base.GetMenu(165);

            var client = ServiceHub.GetCommonServiceClient<NewsServiceClient>();
            var clientCategory = ServiceHub.GetCommonServiceClient<NewsCategoryServiceClient>();
            var CompanyID = 0;
            List<NewsModel> NewsList = null;
            List<NewsCategoryModel> CategoryList = null;

            List<NewsModel> NewsToCategoryList = null;
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                CompanyID = UserModel.CompanyID.Value;

                CategoryList = clientCategory.NewsCategoryQueryListByCompany(CompanyID).ToList();
                NewsList = client.NewsQueryListByCompany(CompanyID).ToList();

                if (NewsList!=null&&CategoryList!=null)
                {
                    NewsToCategoryList=CategoryList.Join(NewsList, c => c.NewsCategoryID, n => n.NewsCategoryID, (c, n) => new NewsModel
                    { 
                         NewsCategoryID=n.NewsCategoryID,
                         NewsAbstract=n.NewsAbstract,
                         NewsBody=n.NewsBody,
                         NewsCategoryModel=c,
                         NewsID=n.NewsID,
                         NewsKeywords=n.NewsKeywords,
                         NewsLabels=n.NewsLabels,
                         NewsOrderID=n.NewsOrderID,
                         NewsThumbnail=n.NewsThumbnail,
                         NewsTitle=n.NewsTitle,
                    }).ToList();
                }
            }
            ViewBag.NewsList = NewsToCategoryList;

            client.Close();
            clientCategory.Close();
            return View();
        }
        #endregion

        #region CompanyPatentAdd
        /// <summary>
        /// 添加企业专利
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyPatentAdd()
        {
            ViewBag.MenuModel = base.GetMenu(162);

            PatentModel PatentModel = new PatentModel {
                PatentNumber = "PatentNumber0000",
                PatentName = "PatentName0000",
                PatentAbstract = "PatentAbstract0000",
                PatentCerificate = "PatentCerificate0000",
                PatentDate=DateTime.Now.ToShortDateString(),
                PatentTechnologyDomain = 0,
                PatentDevelopmentStatus = 0,
                PatentOrderID = 0,
                PatentLabels = "PatentLabels0000",
                PatentIntro = "PatentIntro0000",
                IsDisabled=false,
                IsTransferred=false,
                CompanyID=0,
            };

            var client = ServiceHub.GetCommonServiceClient<PatentServiceClient>();
            ViewBag.TechnologyDomainList = client.GetTechnologyDomainList();
            ViewBag.DevelopmentStatusList = client.GetDevelopmentStatusList();
            client.Close();

            return View(PatentModel);
        }
        #endregion

        #region CompanyPatentSave
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CompanyPatentSave(int CompanyID, int IsInsert, string PatentNumber, string PatentName,
            string PatentAbstract, string PatentCerificate, string PatentDate, string PatentTechnologyDomain,
            string PatentDevelopmentStatus, int PatentOrderID, string PatentLabels, string PatentIntro)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var client = ServiceHub.GetCommonServiceClient<PatentServiceClient>();
            var ReturnResult = string.Empty;
            if (!string.IsNullOrEmpty(CurrentUser) && CompanyID > 0)
            {
                var PatentModel = new PatentModel
                {
                    PatentNumber = PatentNumber,
                    PatentName = PatentName,
                    PatentAbstract = PatentAbstract,
                    PatentCerificate = PatentCerificate,
                    PatentDate = PatentDate,
                    PatentTechnologyDomain = Converter.ToInt(PatentTechnologyDomain),
                    PatentDevelopmentStatus = Converter.ToInt(PatentDevelopmentStatus),
                    PatentOrderID = PatentOrderID,
                    PatentLabels = PatentLabels,
                    PatentIntro = PatentIntro,
                    IsDisabled = false,
                    IsTransferred = false,
                    CompanyID = CompanyID,
                };

                if (IsInsert == 1) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.PatentInsert(PatentModel); // update DB
                    if (ResultModel != null && ResultModel.PatentID > 0) // update web cache
                    {
                        //OperatorFactory.UpdateNewsCategoryCache(User.Identity.GetUserId(), ResultModel);
                        ReturnResult = "OK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                    #endregion
                }
                else // old existing model -- do update
                {
                    ReturnResult = "NOK";
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

        #region CompanyPatentList
        /// <summary>
        /// 专利列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyPatentList()
        {
            ViewBag.MenuModel = base.GetMenu(161);

            var client = ServiceHub.GetCommonServiceClient<PatentServiceClient>();
            var CompanyID = 0;
            List<PatentModel> PatentList = null;

            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                CompanyID = UserModel.CompanyID.Value;
                PatentList = client.PatentQueryListByCompany(CompanyID).ToList();
            }
            ViewBag.PatentList = PatentList;

            return View();
        }
        #endregion

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

        #region CompanySinglePage
        /// <summary>
        /// 单页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanySinglePage()
        {
            ViewBag.MenuModel = base.GetMenu(160);

            var client = ServiceHub.GetCommonServiceClient<SinglePageServiceClient>();
            var CompanyID = 0;
            List<SinglePageModel> PageList = null;

            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                CompanyID = UserModel.CompanyID.Value;
                PageList = client.SinglePageQueryListByCompany(CompanyID).ToList();
            }
            ViewBag.PageList = PageList;

            return View();
        }
        #endregion

        #region CompanySinglePageAdd
        /// <summary>
        /// 添加单页
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanySinglePageAdd()
        {
            ViewBag.MenuModel = base.GetMenu(159);

            SinglePageModel SinglePageModel = new SinglePageModel
            {
                PageTitle = "PageTitle0000",
                PageKeyWords = "PageKeyWords0000",
                PageDescription = "PageDescription0000",
                PageRelationFlag = "PageRelationFlag0000",
                PageBody = "PageBody0000",
                CompanyID=0,
                ModifiedTime=DateTime.Now,
            };
            return View(SinglePageModel);
        }
        #endregion

        #region CompanySinglePageSave
        public ActionResult CompanySinglePageSave(int CompanyID, int IsInsert, string PageTitle, string PageKeyWords,
            string PageDescription, string PageRelationFlag, string PageBody)
        { 
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var client = ServiceHub.GetCommonServiceClient<SinglePageServiceClient>();
            var ReturnResult = string.Empty;
            if (!string.IsNullOrEmpty(CurrentUser) && CompanyID > 0)
            {
                var SinglePageModel = new SinglePageModel
                {
                    PageTitle = PageTitle,
                    PageKeyWords = PageKeyWords,
                    PageDescription = PageDescription,
                    PageRelationFlag = PageRelationFlag,
                    PageBody = PageBody,
                    CompanyID = CompanyID,
                    ModifiedTime = DateTime.Now,
                };
                if (IsInsert == 1) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.SinglePageInsert(SinglePageModel); // update DB
                    if (ResultModel != null && ResultModel.PageID > 0) // update web cache
                    {
                        //OperatorFactory.UpdateNewsCategoryCache(User.Identity.GetUserId(), ResultModel);
                        ReturnResult = "OK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                    #endregion
                }
                else // old existing model -- do update
                {
                    ReturnResult = "NOK";
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

        /// <summary>
        /// 水印设置
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyWatermarkManage()
        {
            ViewBag.MenuModel = base.GetMenu(166);
            var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
            var WateModel = client.WaterFind(User.Identity.GetUserId().ToInt(0));
            client.Close();
            return View(WateModel);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyWaterEdit(bool IsCompanyName, bool IsWebSite, int Postion)
        {
            try
            {
                var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();  
                var  Water = client.WaterFind(User.Identity.GetUserId().ToInt(0));              
                Water.UserId = User.Identity.GetUserId().ToInt(0);
                Water.WaterPostion = Postion;
                Water.IsCompanyName = IsCompanyName;
                Water.IsWebsite = IsWebSite;
                client.WaterEdit(Water);

                client.Close();
                return Content("OK");
            }
            catch (Exception ex)
            {
                return Content("NOK");
            }
        }

    }
}