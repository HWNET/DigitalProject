using Digital.Contact.BLL;
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


        public static IBaseService<T> CreateDBOperator<T>(string classname)
        {
            object objType = null;

            try
            {
                var obj = HttpContext.Current.Cache.Get(classname);
                if (obj == null)
                {
                    //objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建
                    var Ass = Assembly.Load("Digital.Contact");
                    objType = Ass.CreateInstance(classname);
                    HttpContext.Current.Cache.Insert(classname, objType);
                }
                else
                {
                    return (IBaseService<T>)obj;
                }
            }
            catch
            { }

            return (IBaseService<T>)objType;
        }


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