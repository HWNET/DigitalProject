using Digital.Common.Logging;
using Digital.Contact.DAL;
using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.BLL
{
    public class WebSiteService
    {
        public WebSiteModel GetWebSiteModel(int CompanyId)
        {
            WebSiteModel Indexs = new WebSiteModel();
            Indexs.Description = "Test";
            Indexs.CompanyId = CompanyId;
            Indexs.KeyWords = "testKeywords";
            Indexs.WebTitle = "testTitle";
            Indexs.TemplatePath = "/Company/Template1";
            return Indexs;
        }


        public CreatePageModel FindCreatPage(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var PageModels = db.CreatePageModel.Where(o => o.CompanyId == CompanyId).FirstOrDefault();
                    return PageModels;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public CreatePageModel UpdatePage(CreatePageModel CPageModel)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    //if (CPageModel != null && CPageModel.Id != 0)
                    //{
                    //    db.Entry(CPageModel).State = EntityState.Modified;
                    //    db.SaveChanges();
                    //    return CPageModel;
                    //}
                    //else
                    //{
                        var PageModels= db.CreatePageModel.Where(o => o.CompanyId == CPageModel.CompanyId).FirstOrDefault();
                        if (PageModels == null)
                        {
                            CPageModel = db.CreatePageModel.Add(CPageModel);
                            db.SaveChanges();
                        }
                        else
                        {
                            PageModels.IsScan = CPageModel.IsScan;
                            PageModels.CompanyId = CPageModel.CompanyId;
                            PageModels.TemplateId = CPageModel.TemplateId;
                            PageModels.UpdateTime = CPageModel.UpdateTime;
                            PageModels.State = CPageModel.State;
                            db.Entry(PageModels).State = EntityState.Modified;
                            db.SaveChanges();
                           
                        }
                        return CPageModel;
                    //}
                }
                catch (DbEntityValidationException dbEx)
                {
                    Logger.Error(dbEx.InnerException.ToString());
                    return null;
                }
            }
        }
    }
}
