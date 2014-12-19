using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Digital.Common.Mvc.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string Script(this HtmlHelper helper, string script)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<script language=\"javascript\" type=\"text/javascript\">");
            sb.Append(script);
            sb.AppendLine("</script>");
            return sb.ToString();
        }

        public static string OnDocumentReady(this HtmlHelper helper, string script)
        {
            var sb = new StringBuilder();
            sb.AppendLine("$(document).ready(function () { ");
            sb.Append(script);
            sb.AppendLine("});");
            return helper.Script(sb.ToString());
        }

        public static string SelectNav(this HtmlHelper helper, string allItemsSelector, string currentSelector, string selectedClass)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("$(\"{0}\").removeClass(\"{1}\");", allItemsSelector, selectedClass));
            sb.AppendLine(string.Format("$(\"{0}\").addClass(\"{1}\");", currentSelector, selectedClass));
            return helper.OnDocumentReady(sb.ToString());
        }

        public static System.Web.IHtmlString IsChecked(this HtmlHelper helper, bool isChecked)
        {
            return helper.Raw(isChecked ? "checked = 'checked'" : string.Empty);
        }

        public static string IsSelected(this HtmlHelper helper, bool isSelected)
        {
            return isSelected ? "selected = 'selected'" : string.Empty;
        }

        public static string IsDisabled(this HtmlHelper helper, bool isDisabled)
        {
            return isDisabled ? "disabled = 'disabled'" : string.Empty;
        }


        /// <summary>
        /// comAjax
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Url"></param>
        /// <param name="FucntionName"></param>
        /// <param name="FormatParam"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static System.Web.IHtmlString ComAjax(this HtmlHelper helper, string Url, string FucntionName, string FormatParam, params string[] Param)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" $.ajaxAntiForgery({");
            sb.AppendLine("   type: \"post\",");
            sb.AppendLine("   data: { " + string.Format(FormatParam, Param) + " },");
            sb.AppendLine("   url: \"" + Url + "\",");
            sb.AppendLine("   success: function (data) {");
            sb.AppendLine("          if (data == \"OK\") {");
            sb.AppendLine("             SaveInfo(true, \"" + FucntionName + "\");");
            sb.AppendLine("            }");
            sb.AppendLine("          else {");
            sb.AppendLine("                SaveInfo(false, \"" + FucntionName + "\");");
            sb.AppendLine("               }");
            sb.AppendLine("   }");
            sb.AppendLine("})");
            return helper.Raw(sb.ToString());
        }



        public static System.Web.IHtmlString MutiDropDownSelectIsNull(this HtmlHelper helper, string StrValue)
        {
            if (string.IsNullOrEmpty(StrValue))
            {
                return helper.Raw( "\"0\"");
            }
            else
            {
                return helper.Raw("\"" + StrValue + "\"");
            }
        }


        public static object IsNull(this HtmlHelper helper, object obj, object IsNullobj, object IsNotNullObj)
        {
            if (obj == null)
            {
                return IsNullobj;
            }
            else
            {
                return IsNotNullObj;
            }
        }

        public static string IsDisplay(this HtmlHelper helper, bool isDisplay)
        {
            return isDisplay ? "display:block" : "display:none";
        }

        public static string ShowClass(this HtmlHelper helper, bool isShow)
        {
            return isShow ? "show" : "hidden";
        }

        public static string IsLastTd(this HtmlHelper helper, int current, int total)
        {
            if (current == total - 1)
                return " last";
            return "";
        }
    }
}
