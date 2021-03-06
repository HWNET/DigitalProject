﻿using System.Collections.ObjectModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Digital.WCFClient.ConfigService;
using Digital.WCFClient;

namespace Digital.Web.Controllers
{
    public class BaseController : Controller 
    {
       

        public string CacheUserKey = "Users.";
        public UsersModel _user { get; set; }

        private bool GetUserLogin()
        {
            _user = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (_user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MenuModel GetMenu(int Id)
        {
         
           //return MenuBll.GetMenuModel(Id);
            var MenuList = OperatorFactory.GetCache<ObservableCollection<MenuModel>>("Menu");
            var CommonRigthList = OperatorFactory.GetCache<ObservableCollection<CommonRightModel>>("CommonRight");
           if (MenuList == null)
           {
               var client = ServiceHub.GetCommonServiceClient<ConfigServiceClient>();
               var MenuModelIist = client.GetMenuList();
               if (CommonRigthList == null)
               {
                   var CommonRigthModelList = client.GetCommonRightList();
                   OperatorFactory.InsertCache<ObservableCollection<CommonRightModel>>(CommonRigthModelList, "CommonRight");
               }
               client.Close();
               MenuList = MenuModelIist;
               OperatorFactory.InsertCache<ObservableCollection<MenuModel>>(MenuList, "Menu");
           }

            

           return MenuList.Where(o => o.ID == Id).FirstOrDefault();
        }

       

        //public ActionResult BaseList<T, S>(int? PageIndex, Func<T, bool> where, bool IsAsc, Func<T, S> orderByLambda) where T : new()
        //{
        //    IBaseService<T> bll = GetBLLInstance<T>();
        //    if (GetUserLogin())
        //    {
        //        if (PageIndex == null)
        //        {
        //            PageIndex = 1;
        //        }
        //        int TotalCount = 0;
        //        int PageCount = 0;

        //        var Modelist = bll.PageList(PageIndex.Value, 10, out TotalCount, out PageCount, where, IsAsc, orderByLambda).ToList();
        //        ViewBag.TotalCount = TotalCount;
        //        ViewBag.PageCount = PageCount;
        //        ViewBag.PageIndex = PageIndex;
        //        return View(Modelist);
        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }
        //}

        //public ActionResult BaseList<T>(int? PageIndex) where T : new()
        //{
        //    if (GetUserLogin())
        //    {
        //        if (PageIndex == null)
        //        {
        //            PageIndex = 1;
        //        }
        //        int TotalCount = 0;
        //        int PageCount = 0;
        //        IBaseService<T> bll = GetBLLInstance<T>();
        //        var Modelist = bll.PageList(PageIndex.Value, 10, out TotalCount, out PageCount).ToList();
        //        ViewBag.TotalCount = TotalCount;
        //        ViewBag.PageCount = PageCount;
        //        ViewBag.PageIndex = PageIndex;
        //        return View(Modelist);
        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }
        //}

        //public T BaseFind<T>(int? id)  where T : new()
        //{
        //    IBaseService<T> bll = GetBLLInstance<T>();
        //    if (GetUserLogin())
        //    {
        //        if (id != null)
        //        {
        //            T usersmodel = bll.Find(id);
        //            return usersmodel;
        //        }
        //        else
        //        {
        //            return new T();
        //        }
        //    }
        //    else
        //    {
        //        return new T();
        //    }
        //}

        //public bool BaseEdit<T>(T model) where T : new()
        //{
        //    IBaseService<T> bll = GetBLLInstance<T>();
        //    if (GetUserLogin())
        //    {
        //        return bll.Edit(model);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool BaseDelete<T>(int Id) where T : new()
        //{
        //    IBaseService<T> bll = GetBLLInstance<T>();
        //    if (GetUserLogin())
        //    {
        //        return bll.Delete(Id);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}



        //private static IBaseService<T> GetBLLInstance<T>()
        //{
        //    string ClassName = typeof(T).ToString().Replace("Models", "BLL").Replace("Model", "Service");
        //    IBaseService<T> bll = OperatorFactory.CreateDBOperator<T>(ClassName);
        //    return bll;
        //}

       
    }
}
