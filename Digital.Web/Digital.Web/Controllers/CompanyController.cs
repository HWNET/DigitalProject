﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Digital.WCFClient;
using Digital.WCFClient.ConfigService;
using Microsoft.AspNet.Identity;
using Digital.Common.Utilities;
using System.IO;
using Digital.Common.Captcha;
using System.ServiceModel;

namespace Digital.Web.Controllers
{
    public class CompanyController : BaseController
    {
        //
        // GET: /Company/Index  这个地方就是他的地址  下面的INDEX就是你页面名  Company就是文件夹的名字 你要现实页面 下面代码必须有  如果你的页面叫LIST   
        #region SaveUploadedFile
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
        #endregion

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
            try
            {
                var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
                if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
                {
                    CompanyID = UserModel.CompanyID.Value;
                    CompanyModel = client.CompanyQueryById(CompanyID);
                    if (CompanyModel.CompanyBusinessProvinceMode != null && CompanyModel.CompanyBusinessCityMode != null)
                    {
                        ViewBag.BusinessAddress = CompanyModel.CompanyBusinessProvinceMode.Name + CompanyModel.CompanyBusinessCityMode.Name;
                    }
                    else
                    {
                        ViewBag.BusinessAddress = "";
                    }
                }
                else
                {
                    CompanyModel = new CompanyModel
                    {
                        CompanyName = "",
                        CompanyRegisteredNO = "",
                        PrimarySalesArea = "0",
                        IsProvideOEM = true,
                        //CompanyBusinessAddress = "CompanyBusinessAddress1111",
                        CompanyIntro = ""
                    };
                    ViewBag.BusinessAddress = "";
                    //return HttpNotFound();
                }

                client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
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
            string PrimarySalesArea, int CompanyBusinessProvince, int CompanyBusinessCity, string CompanyIntro)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            var ReturnResult = string.Empty;
            try
            {

            
            if (!string.IsNullOrEmpty(CurrentUser))
            {
                var NewModel = new CompanyModel
                {
                    CompanyID = CompanyID,
                    CompanyName = CompanyName,
                    CompanyRegisteredNO = CompanyRegisteredNO,
                    CompanyTypeNO = CompanyTypeNO,
                    CompanyMembers = CompanyMembers,
                    CompanyBusinessModel = CompanyBusinessModel,
                    IsProvideOEM = IsProvideOEM == 1 ? true : false,
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
                    if (ResultModel != null && ResultModel.CompanyID > 0) // update web cache
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
                    var CompanyModel = client.CompanyUpdate(NewModel, 1); // update DB
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
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyBaseInfoCapacity -- FOR TAB TWO
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyBaseInfoCapacity(int CompanyID, int IsInsert, int ProductionForm,
            string ServicesDomain, string ProcessingMethod, string ProcessingCraft, string EquipmentIntro, int ResearchDepartMembers, int CapacityIntro, int CapacityIntroUnit,
            int AnnualBusinessVolume, int AnnualExportsVolume, string ManagementSystemCertification, string ProductQualityCertification,
            int QualityAssurance, string FactoryArea, string PrimaryEquipments)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var ReturnResult = string.Empty;
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            try
            {

            
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
                    FactoryArea = float.Parse(FactoryArea),
                    PrimaryEquipments = PrimaryEquipments,
                };
                if (IsInsert == 1) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.CompanyInsert(NewModel); // update DB
                    if (ResultModel != null && ResultModel.CompanyID > 0) // update web cache
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
                    var CompanyModel = client.CompanyUpdate(NewModel, 2); // update DB
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
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
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
            try
            {

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

                if (IsInsert == 1) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.CompanyInsert(NewModel); // update DB
                    if (ResultModel != null && ResultModel.CompanyID > 0) // update web cache
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
                    var CompanyModel = client.CompanyUpdate(NewModel, 3); // update DB
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
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            
            return Content(ReturnResult);
        }
        #endregion

        #region UI Modes For Company Base Informations
        #region TAB ONE
        public void SetCompanyTabOne()
        {
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            try
            {
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
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            
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
            try
            {
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
            catch (Exception)
            {
                if (client!=null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
        }
        #endregion

        #region TAB THREE
        public void SetCompanyTabThree()
        {
            var client = ServiceHub.GetCommonServiceClient<CompanyServiceClient>();
            try
            {
                var CompanyYearEstablishedList = client.GetCompanyYearEstablishedList();
                ViewBag.CompanyYearEstablishedList = CompanyYearEstablishedList;

                var RegisteredAssetsUnitList = client.GetCompanyRegisteredAssetsUnitList();
                ViewBag.RegisteredAssetsUnitList = RegisteredAssetsUnitList;
                client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }

                throw;
            }
            
        }
        #endregion

        #endregion

        #region Upgrade UserToCompanyMember
        public bool UpgradeUserToCompanyMember(int CompanyID)
        {
            bool Result = false;
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
            try
            {

            if (UserModel != null)
            {
                if (UserModel.CompanyID == null || (UserModel.CompanyID != null && string.IsNullOrEmpty(UserModel.CompanyID.Value.ToString())))
                {
                    //update web cache for UserModel
                    UserModel.CompanyID = CompanyID;
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
            }
            catch (Exception)
            {
                Result = false;
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
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
            CasesModel CategoryModel = null;
            var client = ServiceHub.GetCommonServiceClient<CasesServiceClient>();
            try
            {

            if (!string.IsNullOrEmpty(Request["Id"]))
            {
                CategoryModel = client.CasesQueryById(Request["Id"].ToString().ToInt());
                client.Close();
            }
            else
            {
                CategoryModel = new CasesModel
                {
                    CasesName = "",
                    CasesAbstract = "",
                    CasesDate = DateTime.Now,
                    CasesOrderBy = "1",
                    CasesLabels = "",
                    CasesDetails = "",
                    CasesCategoryID = 0
                };
            }
            CasesCategoryList();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return View(CategoryModel);
        }

        /// <summary>
        /// 案例分类list
        /// </summary>
        private void CasesCategoryList()
        {
            var CompanyID = 0;
            var client = ServiceHub.GetCommonServiceClient<CasesCategoryServiceClient>();
            List<BaseNameValueMode> CasesCategoryList = new List<BaseNameValueMode>();
            try
            {

            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0)
            {
                CompanyID = UserModel.CompanyID.Value;
                var TempList = client.CasesCategoryQueryListByCompany(CompanyID).ToList();
                foreach (var Temp in TempList)
                {
                    CasesCategoryList.Add(new BaseNameValueMode { Id = Temp.CasesCategoryID, Name = Temp.CasesCategoryName });
                }
                ViewBag.CasesCategory = CasesCategoryList;
            }
            client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCasesDelete(int CasesId)
        {
            var client = ServiceHub.GetCommonServiceClient<CasesServiceClient>();
            try
            {
                client.CasesDeleteById(CasesId);
                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCasesDeleteAll(string CasesIds)
        {
            var client = ServiceHub.GetCommonServiceClient<CasesServiceClient>();
            try
            {
                client.CasesDeleteByIds(CasesIds);
                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CompanyCasesAdd(int Id, string CName, string Abstract, string CImage, string CasesDate, int CasesCategory, int OrderBy, string Labels, string CasesDetails)
        {
            var client = ServiceHub.GetCommonServiceClient<CasesServiceClient>();
            CasesModel CModel = new CasesModel();
            try
            {
                var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
                if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0)
                {
                    if (Id == 0)
                    {
                        CModel.CasesAbstract = Abstract;
                        CModel.CasesCategoryID = CasesCategory;
                        CModel.CasesDate = CasesDate.ToDateTime();
                        CModel.CasesDetails = CasesDetails;
                        CModel.CasesLabels = Labels;
                        CModel.CasesName = CName;
                        CModel.CasesOrderBy = OrderBy.ToString();
                        CModel.CasesThumbnail = CImage;
                        CModel.CompanyID = UserModel.CompanyID.Value;
                        CModel.UpdateStatus = 1;
                        client.CasesInsert(CModel);
                        client.Close();
                    }
                    else
                    {
                        CModel.CasesID = Id;
                        CModel.CasesAbstract = Abstract;
                        CModel.CasesCategoryID = CasesCategory;
                        CModel.CasesDate = CasesDate.ToDateTime();
                        CModel.CasesDetails = CasesDetails;
                        CModel.CasesLabels = Labels;
                        CModel.CasesName = CName;
                        CModel.CasesOrderBy = OrderBy.ToString();
                        CModel.CasesThumbnail = CImage;
                        CModel.CompanyID = UserModel.CompanyID.Value;
                        CModel.UpdateStatus = 1;
                        client.CasesUpdate(CModel);
                        client.Close();
                    }

                    return Content("OK");
                }
                else
                {
                    return Content("NOK");
                }
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
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
            try
            {
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0)
            {
                CompanyID = UserModel.CompanyID.Value;
                CategoryList = client.CasesCategoryQueryListByCompany(CompanyID).ToList();

                ViewBag.CategoryList = CategoryList;
            }
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
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
            var client = ServiceHub.GetCommonServiceClient<CasesServiceClient>();
            try
            {
                CasesCategoryList();
                
                var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
                if (UserModel != null)
                {
                    ViewBag.CaseList = client.CasesQueryListByCompany(UserModel.CompanyID.Value);
                }
                else
                {
                    Response.Redirect(Url.StaticFile());
                }
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
            }
            return View();
        }
        #endregion

        #region CompanyCasesClassAdd
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCasesClassAdd(int Id, string CName, int COrderId, string CImage, string CContent, int CParent)
        {
            var client = ServiceHub.GetCommonServiceClient<CasesCategoryServiceClient>();
            var clientUser = ServiceHub.GetCommonServiceClient<UserServiceClient>();
            try
            {
                var CompanyId = clientUser.GetUserInfo(User.Identity.GetUserId().ToInt(0)).CompanyID;
                clientUser.Close();
                if (Id == 0)
                {
                    var CaseModel = new CasesCategoryModel()
                    {
                        CasesCategoryName = CName,
                        CasesCategoryOrderID = COrderId,
                        CasesCategoryPicture = CImage,
                        CasesCategoryContent = CContent,
                        CasesCategoryParentID = CParent,
                        CompanyID = CompanyId.Value,
                        UpdateStatus = 1
                    };
                    client.CasesCategoryInsert(CaseModel);
                    client.Close();
                }
                else
                {
                    var CaseModel = new CasesCategoryModel()
                    {
                        CasesCategoryName = CName,
                        CasesCategoryOrderID = COrderId,
                        CasesCategoryPicture = CImage,
                        CasesCategoryContent = CContent,
                        CasesCategoryParentID = CParent,
                        CompanyID = CompanyId.Value,
                        CasesCategoryID = Id,
                        UpdateStatus = 2
                    };
                    client.CasesCategoryUpdate(CaseModel);
                    client.Close();
                }
                return Content("OK");
            }
            catch (Exception)
            {
                if (clientUser != null)
                {
                    if (clientUser.State == CommunicationState.Opened)
                    {
                        clientUser.Close();
                    }
                    clientUser = null;
                }
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
        }
        #endregion

        #region CompanyCasesClassDelete
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCasesClassDelete(int CasesId)
        {
            var client = ServiceHub.GetCommonServiceClient<CasesCategoryServiceClient>();
            try
            {
                client.CasesCategoryDeleteById(CasesId);
                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
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

        #region CompanyFiles
        /// <summary>
        /// 企业文件柜
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyFiles()
        {
            ViewBag.MenuModel = base.GetMenu(154);

            var client = ServiceHub.GetCommonServiceClient<FileCabinetServiceClient>();
            try
            {
                var uploadFolder = UploadConfigContext.UploadPath;

                List<FileFolderMode> DirectoryList = new List<FileFolderMode>();
                if (client.VerifyUploadPath(uploadFolder))
                {
                    DirectoryList = client.FileDirectoryList(User.Identity.GetUserId()).ToList();
                }
                ViewBag.DirectoryList = DirectoryList;
                client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return View();
        }
        #endregion

        #region CompanyFilesFolderSave
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyFilesFolderSave(string UserId, string FolderName)
        {
            var ReturnResult = string.Empty;
            var client = ServiceHub.GetCommonServiceClient<FileCabinetServiceClient>();
            var clientFolder = ServiceHub.GetCommonServiceClient<FolderServiceClient>();

            var uploadFolder = UploadConfigContext.UploadPath;
            var FolderNameCode = Config.CaptchaCode;
            var result = false;
            var resultDB = false;
            var CompanyID = 0;
            CompanyModel CompanyModel = null;
            try
            {

            
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0)
            {
                CompanyID = UserModel.CompanyID.Value;
                CompanyModel = OperatorFactory.GetCompanyCache(User.Identity.GetUserId());
            }

            if (client.VerifyUploadPath(uploadFolder))
            {
                result = client.FileDirectoryCreate(UserId,FolderName,FolderNameCode);
                if (result)
                {
                    var folder = new FolderModel { 
                        FolderName=FolderName,
                        FolderNameCode = FolderNameCode,
                        CompanyID = CompanyID,
                        CompanyModel = CompanyModel
                    };
                    var folderResult=clientFolder.FolderInsert(folder); // folder infos , insert DB
                    resultDB = folderResult != null ? true : false;
                }
            }

            if (result && resultDB)
            {
                ReturnResult = "OK";
            }
            else
            {
                ReturnResult = "NOK";
            }

            clientFolder.Close();
            client.Close();
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (clientFolder != null)
                {
                    if (clientFolder.State == CommunicationState.Opened)
                    {
                        clientFolder.Close();
                    }
                    clientFolder = null;
                }
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyFilesList
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyFilesList()
        {
            List<FilesMode> FilesList = new List<FilesMode>();
            var client = ServiceHub.GetCommonServiceClient<FileCabinetServiceClient>();
            try
            {
                var uploadFolder = UploadConfigContext.UploadPath;
                if (client.VerifyUploadPath(uploadFolder))
                {
                    FilesList = client.FilesList(User.Identity.GetUserId(), string.Empty, string.Empty).ToList();
                }
                client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return Json(FilesList);
        }
        #endregion

        #region CompanyFilesByFolder
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyFilesByFolder(string FolderName)
        {
            var client = ServiceHub.GetCommonServiceClient<FileCabinetServiceClient>();
            List<FilesMode> FilesList = new List<FilesMode>();
            try
            {
                var uploadFolder = UploadConfigContext.UploadPath;
                if (client.VerifyUploadPath(uploadFolder))
                {
                    FilesList = client.FilesListByDirectory(User.Identity.GetUserId(), FolderName).ToList();
                }
                ViewBag.FilesList = FilesList;

                client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return Json(FilesList);
        }
        #endregion

        #region CompanyFilesDownloadByFolder
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyFileAllDownloadByFolder(string FolderNames,string FileNames)
        {
            var ReturnResult = string.Empty;
            var StringFolders = FolderNames.Split(',');
            var StringFiles = FileNames.Split(',');

            for (int i = 0; i < StringFolders.Length; i++)
            {
                var UrlFileDownload = string.Format("~/FileDownload.ashx?folderName={0}&fileName={1}", StringFolders[i], StringFiles[i]);
                Response.Redirect(Url.StaticFile(UrlFileDownload));
            }

            return Content(ReturnResult);
        }
        #endregion

        #region CompanyFileDeleteByFolder
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyFileDeleteByFolder(string FolderName, string FileName)
        {
            var ReturnResult = string.Empty;
            var client = ServiceHub.GetCommonServiceClient<FileCabinetServiceClient>();
            try
            {
                var uploadFolder = UploadConfigContext.UploadPath;
                var result = false;
                if (client.VerifyUploadPath(uploadFolder))
                {
                    result = client.FileRemove(User.Identity.GetUserId(), FolderName, FileName);
                }

                if (result)
                {
                    ReturnResult = "OK";
                }
                else
                {
                    ReturnResult = "NOK";
                }
                client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyFileAllDeleteByFolder
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyFileAllDeleteByFolder(string FolderNames, string FileNames)
        {
            var client = ServiceHub.GetCommonServiceClient<FileCabinetServiceClient>();
            try
            {
                var StringFolders = FolderNames.Split(',');
                var StringFiles = FileNames.Split(',');
                
                var uploadFolder = UploadConfigContext.UploadPath;
                for (int i = 0; i < StringFolders.Length; i++)
                {
                    client.FileRemove(User.Identity.GetUserId(), StringFolders[i], StringFiles[i]);
                }
                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
            
        }
        #endregion

        #region CompanyNewsAdd
        /// <summary>
        /// 添加企业播报
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult CompanyNewsAdd()
        {
            ViewBag.MenuModel = base.GetMenu(163);
            NewsModel NewsModel = null;
            var clientNews = ServiceHub.GetCommonServiceClient<NewsServiceClient>();
            var client = ServiceHub.GetCommonServiceClient<NewsCategoryServiceClient>();
            var CompanyID = 0;
            List<BaseNameValueMode> CategoryList = new List<BaseNameValueMode>();

            try
            {
                if (!string.IsNullOrEmpty(Request["Id"]))
                {
                    NewsModel = clientNews.NewsQueryById(Request["Id"].ToString().ToInt());
                    clientNews.Close();
                }
                else
                {
                     NewsModel = new NewsModel
                    {
                        NewsTitle = "",
                        NewsAbstract = "",
                        NewsThumbnail = "",
                        NewsCategoryID = 0,
                        NewsOrderID = 0,
                        NewsKeywords = "",
                        NewsLabels = "",
                        NewsBody = "",
                        ReleaseTime = DateTime.Now,
                    };
                }

                var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
                if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
                {
                    CompanyID = UserModel.CompanyID.Value;
                    var  CategoryModelList = client.NewsCategoryQueryListByCompany(CompanyID).ToList();
                    foreach (var Category in CategoryModelList)
                    {
                        CategoryList.Add(new BaseNameValueMode { Id = Category.NewsCategoryID, Name = Category.NewsCategoryName });
                    }
                }
                ViewBag.CategoryList = CategoryList;
            }
            catch (Exception)
            {
                if (clientNews != null)
                {
                    if (clientNews.State == CommunicationState.Opened)
                    {
                        clientNews.Close();
                    }
                    clientNews = null;
                }
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return View(NewsModel);
        }
        #endregion

        #region CompanyNewsSave
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CompanyNewsSave(int Id, string NewsTitle, string NewsAbstract,
            string NewsThumbnail, int NewsCategoryID, int NewsOrderID, string NewsKeywords, string NewsLabels,
            string NewsBody)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var CompanyID = 0;
            var client = ServiceHub.GetCommonServiceClient<NewsServiceClient>();
            var ReturnResult = string.Empty;
            try
            {

            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                //IsInsert = 0;
                CompanyID = UserModel.CompanyID.Value;
            }

            if (!string.IsNullOrEmpty(CurrentUser) && CompanyID > 0)
            {
                var NewsModel = new NewsModel
                {
                    CompanyID=CompanyID,
                    NewsTitle = NewsTitle,
                    NewsAbstract = NewsAbstract,
                    NewsThumbnail = NewsThumbnail,
                    NewsCategoryID = NewsCategoryID,
                    NewsOrderID = NewsOrderID,
                    NewsKeywords = NewsKeywords,
                    NewsLabels = NewsLabels,
                    NewsBody = NewsBody,
                    ReleaseTime = DateTime.Now,
                };
                if (Id == 0) // new model -- do insert
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
                    NewsModel.NewsID=Id;
                    var ResultModel = client.NewsUpdate(NewsModel);
                    if (ResultModel != null && ResultModel.NewsID > 0) // update web cache
                    {
                        //OperatorFactory.UpdateNewsCategoryCache(User.Identity.GetUserId(), ResultModel);
                        ReturnResult = "OK";
                    }
                    else
                    {
                        ReturnResult = "NOK";
                    }
                }
            }
            else
            {
                ReturnResult = "NOK";
            }
            client.Close();
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }

                throw;
            }
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyNewsDelete
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NewsDelete(int Id)
        {
            var client = ServiceHub.GetCommonServiceClient<NewsServiceClient>();
            try
            {
                client.NewsDeleteById(Id);
                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
        }
        #endregion

        #region CompanyNewsDeletes
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NewsDeletes(string Ids)
        {
            var client = ServiceHub.GetCommonServiceClient<NewsServiceClient>();
            try
            {
                var IdStr = Ids.Trim(',').Split(',');
                foreach (var Id in IdStr)
                {
                    client.NewsDeleteById(Id.ToInt());
                }
                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
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
            NewsCategoryModel CategoryModel = null;
            try
            {
                var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
                if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
                {
                    CompanyID = UserModel.CompanyID.Value;
                    CategoryList = client.NewsCategoryQueryListByCompany(CompanyID).ToList();
                }
                ViewBag.CategoryList = CategoryList;
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }

                throw;
            }
            finally
            {
                CategoryModel = new NewsCategoryModel
                {
                    NewsCategoryOrderID = 0,
                    NewsCategoryName = "",
                    NewsCategoryPicture = "",
                    NewsCategoryContent = "",
                };
            }
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
            try
            {

            if (!string.IsNullOrEmpty(CurrentUser))
            {
                CategoryModel = client.NewsCategoryQueryById(CategoryID);
                ViewBag.CategoryID = CategoryID;
            }
            else
            {
                CategoryModel = new NewsCategoryModel();
                ViewBag.CategoryID = 0;
            }

            client.Close();
            }
            catch (Exception)
            {
                CategoryModel = new NewsCategoryModel();
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return Json(CategoryModel);
        }
        #endregion

        #region CompanyNewsCategoryEdit
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyNewsCategoryEdit(int CategoryID, int IsInsert, string NewsCategoryName, string NewsCategoryPicture, string NewsCategoryContent,
            int NewsCategoryParentID, int NewsCategoryOrderID)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var CompanyID = 0;
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            var client = ServiceHub.GetCommonServiceClient<NewsCategoryServiceClient>();
            var ReturnResult = string.Empty;
            try
            {
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                //IsInsert = 0;
                CompanyID = UserModel.CompanyID.Value;
            }

            if (!string.IsNullOrEmpty(CurrentUser))
            {
                var CategoryModel = new NewsCategoryModel
                {
                    NewsCategoryID = CategoryID,
                    CompanyID = CompanyID,
                    NewsCategoryName = NewsCategoryName,
                    NewsCategoryPicture = NewsCategoryPicture,
                    NewsCategoryContent = NewsCategoryContent,
                    NewsCategoryParentID = NewsCategoryParentID,
                    NewsCategoryOrderID = NewsCategoryOrderID,
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
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
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
            var result = client.NewsCategoryDeleteById(CategoryID); // update DB
            try
            {
            if (!string.IsNullOrEmpty(CurrentUser))
            {
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
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
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
            try
            {

            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                CompanyID = UserModel.CompanyID.Value;

                CategoryList = clientCategory.NewsCategoryQueryListByCompany(CompanyID).ToList();
                NewsList = client.NewsQueryListByCompany(CompanyID).ToList();

                if (NewsList != null && CategoryList != null)
                {
                    NewsToCategoryList = CategoryList.Join(NewsList, c => c.NewsCategoryID, n => n.NewsCategoryID, (c, n) => new NewsModel
                    {
                        NewsCategoryID = n.NewsCategoryID,
                        NewsAbstract = n.NewsAbstract,
                        NewsBody = n.NewsBody,
                        NewsCategoryModel = c,
                        NewsID = n.NewsID,
                        NewsKeywords = n.NewsKeywords,
                        NewsLabels = n.NewsLabels,
                        NewsOrderID = n.NewsOrderID,
                        NewsThumbnail = n.NewsThumbnail,
                        NewsTitle = n.NewsTitle,
                        ReleaseTime=n.ReleaseTime
                    }).ToList();
                }
            }
            ViewBag.NewsList = NewsToCategoryList;

            client.Close();
            clientCategory.Close();
            }
            catch (Exception)
            {
                if (clientCategory != null)
                {
                    if (clientCategory.State == CommunicationState.Opened)
                    {
                        clientCategory.Close();
                    }
                    clientCategory = null;
                }
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
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
            var client = ServiceHub.GetCommonServiceClient<PatentServiceClient>();
            PatentModel PatentModel = null;
            try
            {
                if (!string.IsNullOrEmpty(Request["Id"]))
                {
                    PatentModel = client.PatentQueryById(Request["Id"].ToString().ToInt());
                }
                ViewBag.TechnologyDomainList = client.GetTechnologyDomainList();
                ViewBag.DevelopmentStatusList = client.GetDevelopmentStatusList();
                client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            finally
            {
                PatentModel = new PatentModel
                {
                    PatentNumber = "",
                    PatentName = "",
                    PatentAbstract = "",
                    PatentCerificate = "",
                    PatentDate = DateTime.Now.ToShortDateString(),
                    PatentTechnologyDomain = 0,
                    PatentDevelopmentStatus = 0,
                    PatentOrderID = 0,
                    PatentLabels = "",
                    PatentIntro = "",
                    IsDisabled = false,
                    IsTransferred = false,
                    CompanyID = 0,
                };
            }
            return View(PatentModel);
        }
        #endregion

        #region CompanyPatentSave
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CompanyPatentSave(int Id, string PatentNumber, string PatentName,
            string PatentAbstract, string PatentCerificate, string PatentDate, string PatentTechnologyDomain,
            string PatentDevelopmentStatus, int PatentOrderID, string PatentLabels, string PatentIntro)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            var client = ServiceHub.GetCommonServiceClient<PatentServiceClient>();
            var ReturnResult = string.Empty;
            try
            {
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID > 0)
            {
                if (Id == 0) // new model -- do insert
                {
                    var PatentModel = new PatentModel
                    {
                        PatentNumber = PatentNumber,
                        PatentName = PatentName,
                        PatentAbstract = PatentAbstract,
                        PatentCerificate = PatentCerificate,
                        PatentDate = PatentDate,
                        PatentTechnologyDomain = PatentTechnologyDomain.ToInt(),
                        PatentDevelopmentStatus = PatentDevelopmentStatus.ToInt(),
                        PatentOrderID = PatentOrderID,
                        PatentLabels = PatentLabels,
                        PatentIntro = PatentIntro,
                        IsDisabled = false,
                        IsTransferred = false,
                        CompanyID = UserModel.CompanyID.Value,
                    };
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
                    var UpdatePatentModel = client.PatentQueryById(Request["Id"].ToString().ToInt());
                    UpdatePatentModel.PatentNumber = PatentNumber;
                    UpdatePatentModel.PatentName = PatentName;
                    UpdatePatentModel.PatentAbstract = PatentAbstract;
                    UpdatePatentModel.PatentCerificate = PatentCerificate;
                    UpdatePatentModel.PatentDate = PatentDate;
                    UpdatePatentModel.PatentTechnologyDomain = PatentTechnologyDomain.ToInt();
                    UpdatePatentModel.PatentDevelopmentStatus = PatentDevelopmentStatus.ToInt();
                    UpdatePatentModel.PatentOrderID = PatentOrderID;
                    UpdatePatentModel.PatentLabels = PatentLabels;
                    UpdatePatentModel.PatentIntro = PatentIntro;
                    UpdatePatentModel.IsDisabled = false;
                    UpdatePatentModel.IsTransferred = false;
                    UpdatePatentModel.CompanyID = UserModel.CompanyID.Value;
                    client.PatentUpdate(UpdatePatentModel);
                    ReturnResult = "OK";
                }
            }
            else
            {
                ReturnResult = "NOK";
            }
            client.Close();
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyPatentDelete
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyPatentDelete(int Id)
        {
            var client = ServiceHub.GetCommonServiceClient<PatentServiceClient>();
            try
            {
                client.PatentDeleteById(Id);
                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
        }
        #endregion

        #region CompanyPatentDeletes
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyPatentDeletes(string Ids)
        {
            var client = ServiceHub.GetCommonServiceClient<PatentServiceClient>();
            try
            {
                var IdStr = Ids.Trim(',').Split(',');
                foreach (var Id in IdStr)
                {
                    client.PatentDeleteById(Id.ToInt());
                }
                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
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
            try
            {
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
            {
                CompanyID = UserModel.CompanyID.Value;
                PatentList = client.PatentQueryListByCompany(CompanyID).ToList();
            }
            ViewBag.PatentList = PatentList;
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return View();
        }
        #endregion

        #region CompanyPictureUpload
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyPictureUpload()
        {
            ViewBag.MenuModel = base.GetMenu(155);

            var client = ServiceHub.GetCommonServiceClient<FileCabinetServiceClient>();
            var uploadFolder = UploadConfigContext.UploadPath;

            List<FileFolderMode> DirectoryList = new List<FileFolderMode>();
            try
            {
            if (client.VerifyUploadPath(uploadFolder))
            {
                DirectoryList = client.FileDirectoryList(User.Identity.GetUserId()).ToList();
            }
            ViewBag.DirectoryList = DirectoryList;
            client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return View();
        }
        #endregion

        #region CompanyCertificateSave
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CompanyCertificateSave(int Id, string CertificateName, string CertificateThumbnail, string CertificateIntros)
        {
            var ReturnResult = string.Empty;
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            var client = ServiceHub.GetCommonServiceClient<CertificateServiceClient>();
            try
            {
            if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID > 0)
            {
                var CertificateModel = new CertificateModel { 
                    CertificateName=CertificateName,
                    CertificateThumbnail=CertificateThumbnail,
                    CertificateIntros=CertificateIntros,
                    CompanyID=UserModel.CompanyID.Value,
                };
                if (Id == 0) // new model -- do insert
                {
                    #region new model -- do insert
                    var ResultModel = client.CertificateInsert(CertificateModel); // update DB
                    if (ResultModel != null && ResultModel.CertificateID > 0) // update web cache
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
                    var UpdateCertificateModel = client.CertificateQueryById(Id);
                    UpdateCertificateModel.CertificateName = CertificateName;
                    UpdateCertificateModel.CertificateThumbnail = CertificateThumbnail;
                    UpdateCertificateModel.CertificateIntros = CertificateIntros;
                    UpdateCertificateModel.CompanyID = UserModel.CompanyID.Value;
                    client.CertificateUpdate(UpdateCertificateModel);
                    ReturnResult = "OK";
                }
            }
            else
            {
                ReturnResult = "NOK";
            }
            client.Close();
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }

                throw;
            }
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyQualificationCertificateList
        /// <summary>
        /// 资质介绍
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyQualificationCertificateList()
        {
            ViewBag.MenuModel = base.GetMenu(152);
            
            #region ViewBag.DirectoryList
            var client = ServiceHub.GetCommonServiceClient<FileCabinetServiceClient>();
            List<FileFolderMode> DirectoryList = new List<FileFolderMode>();
            try
            {
                var uploadFolder = UploadConfigContext.UploadPath;
                if (client.VerifyUploadPath(uploadFolder))
                {
                    DirectoryList = client.FileDirectoryList(User.Identity.GetUserId()).ToList();
                }
                ViewBag.DirectoryList = DirectoryList;
                client.Close();
                #endregion

                #region ViewBag.FolderNameCode
                //var clientFolder = ServiceHub.GetCommonServiceClient<FolderServiceClient>();
                //var FolderMode=clientFolder.FolderQueryByName("企业资质");
                //ViewBag.FolderNameCode = FolderMode == null ? "default" : FolderMode.FolderNameCode;
                //clientFolder.Close();
                #endregion
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return View();
        }
        #endregion

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
            try
            {
                var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
                if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID.Value > 0) // UserModel.CompanyID.Value : update existing company model
                {
                    CompanyID = UserModel.CompanyID.Value;
                    PageList = client.SinglePageQueryListByCompany(CompanyID).ToList();
                }
                ViewBag.PageList = PageList;
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
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
            SinglePageModel SinglePageModel = null;
            var client = ServiceHub.GetCommonServiceClient<SinglePageServiceClient>();
            try
            {
                if (!string.IsNullOrEmpty(Request["Id"]))
                {
                    SinglePageModel = client.SinglePageQueryById(Request["Id"].ToString().ToInt());
                    client.Close();
                }
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            finally
            {
                SinglePageModel = new SinglePageModel
                {
                    PageTitle = "",
                    PageKeyWords = "",
                    PageDescription = "",
                    PageRelationFlag = "",
                    PageBody = "",
                    CompanyID = 0,
                    ModifiedTime = DateTime.Now,
                };
            }

            return View(SinglePageModel);
        }
        #endregion

        #region SinglePageDelete
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanySinglePageDelete(int Id)
        {
            var client = ServiceHub.GetCommonServiceClient<SinglePageServiceClient>();
            try
            {
                client.SinglePageDeleteById(Id);
                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
        }
        #endregion

        #region CompanySinglePageSave
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CompanySinglePageSave(int Id, string PageTitle, string PageKeyWords,
            string PageDescription, string PageRelationFlag, string PageBody)
        {
            //currnet log on user
            var CurrentUser = User.Identity.Name;
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            var client = ServiceHub.GetCommonServiceClient<SinglePageServiceClient>();
            var ReturnResult = string.Empty;
            try
            {
                if (UserModel != null && UserModel.CompanyID != null && UserModel.CompanyID > 0)
                {
                    var SinglePageModel = new SinglePageModel
                    {
                        PageTitle = PageTitle,
                        PageKeyWords = PageKeyWords,
                        PageDescription = PageDescription,
                        PageRelationFlag = PageRelationFlag,
                        PageBody = PageBody,
                        CompanyID = UserModel.CompanyID.Value,
                        ModifiedTime = DateTime.Now,
                    };
                    if (Id == 0) // new model -- do insert
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
                        var UpdateSinglePageModel = client.SinglePageQueryById(Id);
                        UpdateSinglePageModel.PageTitle = PageTitle;
                        UpdateSinglePageModel.PageKeyWords = PageKeyWords;
                        UpdateSinglePageModel.PageDescription = PageDescription;
                        UpdateSinglePageModel.PageRelationFlag = PageRelationFlag;
                        UpdateSinglePageModel.PageBody = PageBody;
                        UpdateSinglePageModel.CompanyID = UserModel.CompanyID.Value;
                        UpdateSinglePageModel.ModifiedTime = DateTime.Now;
                        client.SinglePageUpdate(UpdateSinglePageModel);
                        ReturnResult = "OK";
                    }
                }
                else
                {
                    ReturnResult = "NOK";
                }
                client.Close();
            }
            catch (Exception)
            {
                ReturnResult = "NOK";
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return Content(ReturnResult);
        }
        #endregion

        #region CompanyWatermarkManage
        /// <summary>
        /// 水印设置
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyWatermarkManage()
        {
            ViewBag.MenuModel = base.GetMenu(166);
            var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
            WaterMarkModel WateModel = null;
            try
            {
                WateModel = client.WaterFind(User.Identity.GetUserId().ToInt(0));
                client.Close();
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                throw;
            }
            return View(WateModel);
        }
        #endregion

        #region CompanyWaterEdit
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyWaterEdit(bool IsCompanyName, bool IsWebSite, int Postion)
        {
            var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
            try
            {
                var Water = client.WaterFind(User.Identity.GetUserId().ToInt(0));
                Water.UserId = User.Identity.GetUserId().ToInt(0);
                Water.WaterPostion = Postion;
                Water.IsCompanyName = IsCompanyName;
                Water.IsWebsite = IsWebSite;
                client.WaterEdit(Water);

                client.Close();
                return Content("OK");
            }
            catch (Exception)
            {
                if (client != null)
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    client = null;
                }
                return Content("NOK");
            }
        }
        #endregion

    }
}