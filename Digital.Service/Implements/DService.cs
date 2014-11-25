﻿using System;
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
        public CancellationToken _cancelDBToken;
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
            _CHeckReloadDB = new Task(() =>
            {
                try
                {
                    UpdateDB(_cancelDBToken);
                }
                catch (Exception ex)
                {
                    //log
                }
            });
            _CHeckReloadDB.Start();
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
                GenericList.CacheModelObj.UserModellist = UserService.GetAllUserList();
            }
        }


        /// <summary>
        /// Skill cache
        /// </summary>
        private void SkillCacheList()
        {
            if (GenericList.CacheModelObj.UserModellist == null)
            {
                Digital.Contact.BLL.UsersService UserService = new Contact.BLL.UsersService();
                GenericList.CacheModelObj.SkillsModellist = UserService.GetSkillList();
            }
        }
        #endregion


        private void UpdateDB(CancellationToken token)
        {
            var ct = token;
            while (true)
            {
                try
                {
                    if (ct.IsCancellationRequested)
                        break;
                    if (GenericList.CacheModelObj != null)
                    {
                        //UserModel
                        UpdateUser.UpdateUserALLTable();
                    }

                }
                catch (Exception ex)
                {
                    //log
                }
                _CHeckReloadDB.Wait(delayTime * 1000);
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
