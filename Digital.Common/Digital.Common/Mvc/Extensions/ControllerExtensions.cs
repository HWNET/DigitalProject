using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;

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


        //public void SaveWebSite(string Path)
        //{
        //    //if(File.)
        //}
    }
}
