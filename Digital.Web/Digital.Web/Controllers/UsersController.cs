using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Digital.WCFClient;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Digital.WCFClient;
using Digital.WCFClient.ConfigService;
using System.Security.Claims;

namespace Digital.Web.Controllers
{
    public class UsersController : BaseController
    {

        // GET: /Users/
        public ActionResult Index(int? PageIndex)
        {
            ViewBag.MenuModel = base.GetMenu(2);
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());

            if (UserModel != null)
            {
                return View(UserModel);
            }
            else
            {
                return HttpNotFound();
            }

            //if (!string.IsNullOrEmpty(Request["name"]))
            //{
            //    return SearchFun(PageIndex);
            //}
            //return base.BaseList<UsersModel>(PageIndex);
        }



        public ActionResult UserSafe()
        {
            ViewBag.MenuModel = base.GetMenu(3);
            
            var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
            var SkillList = client.GetSkillList();
            ViewBag.SkillList = SkillList;
            client.Close();
            var UserModel = OperatorFactory.GetUser(User.Identity.GetUserId());
            if (UserModel != null)
            {
                return View(UserModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserSafe(int UsersInfoID, string TrueName, string NickName, int Sex, int ProvinceID, int CityID, string QQ, string Email, string Zip, string Tel)
        {
            if (UsersInfoID != 0)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
                var userModel = client.GetUserInfo(UsersInfoID);
                userModel.UsersInfoModel.TrueName = TrueName;
                userModel.UsersInfoModel.NickName = NickName;
                userModel.UsersInfoModel.Sex = Sex;
                userModel.UsersInfoModel.ProvinceID = ProvinceID;
                userModel.UsersInfoModel.CityID = CityID;
                userModel.UsersInfoModel.QQ = QQ;
                userModel.UsersInfoModel.Email = Email;
                userModel.UsersInfoModel.Zip = Zip;
                userModel.UsersInfoModel.Tel = Tel;
                if (client.UpdateUsersInfoModel(userModel) != null)
                {

                    OperatorFactory.UpdateUserModelCache(User.Identity.GetUserId(), userModel);
                    client.Close();
                    return Content("OK");
                }
                else
                {
                    client.Close();
                    return Content("NOK");
                }


            }
            else
            {
                return Content("NOK");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserSafeIntroduction(int UsersInfoID, string BeGoodAtIntroduction)
        {
            if (UsersInfoID != 0)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
                var userModel = client.GetUserInfo(UsersInfoID);
                //var userInfosModel = base.BaseFind<UsersInfoModel>(UsersInfoID);
                userModel.UsersInfoModel.BeGoodAtIntroduction = BeGoodAtIntroduction;
                if (client.UpdateUsersInfoModel(userModel) != null)
                {
                    OperatorFactory.UpdateUserModelCache(User.Identity.GetUserId(), userModel);
                    client.Close();
                    return Content("OK");
                }
                else
                {
                    client.Close();
                    return Content("NOK");
                }

            }
            else
            {
                return Content("NOK");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserSafeSkill(int UsersInfoID, string SkillStr)
        {
            if (UsersInfoID != 0)
            {
                var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
                var userModel = client.GetUserInfo(UsersInfoID);
                var SkillList = client.GetSkillList();
                string[] SkillIds = SkillStr.Split(',');

                foreach (var SkillModel in SkillList)
                {
                    bool IsInclude = SkillIds.Any(e => e.ToString() == SkillModel.SkillId.ToString());
                    var GoodModel = userModel.UsersInfoModel.GoodAtWhatModels.Where(o => o.UsersInfoID == UsersInfoID && o.SkillId == SkillModel.SkillId).FirstOrDefault();
                    if (GoodModel != null && !IsInclude)
                    {
                        userModel.UsersInfoModel.GoodAtWhatModels.Remove(GoodModel);
                    }
                    else
                    {

                        if (GoodModel == null && IsInclude)
                        {
                            userModel.UsersInfoModel.GoodAtWhatModels.Add(new GoodAtWhatModel { SkillId = SkillModel.SkillId, UsersInfoID = UsersInfoID });
                        }
                    }
                }

                if (client.UpdateGoodAtWhat(userModel))
                {
                    OperatorFactory.UpdateUserModelCache(User.Identity.GetUserId(), userModel);
                    client.Close();
                    return Content("OK");
                }
                else
                {
                    client.Close();
                    return Content("NOK");
                }

            }
            else
            {
                return Content("NOK");
            }


        }

        public ActionResult Test1()
        {
            ViewBag.MenuModel = base.GetMenu(3);
            return View();
        }





        //[HttpPost, ActionName("Index")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Search(int? PageIndex)
        //{
        //    if (!string.IsNullOrEmpty(Request["name"]))
        //    {
        //        return SearchFun(PageIndex);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

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
            var client = ServiceHub.GetCommonServiceClient<UserServiceClient>();
            var _user = client.GetUserInfoByName(UserName);
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
                _user.ID = 0;
                client.Register(_user);
                client.Close();
                return Content("OK");
            }
        }


        #region
        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
        #endregion


        public ClaimsIdentity CreateIdentity(UsersModel user, string authenticationType)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            _identity.AddClaim(new Claim("http://www.yy2xx.com", "longdream"));
            return _identity;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password, string returnUrl)
        {
            string ErrorMessage = string.Empty;
            var client = ServiceHub.GetCommonServiceClient<UserServiceClient>(); ;
           
            var _user = client.GetUserInfoByName(UserName);
            base._user = _user;
            if (_user == null) return Content("error:" + "用户名不存在");
            else if (_user.Passwords == Common.CryptoService.MD5Encrypt(Password))
            {
                _user.LastLoginTime = System.DateTime.Now;
                _user.LoginIP = Request.UserHostAddress;
                client.UpdateUsersModel(_user);
                client.Close();
                OperatorFactory.InsertCache<UsersModel>(_user, base.CacheUserKey + _user.ID);
                var _identity = CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, _identity);
                if (string.IsNullOrEmpty(returnUrl)) return Content("Sites:Users/Index");
                else if (Url.IsLocalUrl(returnUrl)) return Content("Sites:" + returnUrl);

                else return Content("Sites:Users/Index");
            }

            else { client.Close(); return Content("error:" + "用户名或密码错误！"); }
        }

        //private ActionResult SearchFun(int? PageIndex)
        //{
        //    Func<UsersModel, bool> where = o => o.Name == Request["name"];
        //    ViewBag.Search = Request["name"];
        //    Func<UsersModel, int> orderByLambda = o => o.ID;
        //    return base.BaseList<UsersModel, int>(PageIndex, where, true, orderByLambda);
        //}


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
                var client = ServiceHub.GetCommonServiceClient<UserServiceClient>(); ;
                var userModel = client.GetUserInfo(id.Value);
                client.Close();
                if (userModel == null || userModel.ID == 0)
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
            var client = ServiceHub.GetCommonServiceClient<UserServiceClient>(); 
            usersmodel.RegisterDate = DateTime.Now;
            usersmodel.LastLoginTime = System.DateTime.Now;
            usersmodel.LoginIP = Request.UserHostAddress;
            usersmodel.Passwords = Common.CryptoService.MD5Encrypt(usersmodel.Passwords);
            if (ModelState.IsValid)
            {
                if (client.UpdateUsersModel(usersmodel)!=null)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(usersmodel);
        }

        //// POST: /Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        if (base.BaseDelete<UsersModel>(id))
        //        {
        //            return Content("true");
        //        }
        //        else
        //        {
        //            return Content("false");
        //        }
        //    }
        //    catch
        //    {
        //        return Content("");
        //    }
        //}



    }
}
