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
                    logger.WriteInfo( "Service"+e.Exception.InnerException.ToString());
                };
            }
            catch (Exception ex)
            {
                logger.WriteInfo( "Service"+ex.ToString());
            }
            _CheckReloadXml = new Task(() =>
            {
                try
                {
                    CheckReloadStatus(_cancelToken);
                }
                catch (Exception ex)
                {
                    logger.WriteInfo("_CheckReloadXml:Error"+ex.ToString());
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
        }

       

        private void CreateDBMonitor(CancellationToken token)
        {
            //_tagMonitorTask = Task.Factory.StartNew(DAServiceTripMonitor, token, token);
            Task.Factory.StartNew(UpdateDB, token, token).IgnoreExceptions();
        }




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
                    if (UserList!=null)
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
                    logger.WriteInfo("UserCacheList:Error:"+ex.ToString());
                }
            }
        }
        /// <summary>
        /// Company Cache
        /// </summary>
        private void CompanyCacheList()
        {
            if (GenericList.CacheModelObj.CompanyModellist==null)
            {
                GenericList.CacheModelObj.CompanyModellist = new List<CompanyModel>();
                CompanyService CompanyService = new CompanyService();

                var CompanyList = CompanyService.CompanyQueryList();
                if (CompanyList!=null)
                {
                    foreach (var Company in CompanyList)
                    {
                        GenericList.CacheModelObj.CompanyModellist.Add(Company);
                    }
                }
            }
        }
        /// <summary>
        /// CasesCategory Cache
        /// </summary>
        private void CasesCategoryCacheList()
        {
            if (GenericList.CacheModelObj.CasesCategoryModellist==null)
            {
                GenericList.CacheModelObj.CasesCategoryModellist = new List<CasesCategoryModel>();
                CasesCategoryService CasesCategoryService = new CasesCategoryService();

                var CasesCategoryList = CasesCategoryService.CasesCategoryQueryList();
                if (CasesCategoryList!=null)
                {
                    CasesCategoryList.ForEach(c => GenericList.CacheModelObj.CasesCategoryModellist.Add(c));
                }
            }
        }
        /// <summary>
        /// Cases Cache
        /// </summary>
        private void CasesCacheList()
        {
            if (GenericList.CacheModelObj.CasesModellist==null)
            {
                GenericList.CacheModelObj.CasesModellist = new List<CasesModel>();
                CasesService CasesService = new CasesService();

                var CasesList = CasesService.CasesQueryList();
                if (CasesList!=null)
                {
                    CasesList.ForEach(c => GenericList.CacheModelObj.CasesModellist.Add(c));
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
                        if (_Buffer!=null&&_Buffer.MainObject as GoodAtWhatModel != null)
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
                        if (_Buffer!=null&&_Buffer.MainObject as CompanyModel !=null)
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
