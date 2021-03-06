﻿using Digital.WCFClient.ConfigService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Digital.Web.Controllers
{
    public class OperatorFactory
    {

        public static string CacheUserKey = "Users.";
        public static string CacheCompanyKey = "Company.";
        public static string CacheNewsCategoryKey = "NewsCategory.";
        //public static IBaseService<T> CreateDBOperator<T>(string classname)
        //{
        //    object objType = null;

        //    try
        //    {
        //        var obj = HttpContext.Current.Cache.Get(classname);
        //        if (obj == null)
        //        {
        //            //objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建
        //            var Ass = Assembly.Load("Digital.Contact");
        //            objType = Ass.CreateInstance(classname);
        //            HttpContext.Current.Cache.Insert(classname, objType);
        //        }
        //        else
        //        {
        //            return (IBaseService<T>)obj;
        //        }
        //    }
        //    catch
        //    { }

        //    return (IBaseService<T>)objType;
        //}

        //public static T GreateDBBll<T>(string classname)
        //{
        //    object objType = null;

        //    try
        //    {
        //        var obj = HttpContext.Current.Cache.Get(classname);
        //        if (obj == null)
        //        {
        //            //objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建
        //            var Ass = Assembly.Load("Digital.Contact");
        //            objType = Ass.CreateInstance(classname);
        //            HttpContext.Current.Cache.Insert(classname, objType);
        //        }
        //        else
        //        {
        //            return (T)obj;
        //        }
        //    }
        //    catch
        //    { }

        //    return (T)objType;
        //}

        /// <summary>
        /// 获取用户缓存
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static UsersModel GetUser(string UserId)
        {
            var UserModels= GetCache<UsersModel>(CacheUserKey + UserId);
            if (UserModels != null)
            {
                return UserModels;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新用户缓存
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserModel"></param>
        public static void UpdateUserModelCache(string UserId, UsersModel UserModel)
        {
            string Key = CacheUserKey + UserId;
            var obj = HttpContext.Current.Cache.Get(Key);
            if (obj != null)
            {
                HttpContext.Current.Cache.Remove(Key);
            }
            HttpContext.Current.Cache.Insert(Key, UserModel);
        }

        #region Data Cache For Company Base Informations
        public static CompanyModel GetCompanyCache(string UserId)
        {
            var CompanyModel = GetCache<CompanyModel>(CacheCompanyKey + UserId);
            if (CompanyModel!=null)
            {
                return CompanyModel;
            }
            else
            {
                return null;
            }
        }
        public static void UpdateCompanyCache(string UserId, CompanyModel CompanyModel)
        {
            var key = CacheCompanyKey + UserId;
            var obj = HttpContext.Current.Cache.Get(key);
            if (obj!=null)
            {
                HttpContext.Current.Cache.Remove(key);
            }
            HttpContext.Current.Cache.Insert(key, CompanyModel);
        }
        #endregion

        #region Data Cache For Company News Category
        public static NewsCategoryModel GetNewsCategoryCache(string UserId)
        {
            var CategoryModel = GetCache<NewsCategoryModel>(CacheNewsCategoryKey + UserId);
            if (CategoryModel != null)
            {
                return CategoryModel;
            }
            else
            {
                return null;
            }
        }
        public static void UpdateNewsCategoryCache(string UserId, NewsCategoryModel CategoryModel)
        {
            var key = CacheNewsCategoryKey + UserId;
            var obj = HttpContext.Current.Cache.Get(key);
            if (obj != null)
            {
                HttpContext.Current.Cache.Remove(key);
            }
            HttpContext.Current.Cache.Insert(key, CategoryModel);
        }
        public static void RemoveNewsCategoryCache(string UserId)
        {
            var key = CacheNewsCategoryKey + UserId;
            var obj = HttpContext.Current.Cache.Get(key);
            if (obj != null)
            {
                HttpContext.Current.Cache.Remove(key);
            }
        }
        #endregion

        public static void InsertCache<T>(T Model,string Key)
        {

            var obj = HttpContext.Current.Cache.Get(Key);
            if (obj == null)
            {
                HttpContext.Current.Cache.Insert(Key, Model);
            }
        }

        public static T GetCache<T>(string Key)
        {
            var obj = HttpContext.Current.Cache.Get(Key);
            return (T)obj;
        }

    }
}