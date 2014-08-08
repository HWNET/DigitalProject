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

namespace Digital.Web.Controllers
{
    public class UsersController : BaseController<UsersModel>
    {

        // GET: /Users/
        public ActionResult Index(int? PageIndex)
        {
            if (!string.IsNullOrEmpty(Request["name"]))
            {
                return SearchFun(PageIndex);
            }
             return base.BaseList(PageIndex);
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

        private ActionResult SearchFun(int? PageIndex)
        {
            Func<UsersModel, bool> where = o => o.Name == Request["name"];
            ViewBag.Search = Request["name"];
            Func<UsersModel, int> orderByLambda = o => o.ID;
            return base.BaseList<int>(PageIndex, where, true, orderByLambda);
        }
        
        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var userModel = base.BaseFind(id);
                if (userModel == null)
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
        public ActionResult Edit([Bind(Include = "ID,Name,RegisterDate")] UsersModel usersmodel)
        {

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
                if (base.BaseDelete(id))
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
