using Digital.Contact.BLL;
using Digital.Contact.Models;
using Digital.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Digital.Common.Utilities;

namespace Digital.Service.Implements
{
    public partial class Service : IWebSiteService
    {
        public WebSiteModel GetIndexModel(int CompanyId)
        {
            WebSiteModel Model = new WebSiteModel();
            try
            {
                WebSiteService IndexSvr = new WebSiteService();
                Model = IndexSvr.GetWebSiteModel(CompanyId);
            }
            catch (Exception ex)
            {

            }
            return Model;
        }

        public bool CreatePage(CreatePageModel Model)
        {
            try
            {
                WebSiteService IndexSvr = new WebSiteService();
                var CModel= IndexSvr.FindCreatPage(Model.CompanyId);
                //insert
                if (CModel == null)
                {
                    var Buffer = IndexSvr.UpdatePage(Model);
                    GenericList.InsertPageBuffer(Buffer);
                }
                else
                {
                    //update 大于10分钟才能更新
                    if (CModel.UpdateTime != null && DateTime.Now.Subtract(CModel.UpdateTime.Value).TotalMinutes > 10 && !CModel.IsScan)
                    {
                        var Buffer = IndexSvr.UpdatePage(Model);
                        GenericList.InsertPageBuffer(Buffer);
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<PageModel> GetPageList(int TemplateId,int CompanyId)
        {
            List<PageModel> PageList = new List<PageModel>();
            var FormatList= GetHtmlMap(CompanyId);
            foreach (var FormatPage in FormatList)
            {
                if (FormatPage.Loop)
                {
                    if (FormatPage.Model == "CasesModel")
                    {
                        var CaseCategoryList= CasesCategoryQueryListByCompany(CompanyId);
                        var CaselList = CasesQueryListByCompany(CompanyId);
                        if (FormatPage.PageSize > 1)
                        {
                            foreach (var CategoryModel in CaseCategoryList)
                            {
                                var SubCaseList = CaselList.Where(o => o.CasesCategoryID == CategoryModel.CasesCategoryID).ToList();
                                int TotalCount = SubCaseList.Count();
                                int PageCount = (TotalCount / FormatPage.PageSize+1);
                                for (int PageIndex = 1; PageIndex <= PageCount; PageIndex++)
                                {
                                    var PageNewModel = new PageModel()
                                    {
                                        CaseModel = FormatPage.CaseModel,
                                        //CaseModelList = FormatPage.CaseModelList,
                                        FileName = FormatPage.FileName,
                                        Formate = FormatPage.Formate,
                                        Loop = FormatPage.Loop,
                                        Model = FormatPage.Model,
                                        Name = FormatPage.Name,
                                        PageSize = FormatPage.PageSize,
                                        Paremeter = FormatPage.Paremeter,
                                        Path = FormatPage.Path,
                                        
                                    };
                                

                                    //categoryId
                                    PageNewModel.Paremeter[0].ParemeterValue = CategoryModel.CasesCategoryID.ToString();
                                    PageNewModel.Paremeter[1].ParemeterValue = PageIndex.ToString();

                                    PageNewModel.FileName = string.Format(PageNewModel.Formate, PageNewModel.Paremeter[0].ParemeterValue, PageNewModel.Paremeter[1].ParemeterValue);
                                    var tempData = SubCaseList.Skip<CasesModel>(FormatPage.PageSize * (PageIndex - 1)).
                                         Take<CasesModel>(FormatPage.PageSize).ToList();
                                    PageNewModel.CaseModelList = tempData;
                                    PageList.Add(PageNewModel);
                                }
                            }
                            
                        }
                        else
                        {
                            foreach (var CaseModel in CaselList)
                            {
                                var PageNewModel = new PageModel()
                                {
                                   // CaseModel = CaseModel,
                                    CaseModelList = FormatPage.CaseModelList,
                                    FileName = FormatPage.FileName,
                                    Formate = FormatPage.Formate,
                                    Loop = FormatPage.Loop,
                                    Model = FormatPage.Model,
                                    Name = FormatPage.Name,
                                    PageSize = FormatPage.PageSize,
                                    Paremeter = FormatPage.Paremeter,
                                    Path = FormatPage.Path,

                                };
                                //var PageNewModel = FormatPage;
                                PageNewModel.Paremeter[0].ParemeterValue = CaseModel.CasesID.ToString();
                                PageNewModel.CaseModel = CaseModel;
                                PageNewModel.FileName = string.Format(PageNewModel.Formate, PageNewModel.Paremeter[0].ParemeterValue);
                                PageList.Add(PageNewModel);
                            }
                        }
                    }
                }
            }
            return PageList;
        }





        public  List<PageModel> GetHtmlMap(int TemplateId)
        {
            List<PageModel> PageModelList = new List<PageModel>();
            try
            {
                var ServicePath = AppDomain.CurrentDomain.BaseDirectory;
                var xml = new XmlDocument();
                var path = ServicePath + "\\Config\\WebSiteUrl.xml";
                xml.Load(path);
                XmlNodeList nodelist = xml.SelectNodes("/Root/Template");
                foreach (XmlNode Node in nodelist)
                {
                    if (Node.Attributes["Id"].Value == TemplateId.ToString())
                    {
                        foreach (XmlNode PageNodel in Node.ChildNodes)
                        {
                            var PageModels = new PageModel()
                            {
                                Name = PageNodel.Attributes["Name"].Value,
                                Path = PageNodel.Attributes["Path"].Value,
                                Model = PageNodel.Attributes["Model"].Value,
                                Loop = PageNodel.Attributes["Loop"].Value.ToBool(),
                                PageSize = PageNodel.Attributes["PageSize"].Value.ToInt(),
                                Formate = PageNodel.Attributes["Formate"].Value,

                            };
                            List<PageModelParemetr> Paremeterlist = new List<PageModelParemetr>();
                            foreach (XmlNode ParemeterNodel in PageNodel.ChildNodes)
                            {
                                Paremeterlist.Add(new PageModelParemetr()
                                {
                                    ParemeterName = ParemeterNodel.Attributes["value"].Value
                                });
                            }
                            PageModels.Paremeter = Paremeterlist;
                            PageModelList.Add(PageModels);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return PageModelList;
        }
    }
}
