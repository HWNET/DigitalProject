using Digital.Contact.BLL;
using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Digital.Web.Controllers
{
    public class BaseController<T> : Controller where T:new()
    {
        IBaseService<T> bll = GetBLLInstance();

        private static UsersService UserBll=null;

        private bool GetUserLogin()
        {
            UsersModel Models = new UsersModel();
            if (UserBll == null)
            {
                UserBll = new UsersService();
            }
            return UserBll.IsLogin(Models);
        }

        public ActionResult BaseList<S>(int? PageIndex, Func<T, bool> where, bool IsAsc, Func<T, S> orderByLambda)
        {
            if (GetUserLogin())
            {
                if (PageIndex == null)
                {
                    PageIndex = 1;
                }
                int TotalCount = 0;
                int PageCount = 0;

                var Modelist = bll.PageList(PageIndex.Value, 10, out TotalCount, out PageCount, where, IsAsc, orderByLambda).ToList();
                ViewBag.TotalCount = TotalCount;
                ViewBag.PageCount = PageCount;
                ViewBag.PageIndex = PageIndex;
                return View(Modelist);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult BaseList(int? PageIndex)
        {
            if (GetUserLogin())
            {
                if (PageIndex == null)
                {
                    PageIndex = 1;
                }
                int TotalCount = 0;
                int PageCount = 0;

                var Modelist = bll.PageList(PageIndex.Value, 10, out TotalCount, out PageCount).ToList();
                ViewBag.TotalCount = TotalCount;
                ViewBag.PageCount = PageCount;
                ViewBag.PageIndex = PageIndex;
                return View(Modelist);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public T BaseFind(int? id)
        {
            if (GetUserLogin())
            {
                if (id != null)
                {
                    T usersmodel = bll.Find(id);
                    return usersmodel;
                }
                else
                {
                    return new T();
                }
            }
            else
            {
                return new T();
            }
        }

        public bool BaseEdit(T model)
        {
            if (GetUserLogin())
            {
                return bll.Edit(model);
            }
            else
            {
                return false;
            }
        }

        public bool BaseDelete(int Id)
        {
            if (GetUserLogin())
            {
                return bll.Delete(Id);
            }
            else
            {
                return false;
            }
        }

       

        private static IBaseService<T> GetBLLInstance()
        {
            string ClassName = typeof(T).ToString().Replace("Models", "BLL").Replace("Model", "Service");
            IBaseService<T> bll = OperatorFactory.CreateDBOperator<T>(ClassName);
            return bll;
        }
    }
}
