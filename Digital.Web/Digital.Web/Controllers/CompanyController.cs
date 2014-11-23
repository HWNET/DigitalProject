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


        /// <summary>
        /// 账户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountInfo()
        {
            ViewBag.MenuModel = base.GetMenu(101);
            return View();
        }

        /// <summary>
        /// 账户安全
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountSecurity()
        {
            ViewBag.MenuModel = base.GetMenu(103);
            return View();
        }

        /// <summary>
        /// 交易资料
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountTransactionInfo()
        {
            ViewBag.MenuModel = base.GetMenu(104);
            return View();
        }


        //AccountSubUserManage
        /// <summary>
        /// 子账户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SubUserManage()
        {
            ViewBag.MenuModel = base.GetMenu(103);
            return View();
        }


        /// <summary>
        /// 企业信息中心/基础信息
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyBaseInfo()
        {
            ViewBag.MenuModel = base.GetMenu(151);
            return View();
        }


        public ActionResult CompanyBusinessDemand()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

        /// <summary>
        /// 添加企业案例
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCasesAdd()
        {
            ViewBag.MenuModel = base.GetMenu(156);
            return View();
        }

        /// <summary>
        /// 企业案例分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCasesClassManage()
        {
            ViewBag.MenuModel = base.GetMenu(157);
            return View();
        }

        /// <summary>
        /// 企业案例列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCasesList()
        {
            ViewBag.MenuModel = base.GetMenu(158);
            return View();
        }

        /// <summary>
        /// 企业信誉
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyCredibility()
        {
            ViewBag.MenuModel = base.GetMenu(153);
            return View();
        }

        /// <summary>
        /// 企业文件柜
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyFiles()
        {
            ViewBag.MenuModel = base.GetMenu(154);
            return View();
        }

        /// <summary>
        /// 添加企业播报
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyNewsAdd()
        {
            ViewBag.MenuModel = base.GetMenu(163);
            return View();
        }

        /// <summary>
        /// 企业播报分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyNewsClassManage()
        {
            ViewBag.MenuModel = base.GetMenu(164);
            return View();
        }

        /// <summary>
        /// 企业播报列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyNewsList()
        {
            ViewBag.MenuModel = base.GetMenu(165);
            return View();
        }


        /// <summary>
        /// 添加企业案例
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyPatentAdd()
        {
            ViewBag.MenuModel = base.GetMenu(162);
            return View();
        }

        /// <summary>
        /// 案例列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyPatentList()
        {
            ViewBag.MenuModel = base.GetMenu(161);
            return View();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyPictureUpload()
        {
            ViewBag.MenuModel = base.GetMenu(155);
            return View();
        }

        /// <summary>
        /// 资质介绍
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyQualificationCertificateList()
        {
            ViewBag.MenuModel = base.GetMenu(152);
            return View();
        }

        /// <summary>
        /// 单页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanySinglePage()
        {
            ViewBag.MenuModel = base.GetMenu(160);
            return View();
        }

        /// <summary>
        /// 添加单页
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanySinglePageAdd()
        {
            ViewBag.MenuModel = base.GetMenu(159);
            return View();
        }

        /// <summary>
        /// 水印设置
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyWatermarkManage()
        {
            ViewBag.MenuModel = base.GetMenu(166);
            return View();
        }

	}
}