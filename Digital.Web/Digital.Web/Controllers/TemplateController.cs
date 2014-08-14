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

namespace Digital.Web.Controllers
{
    public class TemplateController : BaseController
    {


        public ActionResult ColumnList(int? PageIndex)
        {
            if (!string.IsNullOrEmpty(Request["name"]))
            {
                return SearchFun(PageIndex);
            }
            return base.BaseList<TempColumnModel>(PageIndex);
        }


        // GET: /Template/
        public ActionResult Index(int? PageIndex)
        {

            if (!string.IsNullOrEmpty(Request["name"]))
            {
                return SearchFun(PageIndex);
            }
            return base.BaseList<TemplateModel>(PageIndex);
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
            Func<TemplateModel, bool> where = o => o.Name == Request["name"];
            ViewBag.Search = Request["name"];
            Func<TemplateModel, int> orderByLambda = o => o.ID;
            return base.BaseList<TemplateModel,int>(PageIndex, where, true, orderByLambda);
        }


        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var templatemodels = base.BaseFind<TemplateModel>(id);
                if (templatemodels == null)
                {
                    return HttpNotFound();
                }
                return View(templatemodels);
            }
            else
            {
                return View(new TemplateModel());
            }
        }


        // POST: /Template/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,TableName,IsCreate")] TemplateModel templatemodels)
        {
            if (ModelState.IsValid)
            {
                if (base.BaseEdit(templatemodels))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(templatemodels);
        }



        // POST: /Template/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (base.BaseDelete<TemplateModel>(id))
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
