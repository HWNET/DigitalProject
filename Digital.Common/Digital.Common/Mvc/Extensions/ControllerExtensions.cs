using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using System.Web;
using Digital.Common.Utilities;

namespace Digital.Common.Mvc.Extensions
{
    public static class ControllerExtensions
    {
        public static string RenderHtml<T>(ControllerContext context, string viewPath, T viewModel, TempDataDictionary viewBag)
        {


            var view = new RazorView(context, viewPath, null, false, null);
            viewBag = viewBag ?? new TempDataDictionary();
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                var data = new ViewDataDictionary<T>(viewModel);
                var viewContext = new ViewContext(context, view, data, viewBag, writer);
                view.Render(viewContext, writer);
                writer.Flush();
            }
            return sb.ToString();
        }

        public static void SavePage(string Html,int TemplateId,string SavePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(SavePath, false, System.Text.Encoding.UTF8)) //保存地址 
                {
                    sw.WriteLine(Html);
                    sw.Flush();
                    sw.Close();

                }
            }
            catch
            {
                
            } 
        }

        //public static List<PageModel> GetHtmlMap(int TemplateId)
        //{
        //    List<PageModel> PageModelList = new List<PageModel>();
        //    try
        //    {
        //        XmlDocument xml = new XmlDocument();
        //        xml.Load(HttpContext.Current.Server.MapPath("~/xml/WebSiteUrl.xml"));
        //        XmlNodeList nodelist = xml.SelectNodes("/Root/Template");
        //        foreach (XmlNode Node in nodelist)
        //        {
        //            if (Node.Attributes["Id"].Value == TemplateId.ToString())
        //            {
        //                foreach (XmlNode PageNodel in Node.ChildNodes)
        //                {
        //                    var PageModels = new PageModel()
        //                    {
        //                        Name = PageNodel.Attributes["Name"].Value,
        //                        Path = PageNodel.Attributes["Path"].Value,
        //                        Model = PageNodel.Attributes["Model"].Value,
        //                        Loop = PageNodel.Attributes["Loop"].Value.ToBool(),
        //                        PageSize = PageNodel.Attributes["PageSize"].Value.ToInt(),
        //                        Formate = PageNodel.Attributes["Formate"].Value,

        //                    };
        //                    List<PageModelParemetr> Paremeterlist=new List<PageModelParemetr>();
        //                    foreach (XmlNode ParemeterNodel in PageNodel.ChildNodes)
        //                    {
        //                        Paremeterlist.Add(new PageModelParemetr() {
        //                            ParemeterName = ParemeterNodel.Attributes["value"].Value
        //                        });
        //                    }
        //                    PageModels.Paremeter=Paremeterlist;
        //                    PageModelList.Add(PageModels);
                            
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
 
        //    }
        //    return PageModelList;
        //}


        //public static string[] GetParemeterValue(List<PageModelParemetr> ParemetrName)
        //{
        //    string[] ArrValue=new string[ParemetrName.Count];
        //    for(int i=0;i<ParemetrName.Count;i++)
        //    {
        //        if(ParemetrName[i].ParemeterName=="CasesCategoryID")
        //        {

        //        }
        //    }
        //    return ArrValue;
        //}

        public static string OutHtml(ControllerContext cc, string tempUrl, ViewDataDictionary vd, TempDataDictionary td) 
        { 
            string html = string.Empty;
            IView v = ViewEngines.Engines.FindView(cc, tempUrl, "").View; 
            using (StringWriter sw = new StringWriter()) 
            { 
                ViewContext vc = new ViewContext(cc, v, vd, td, sw);
                vc.View.Render(vc, sw); 
                html = sw.ToString(); 
            } return html; 
        }
    }


    // <Page Name="index" Path="\WebSiteTemplate\Template1\index.cshtml">
    //</Page>
    //<Page Name="CaseList" Model="CasesModel" Loop="true" PageSize="15" Formate="CaseList_{0}_{1}.html" Path="\WebSiteTemplate\Template1\portfolio.cshtml">
    //  <Paremeter value="CasesCategoryID" ></Paremeter>
    //  <Paremeter value="PageId" ></Paremeter>
    //</Page>
    //<Page Name="CaseDetail" Model="CasesModel" Loop="true" PageSize="1" Formate="CaseDetail_{0}.html" Path="\WebSiteTemplate\Template1\blog.cshtml">
    //   <Paremeter value="CasesID" ></Paremeter>
    //</Page>
    
}
