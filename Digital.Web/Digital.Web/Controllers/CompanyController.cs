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


        public ActionResult CompanyInfo()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }



        public ActionResult AccountInfo()
        {
            ViewBag.MenuModel = base.GetMenu(2);
            return View();
        }

	}
}