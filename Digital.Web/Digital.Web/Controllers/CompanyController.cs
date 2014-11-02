using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Digital.Web.Controllers
{
    public class CompanyController : BaseController
    {
        //
        // GET: /Company/Index  这个地方就是他的地址  下面的INDEX就是你页面名  Company就是文件夹的名字 你要现实页面 下面代码必须有  如果你的页面叫LIST   
        public ActionResult Index()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            //this code must be there  ,this use masterpage in this view  so  must  use this code
            return View();
        }


        public ActionResult AccountInfo()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult AccountSecurity()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult AccountTransactionInfo()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }


        //AccountSubUserManage
        public ActionResult AccountSubUserManage()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }



        public ActionResult CompanyBaseInfo()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }


        public ActionResult CompanyBusinessDemand()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyCasesAdd()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyCasesClassManage()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyCasesList()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyCredibility()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyFiles()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyNewsAdd()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyNewsClassManage()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyNewsList()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyPatentAdd()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyPatentList()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyPictureUpload()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyQualificationCertificateList()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanySinglePage()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanySinglePageAdd()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        public ActionResult CompanyWatermarkManage()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

	}
}