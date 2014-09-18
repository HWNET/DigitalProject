using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Digital.Contact.Models;
using Digital.Contact.DAL;
using Digital.Contact.BLL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Digital.Web.Controllers
{
    public class UsersController : BaseController
    {

        // GET: /Users/
        public ActionResult Index(int? PageIndex)
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View(base.GetUser());
            //if (!string.IsNullOrEmpty(Request["name"]))
            //{
            //    return SearchFun(PageIndex);
            //}
            //return base.BaseList<UsersModel>(PageIndex);
        }


        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Search(int? PageIndex)
        {
            if (!string.IsNullOrEmpty(Request["name"]))
            {
                return SearchFun(PageIndex);
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //TempDataDictionary viewBag=null;
            //string ss = Digital.Common.Mvc.Extensions.ControllerExtensions.RenderHtml<UsersModel>(this.ControllerContext, "~/Views/Users/Login.cshtml", base.BaseFind(1), viewBag);
            if (returnUrl == null)
            {
                returnUrl = "null";
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string UserName, string Password)
        {
            UsersService bll = new UsersService();
            var _user = bll.FindByName(UserName);
            if (_user != null) return Content("error:" + "用户名已存在");
            else 
            {
                _user = new UsersModel();
                _user.LastLoginTime = System.DateTime.Now;
                _user.LoginIP = Request.UserHostAddress;
                _user.RegisterDate = DateTime.Now;
                _user.Name = UserName;
                _user.Passwords = Common.CryptoService.MD5Encrypt(Password);
                _user.Status = 1;
                bll.Edit(_user);
                return Content("OK");
            }
        }


        #region 
        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
        #endregion

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password, string returnUrl)
        {
            string ErrorMessage = string.Empty;
            UsersService bll = new UsersService();

            var _user = bll.FindByName(UserName);
            if (_user == null) return Content("error:" + "用户名不存在");
            else if (_user.Passwords == Common.CryptoService.MD5Encrypt(Password))
            {
                _user.LastLoginTime = System.DateTime.Now;
                _user.LoginIP = Request.UserHostAddress;
                bll.Edit(_user);
                var _identity = bll.CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, _identity);
                if (string.IsNullOrEmpty(returnUrl)) return Content("Sites:Users/Index");
                else if (Url.IsLocalUrl(returnUrl)) return Content("Sites:" + returnUrl);
                else return Content("Sites:Users/Index");
            }
            else return Content("error:" + "用户名或密码错误！");
        }

        private ActionResult SearchFun(int? PageIndex)
        {
            Func<UsersModel, bool> where = o => o.Name == Request["name"];
            ViewBag.Search = Request["name"];
            Func<UsersModel, int> orderByLambda = o => o.ID;
            return base.BaseList<UsersModel,int>(PageIndex, where, true, orderByLambda);
        }


        public static SelectList GenerateList()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem(){Text="正常", Value="0"},
                new SelectListItem(){Text="已锁定", Value="1"},
                new SelectListItem(){Text="未通过邮件验证", Value="2"},
                new SelectListItem(){Text="未通过管理员确认", Value="3"},
            };

            SelectList generateList = new SelectList(items, "Value", "Text");

            return generateList;
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewData["VStatus"] = GenerateList();
            if (id != null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var userModel = base.BaseFind<UsersModel>(id);
                if (userModel == null || userModel.ID==0)
                {
                    return HttpNotFound();
                }
                return View(userModel);
            }
            else
            {
                return View(new UsersModel());
            }
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Passwords,Status")] UsersModel usersmodel)
        {
            usersmodel.RegisterDate = DateTime.Now;
            usersmodel.LastLoginTime = System.DateTime.Now;
            usersmodel.LoginIP = Request.UserHostAddress;
            usersmodel.Passwords = Common.CryptoService.MD5Encrypt(usersmodel.Passwords);
            if (ModelState.IsValid)
            {
                if (base.BaseEdit(usersmodel))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(usersmodel);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (base.BaseDelete<UsersModel>(id))
                {
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
            catch
            {
                return Content("");
            }
        }



    }
}
