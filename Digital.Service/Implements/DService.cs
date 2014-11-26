﻿using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

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

        public void Init()
        {
            _CheckReloadXml = new Task(() =>
            {
                try
                {
                    CheckReloadStatus(_cancelToken);
                }
                catch (Exception ex)
                {
                    //log
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
            if (GenericList.CacheModelObj.UserModellist == null)
            {
                Digital.Contact.BLL.UsersService UserService = new Contact.BLL.UsersService();
                //Skill cache
                
                GenericList.CacheModelObj.UserModellist = UserService.GetAllUserList();
                foreach (var Usermodel in GenericList.CacheModelObj.UserModellist)
                {
                    Usermodel.UsersInfoModel.CityModels = GenericList.CacheModelObj.ProvinceModellist.Where(o => o.ID == Usermodel.UsersInfoModel.ProvinceID).FirstOrDefault().CityList.Where(o => o.ID == Usermodel.UsersInfoModel.CityID).FirstOrDefault();
                    foreach (var goodat in Usermodel.UsersInfoModel.GoodAtWhatModels)
                    {
                        goodat.SkillsModel = GenericList.CacheModelObj.SkillsModellist.Where(o => o.SkillId == goodat.SkillId).FirstOrDefault();
                    }
                }
            }
        }


       
        #endregion


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
                      
                    }

                }
                catch (Exception ex)
                {
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
            }

        }



        public void Dispose()
        {
        }

    }
}
