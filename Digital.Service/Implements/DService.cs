using Digital.Common.Logging;
using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Digital.Contact.BLL;
using System.Configuration;

namespace Digital.Service.Implements
{

    [ServiceBehavior
        (IncludeExceptionDetailInFaults = true,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.Single)]
    public partial class Service : IDisposable
    {
        public Service()
        {

        }

        public Task _CheckReloadXml;
        public Task _CHeckReloadDB;
        public CancellationToken _cancelToken;
        private CancellationTokenSource _cancelDBToken;
        private CancellationTokenSource _cancelPageToken;

        private int delayTime = 10;

        public Digital.Contact.Models.MenuModel MenuList { get; set; }

        public NLogHelper logger { get; set; }

        public void Init()
        {
            logger = new NLogHelper();
            logger.WriteInfo("1111");
            try
            {
                TaskScheduler.UnobservedTaskException += (s, e) =>
                {
                    e.SetObserved();
                    logger.WriteInfo("Service" + e.Exception.InnerException.ToString());
                };
            }
            catch (Exception ex)
            {
                logger.WriteInfo("Service" + ex.ToString());
            }
            _CheckReloadXml = new Task(() =>
            {
                try
                {
                    CheckReloadStatus(_cancelToken);
                }
                catch (Exception ex)
                {
                    logger.WriteInfo("_CheckReloadXml:Error" + ex.ToString());
                }
            });
            _CheckReloadXml.Start();
            //_CHeckReloadDB = new Task(() =>
            //{
            //    try
            //    {
            //        UpdateDB(_cancelDBToken);
            //    }
            //    catch (Exception ex)
            //    {
            //        //log
            //    }
            //});
            //_CHeckReloadDB.Start();
            _cancelDBToken = new CancellationTokenSource();
            CreateDBMonitor(_cancelDBToken.Token);
            _cancelPageToken = new CancellationTokenSource();
            CreatePageMonitor(_cancelPageToken.Token);
        }



        private void CreateDBMonitor(CancellationToken token)
        {
            //_tagMonitorTask = Task.Factory.StartNew(DAServiceTripMonitor, token, token);
            Task.Factory.StartNew(UpdateDB, token, token).IgnoreExceptions();

        }

        private void CreatePageMonitor(CancellationToken token)
        {
            Task.Factory.StartNew(PageMonitor, token, token).IgnoreExceptions();
        }

        #region
        private void PageMonitor(object param)
        {
            while (true)
            {
                var cToken = (CancellationToken)param;
                if (cToken.IsCancellationRequested)
                {
                    break;
                }
                try
                {
                    //get create page event from queue
                    if (GenericList.PageBuffer != null)
                    {
                        var _Buffer = GenericList.PageBuffer.Get(cToken);
                        if (_Buffer != null)
                        {
                            var CPageModel = _Buffer as CreatePageModel;
                            var PageList = GetPageList(_Buffer.TemplateId, _Buffer.CompanyId);
                            WebSiteService bll = new WebSiteService();
                            CPageModel.IsScan = true;
                            bll.UpdatePage(CPageModel);
                            string Path = ConfigurationManager.AppSettings["WebRoot"].ToString();
                            foreach (var PageModel in PageList)
                            {
                                string Html = string.Empty;
                                if (PageModel.Model == "CasesModel")
                                {
                                    if (PageModel.PageSize == 1)
                                    {
                                        string path1 = PageModel.Path;

                                        var index = System.IO.File.ReadAllText(path1, Encoding.UTF8);
                                        Html = RazorEngine.Razor.Parse<CasesModel>(index, PageModel.CaseModel, PageModel.Name);
                                    }
                                    else
                                    {
                                        string path1 = PageModel.Path;

                                        var index = System.IO.File.ReadAllText(path1, Encoding.UTF8);
                                        Html = RazorEngine.Razor.Parse<CasesModel>(index, PageModel.CaseModel, PageModel.Name);
                                    }
                                }
                                Digital.Common.Mvc.Extensions.ControllerExtensions.SavePage(Html, _Buffer.TemplateId, Path + @"\Company\" + _Buffer.CompanyId + @"\" + PageModel.FileName);

                            }
                            CPageModel.State = 1;
                            CPageModel.IsScan = false;
                            CPageModel.UpdateTime = DateTime.Now;
                            bll.UpdatePage(CPageModel);
                        }
                    }
                    else
                    {
                        Thread.Sleep(60000);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion


        private object CacheLock = new object();


        #region DBCache function
        /// <summary>
        /// User cache
        /// </summary>
        private void UserCacheList()
        {
            logger.WriteInfo("UserCacheList");
            if (GenericList.CacheModelObj.UserModellist == null)
            {
                try
                {
                    GenericList.CacheModelObj.UserModellist = new Dictionary<int, UsersModel>();
                    Digital.Contact.BLL.UsersService UserService = new Contact.BLL.UsersService();
                    //Skill cache
                    var UserList = UserService.GetAllUserList();
                    if (UserList != null)
                    {
                        foreach (var User in UserList)
                        {
                            GenericList.CacheModelObj.UserModellist.Add(User.ID, User);
                        }
                    }

                    foreach (var Usermodel in GenericList.CacheModelObj.UserModellist)
                    {
                        var ProvinceModel = GenericList.CacheModelObj.ProvinceModellist.Where(o => o.ID == Usermodel.Value.UsersInfoModel.ProvinceID).FirstOrDefault();
                        if (ProvinceModel != null && Usermodel.Value.UsersInfoModel.CityID != null)
                        {
                            Usermodel.Value.UsersInfoModel.CityModels = GenericList.CacheModelObj.ProvinceModellist.Where(o => o.ID == Usermodel.Value.UsersInfoModel.ProvinceID).FirstOrDefault().CityList.Where(o => o.ID == Usermodel.Value.UsersInfoModel.CityID).FirstOrDefault();
                        }
                        if (Usermodel.Value.UsersInfoModel.GoodAtWhatModels != null)
                        {
                            foreach (var goodat in Usermodel.Value.UsersInfoModel.GoodAtWhatModels)
                            {
                                goodat.SkillsModel = GenericList.CacheModelObj.SkillsModellist.Where(o => o.SkillId == goodat.SkillId).FirstOrDefault();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.WriteInfo("UserCacheList:Error:" + ex.ToString());
                }
            }
        }



        /// <summary>
        /// Company Cache
        /// </summary>
        private void CompanyCacheList()
        {
            logger.WriteInfo("CompanyCacheList");
            if (GenericList.CacheModelObj.CompanyModellist == null)
            {
                try
                {
                    GenericList.CacheModelObj.CompanyModellist = new List<CompanyModel>();
                    CompanyService CompanyService = new CompanyService();

                    var CompanyList = CompanyService.CompanyQueryList();
                    if (CompanyList != null)
                    {
                        foreach (var CModel in CompanyList)
                        {
                            #region TAB ONE -- UI Models
                            //if (CModel.CompanyTypeNO>0)
                            //{
                            //    CModel.CompanyTypeMode = new CompanyTypeMode { Id = CModel.CompanyTypeNO, Name = GenericList.CacheModelObj.CompanyTypeModelist.Where(o => o.Id == CModel.CompanyTypeNO).SingleOrDefault().Name };
                            //}
                            //if (CModel.CompanyMembers>0)
                            //{
                            //    CModel.CompanyMemberMode = new CompanyMemberMode { Id = CModel.CompanyMembers, Name = GenericList.CacheModelObj.CompanyMemberModelist.Where(o => o.Id == CModel.CompanyMembers).SingleOrDefault().Name };
                            //}
                            //if (CModel.CompanyBusinessModel>0)
                            //{
                            //    CModel.CompanyBusinessMode = new CompanyBusinessMode { Id = CModel.CompanyBusinessModel, Name = GenericList.CacheModelObj.CompanyBusinessModelist.Where(o => o.Id == CModel.CompanyBusinessModel).SingleOrDefault().Name };
                            //}

                            //PrimaryBusinessCategoryMode BCategoryMode = null;
                            //if (CModel.PrimaryBusinessCategory>0)
                            //{
                            //    foreach (var BCMode in GenericList.CacheModelObj.PrimaryBusinessCategoryModelist)
                            //    {
                            //        if (BCMode.Id == CModel.PrimaryBusinessCategory)
                            //        {
                            //            BCategoryMode = BCMode;
                            //            break;
                            //        }
                            //    }
                            //    CModel.PrimaryBusinessCategoryMode = new PrimaryBusinessCategoryMode { Id = CModel.PrimaryBusinessCategory, Name = BCategoryMode.Name };
                            //}

                            //if (CModel.PrimaryBusiness>0)
                            //{
                            //    var PrimaryBusinessName = string.Empty;
                            //    foreach (var item in GenericList.CacheModelObj.PrimaryBusinessCategoryModelist)
                            //    {
                            //        var mode = item.PrimaryBusinessList.Where(o => o.Id == CModel.PrimaryBusiness).SingleOrDefault();
                            //        if (mode != null && mode.Id > 0)
                            //        {
                            //            PrimaryBusinessName = mode.Name;
                            //            break;
                            //        }
                            //    }
                            //    CModel.PrimaryBusinessMode = new PrimaryBusinessMode { Id = CModel.PrimaryBusiness, Name = PrimaryBusinessName, BusinessCategoryName = BCategoryMode.Name };
                            //}

                            //if (CModel.PrimarySalesArea>0)
                            //{
                            //    CModel.PrimarySalesAreaMode = new PrimarySalesAreaMode { Id = CModel.PrimarySalesArea, Name = GenericList.CacheModelObj.PrimarySalesAreaModelist.Where(o => o.Id == CModel.PrimarySalesArea).SingleOrDefault().Name };
                            //}

                            //CompanyBusinessProvinceMode BusinessProvinceMode = null;
                            //if (CModel.CompanyBusinessProvince>0)
                            //{
                            //    foreach (var BProvinceMode in GenericList.CacheModelObj.CompanyBusinessProvinceModelist)
                            //    {
                            //        if (BProvinceMode.Id == CModel.CompanyBusinessProvince)
                            //        {
                            //            BusinessProvinceMode = BProvinceMode;
                            //            break;
                            //        }
                            //    }
                            //    CModel.CompanyBusinessProvinceMode = new CompanyBusinessProvinceMode { Id = CModel.CompanyBusinessProvince, Name = BusinessProvinceMode.Name };
                            //}

                            //if (CModel.CompanyBusinessCity>0)
                            //{
                            //    var BusinessCityName = string.Empty;
                            //    foreach (var item in GenericList.CacheModelObj.CompanyBusinessProvinceModelist)
                            //    {
                            //        var mode = item.CompanyBusinessCityModeList.Where(o => o.Id == CModel.CompanyBusinessCity).SingleOrDefault();
                            //        if (mode != null && mode.Id > 0)
                            //        {
                            //            BusinessCityName = mode.Name;
                            //            break;
                            //        }
                            //    }
                            //    CModel.CompanyBusinessCityMode = new CompanyBusinessCityMode { Id = CModel.CompanyBusinessCity, Name = BusinessCityName, BusinessProvinceName = BusinessProvinceMode.Name };
                            //}
                            //#endregion

                            //#region TAB TWO -- UI Models
                            //if (CModel.ProductionForm>0)
                            //{
                            //    CModel.ProductionFormMode = new ProductionFormMode { Id = CModel.ProductionForm, Name = GenericList.CacheModelObj.ProductionFormModelist.Where(o => o.Id == CModel.ProductionForm).SingleOrDefault().Name };
                            //}
                            //if (CModel.ServicesDomain>0)
                            //{
                            //    CModel.ServicesDomainMode = new ServicesDomainMode { Id = CModel.ServicesDomain, Name = GenericList.CacheModelObj.ServicesDomainModelist.Where(o => o.Id == CModel.ServicesDomain).SingleOrDefault().Name };
                            //}
                            //if (CModel.ProcessingMethod>0)
                            //{
                            //    CModel.ProcessingMethodMode = new ProcessingMethodMode { Id = CModel.ProcessingMethod, Name = GenericList.CacheModelObj.ProcessingMethodModelist.Where(o => o.Id == CModel.ProcessingMethod).SingleOrDefault().Name };
                            //}
                            //if (CModel.ProcessingCraft>0)
                            //{
                            //    CModel.ProcessingCraftMode = new ProcessingCraftMode { Id = CModel.ProcessingCraft, Name = GenericList.CacheModelObj.ProcessingCraftModelist.Where(o => o.Id == CModel.ProcessingCraft).SingleOrDefault().Name };
                            //}
                            //if (CModel.EquipmentIntro>0)
                            //{
                            //    CModel.EquipmentIntroMode = new EquipmentIntroMode { Id = CModel.EquipmentIntro, Name = GenericList.CacheModelObj.EquipmentIntroModelist.Where(o => o.Id == CModel.EquipmentIntro).SingleOrDefault().Name };
                            //}
                            //if (CModel.ResearchDepartMembers>0)
                            //{
                            //    CModel.ResearchDepartMemberMode = new CompanyMemberMode { Id = CModel.ResearchDepartMembers, Name = GenericList.CacheModelObj.CompanyMemberModelist.Where(o => o.Id == CModel.ResearchDepartMembers).SingleOrDefault().Name };
                            //}
                            //if (CModel.CapacityIntroUnit>0)
                            //{
                            //    CModel.CapacityUnitMode = new CapacityUnitMode { Id = CModel.CapacityIntroUnit, Name = GenericList.CacheModelObj.CapacityUnitModelist.Where(o => o.Id == CModel.CapacityIntroUnit).SingleOrDefault().Name };
                            //}
                            //if (CModel.AnnualBusinessVolume>0)
                            //{
                            //    CModel.AnnualBusinessVolumeMode = new AnnualBusinessVolumeMode { Id = CModel.AnnualBusinessVolume, Name = GenericList.CacheModelObj.AnnualBusinessVolumeModelist.Where(o => o.Id == CModel.AnnualBusinessVolume).SingleOrDefault().Name };
                            //}
                            //if (CModel.AnnualExportsVolume>0)
                            //{
                            //    CModel.AnnualExportsVolumeMode = new AnnualExportsVolumeMode { Id = CModel.AnnualExportsVolume, Name = GenericList.CacheModelObj.AnnualExportsVolumeModelist.Where(o => o.Id == CModel.AnnualExportsVolume).SingleOrDefault().Name };
                            //}
                            //if (CModel.ManagementSystemCertification>0)
                            //{
                            //    CModel.ManagementSystemCertificationMode = new ManagementSystemCertificationMode { Id = CModel.ManagementSystemCertification, Name = GenericList.CacheModelObj.ManagementSystemCertificationModelist.Where(o => o.Id == CModel.ManagementSystemCertification).SingleOrDefault().Name };
                            //}
                            //if (CModel.ProductQualityCertification>0)
                            //{
                            //    CModel.ProductQualityCertificationMode = new ProductQualityCertificationMode { Id = CModel.ProductQualityCertification, Name = GenericList.CacheModelObj.ProductQualityCertificationModelist.Where(o => o.Id == CModel.ProductQualityCertification).SingleOrDefault().Name };
                            //}
                            //if (CModel.QualityAssurance>0)
                            //{
                            //    CModel.QualityAssuranceMode = new QualityAssuranceMode { Id = CModel.QualityAssurance, Name = GenericList.CacheModelObj.QualityAssuranceModelist.Where(o => o.Id == CModel.QualityAssurance).SingleOrDefault().Name };
                            //}
                            #endregion

                            #region TAB THREE -- UI Models
                            //if (CModel.CompanyRegisteredAssetsUnit>0)
                            //{
                            //    CModel.CompanyRegisteredAssetsUnitMode = new CompanyRegisteredAssetsUnitMode { Id = CModel.CompanyRegisteredAssetsUnit, Name = GenericList.CacheModelObj.CompanyRegisteredAssetsUnitModelist.Where(o => o.Id == CModel.CompanyRegisteredAssetsUnit).SingleOrDefault().Name };    
                            //}
                            //CompanyBusinessProvinceMode RegisteredProvinceMode = null;
                            //if (CModel.CompanyRegisteredProvince>0)
                            //{
                            //    foreach (var RProvinceMode in GenericList.CacheModelObj.CompanyRegisteredProvinceModelist)
                            //    {
                            //        if (RProvinceMode.Id == CModel.CompanyRegisteredProvince)
                            //        {
                            //            RegisteredProvinceMode = RProvinceMode;
                            //            break;
                            //        }
                            //    }
                            //    CModel.CompanyRegisteredProvinceMode = new CompanyBusinessProvinceMode { Id = CModel.CompanyRegisteredProvince, Name = RegisteredProvinceMode.Name };
                            //}

                            //if (CModel.CompanyRegisteredCity>0)
                            //{
                            //    var RegisteredCityName = string.Empty;
                            //    foreach (var item in GenericList.CacheModelObj.CompanyRegisteredProvinceModelist)
                            //    {
                            //        var mode = item.CompanyBusinessCityModeList.Where(o => o.Id == CModel.CompanyRegisteredCity).SingleOrDefault();
                            //        if (mode != null && mode.Id > 0)
                            //        {
                            //            RegisteredCityName = mode.Name;
                            //            break;
                            //        }
                            //    }
                            //    CModel.CompanyRegisteredCityMode = new CompanyBusinessCityMode { Id = CModel.CompanyRegisteredCity, Name = RegisteredCityName, BusinessProvinceName = RegisteredProvinceMode.Name };
                            //}

                            #endregion

                            GenericList.CacheModelObj.CompanyModellist.Add(CModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.WriteInfo("CompanyCacheList:Error:" + ex.ToString());
                }

            }
        }
        /// <summary>
        /// CasesCategory Cache
        /// </summary>
        private void CasesCategoryCacheList()
        {
            logger.WriteInfo("CasesCategoryCacheList");
            if (GenericList.CacheModelObj.CasesCategoryModellist == null)
            {
                try
                {
                    GenericList.CacheModelObj.CasesCategoryModellist = new List<CasesCategoryModel>();
                    CasesCategoryService CasesCategoryService = new CasesCategoryService();

                    var CasesCategoryList = CasesCategoryService.CasesCategoryQueryList();
                    if (CasesCategoryList != null)
                    {
                        CasesCategoryList.ForEach(c => GenericList.CacheModelObj.CasesCategoryModellist.Add(c));
                    }
                }
                catch (Exception ex)
                {
                    logger.WriteInfo("CasesCategoryCacheList:Error:" + ex.ToString());
                }

            }
        }
        /// <summary>
        /// Cases Cache
        /// </summary>
        private void CasesCacheList()
        {
            logger.WriteInfo("CasesCacheList");
            if (GenericList.CacheModelObj.CasesModellist == null)
            {
                try
                {
                    GenericList.CacheModelObj.CasesModellist = new List<CasesModel>();
                    CasesService CasesService = new CasesService();

                    var CasesList = CasesService.CasesQueryList();
                    if (CasesList != null)
                    {
                        CasesList.ForEach(c => GenericList.CacheModelObj.CasesModellist.Add(c));
                    }
                }
                catch (Exception ex)
                {
                    logger.WriteInfo("CasesCacheList:Error:" + ex.ToString());
                }
            }
        }

        #endregion

        /// <summary>
        /// buffer monitor 
        /// </summary>
        /// <param name="param"></param>
        private void UpdateDB(object param)
        {
            while (true)
            {
                var cToken = (CancellationToken)param;
                if (cToken.IsCancellationRequested)
                {

                    break;
                }
                try
                {
                    //get db event from queue
                    if (GenericList.MessageBuffer != null)
                    {
                        //Goodat
                        var _Buffer = GenericList.MessageBuffer.Get(cToken);
                        if (_Buffer != null && _Buffer.MainObject as GoodAtWhatModel != null)
                        {
                            UsersModel UserModel = _Buffer.RootObject as UsersModel;
                            GoodAtWhatModel goodatModel = _Buffer.MainObject as GoodAtWhatModel;
                            UpdateUser.UpdateGoodAtWhatModel(UserModel, goodatModel);
                            continue;
                        }
                        //UserInfor
                        if (_Buffer != null && _Buffer.MainObject as UsersInfoModel != null)
                        {
                            UsersModel UserModel = _Buffer.RootObject as UsersModel;
                            UsersInfoModel UserInfoModel = _Buffer.MainObject as UsersInfoModel;
                            UpdateUser.UpdateUserInfoModel(UserModel, UserInfoModel);
                            continue;
                        }
                        //UserModel
                        if (_Buffer != null && _Buffer.MainObject as UsersModel != null)
                        {
                            //UsersModel UserModel = _Buffer.RootObject as UsersModel;
                            UsersModel UserModel = _Buffer.MainObject as UsersModel;
                            UpdateUser.UpdateUserModel(UserModel);
                            continue;
                        }
                        //CompanyModel
                        if (_Buffer != null && _Buffer.MainObject as CompanyModel != null)
                        {
                            CompanyModel CompanyModelBuffer = _Buffer.MainObject as CompanyModel;
                            if (CompanyModelBuffer.UpdateStatus == 2) //update model on DB
                            {
                                UpdateCompany.UpdateCompanyModel(CompanyModelBuffer);
                            }
                            else if (CompanyModelBuffer.UpdateStatus == 3) //delete model on DB
                            {
                                UpdateCompany.DeleteCompanyModel(CompanyModelBuffer);
                            }
                            continue;
                        }
                    }

                }
                catch (Exception ex)
                {
                    logger.WriteInfo("UpdateDB:Error:" + ex.ToString());
                    //log
                }

            }

        }




        private void CheckReloadStatus(CancellationToken token)
        {
            //根据Service.xml中的配置来查看xml是否被重新加载
            var ct = token;
            bool IsFirst = true;
            while (true)
            {
                try
                {
                    if (ct.IsCancellationRequested)
                        break;
                    bool IsAll = false;

                    if (GetXmlConfig.GetXmlValue("ReloadAll") == "1")
                    {
                        //更新xml放入内存的方法
                        IsAll = true;

                    }
                    else
                    {
                        IsAll = false;

                    }
                    //debugger
                    IsAll = true;
                    List<XmlModel> XmlList = GetXmlConfig.GetNeedReloadXml(IsAll);
                    GenericList InitList = new GenericList();
                    lock (CacheLock)
                    {
                        foreach (var xmlMode in XmlList)
                        {
                            //反射不了 只能写死
                            if (xmlMode.Name == "Menu")
                            {
                                InitList.InitModel<Digital.Contact.Models.MenuModel>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "Skills")
                            {
                                InitList.InitModel<Digital.Contact.Models.SkillsModel>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "City")
                            {
                                InitList.GetCityModel();
                            }
                            //用户通用权限
                            if (xmlMode.Name == "CommonRight")
                            {
                                InitList.GetCommonRightModel();
                            }
                            #region For Company Base Informations
                            #region TAB ONE
                            if (xmlMode.Name == "CompanyType")
                            {
                                InitList.InitModel<CompanyTypeMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "CompanyMember")
                            {
                                InitList.InitModel<CompanyMemberMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "CompanyBusiness")
                            {
                                InitList.InitModel<CompanyBusinessMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "PrimaryBusiness") // PrimaryBusinessCategoryMode , PrimaryBusinessMode
                            {
                                //follow province - city implements
                                InitList.GetPrimaryBusinessModel();
                            }
                            if (xmlMode.Name == "PrimarySalesArea")
                            {
                                InitList.InitModel<PrimarySalesAreaMode>(xmlMode.Name, xmlMode.Model);
                            }
                            #endregion

                            #region TAB TWO
                            if (xmlMode.Name == "ProductionForm")
                            {
                                InitList.InitModel<ProductionFormMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "ServicesDomain")
                            {
                                InitList.InitModel<ServicesDomainMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "ProcessingMethod")
                            {
                                InitList.InitModel<ProcessingMethodMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "ProcessingCraft")
                            {
                                InitList.InitModel<ProcessingCraftMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "EquipmentIntro")
                            {
                                InitList.InitModel<EquipmentIntroMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "CapacityUnit")
                            {
                                InitList.InitModel<CapacityUnitMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "AnnualVolume")
                            {
                                if (xmlMode.Model == "AnnualBusinessVolumeMode")
                                {
                                    InitList.InitModel<AnnualBusinessVolumeMode>(xmlMode.Name, xmlMode.Model);
                                }
                                else if (xmlMode.Model == "AnnualExportsVolumeMode")
                                {
                                    InitList.InitModel<AnnualExportsVolumeMode>(xmlMode.Name, xmlMode.Model);
                                }
                            }
                            if (xmlMode.Name == "SystemCertification")
                            {
                                InitList.InitModel<ManagementSystemCertificationMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "QualityCertification")
                            {
                                InitList.InitModel<ProductQualityCertificationMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "QualityAssurance")
                            {
                                InitList.InitModel<QualityAssuranceMode>(xmlMode.Name, xmlMode.Model);
                            }

                            #endregion

                            #region TAB THREE
                            if (xmlMode.Name == "Year")
                            {
                                InitList.InitModel<CompanyYearEstablishedMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "AssetsUnit")
                            {
                                InitList.InitModel<CompanyRegisteredAssetsUnitMode>(xmlMode.Name, xmlMode.Model);
                            }
                            #endregion

                            #region Company Patent
                            if (xmlMode.Name == "PatentTechnologyDomain")
                            {
                                InitList.InitModel<TechnologyDomainMode>(xmlMode.Name, xmlMode.Model);
                            }
                            if (xmlMode.Name == "PatentDevelopStatus")
                            {
                                InitList.InitModel<DevelopmentStatusMode>(xmlMode.Name, xmlMode.Model);
                            }
                            #endregion

                            #endregion
                            GetXmlConfig.UpdateStatus(xmlMode.Name, "0");
                        }
                        if (IsFirst)
                        {
                            DBCache();
                            IsFirst = false;
                        }
                    }
                    if (IsAll)
                    {
                        GetXmlConfig.UpdateStatus("ReloadAll", "0");
                    }
                }
                catch (Exception ex)
                {
                    logger.WriteInfo("CheckReloadStatus:error:" + ex.ToString());
                    //log
                }
                _CheckReloadXml.Wait(delayTime * 1000);
            }

        }

        /// <summary>
        /// 数据缓存
        /// </summary>
        private void DBCache()
        {
            if (GenericList.CacheModelObj != null)
            {
                UserCacheList();
                //WebCacheSiteList();
                CompanyCacheList();
                CasesCategoryCacheList();
                CasesCacheList();
            }

        }





        public void Dispose()
        {
        }

    }
}
