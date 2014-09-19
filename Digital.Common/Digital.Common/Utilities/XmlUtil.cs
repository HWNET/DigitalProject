using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Digital.Common.Utilities
{
    public class XmlUtil
    {
        public static string[] ReadPCityNameXml(int PId,int CityId)
        {
            string[] PCString = new string[2];
            XmlDocument xml = GetXMl();
            XmlNodeList nodelist = xml.SelectNodes("/Root/Province");
            PCString[0] = nodelist.Item(PId-1).Attributes["Name"].Value;
            XmlNodeList nodeCitylist = xml.SelectNodes("/Root/Province/City");
            PCString[1] = nodeCitylist.Item(CityId - 1).InnerText;
            return PCString;
        }

        public static XmlDocument GetXMl()
        {
            try
            {
                var obj = HttpContext.Current.Cache.Get("CityXMl");
                if (obj == null)
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(HttpContext.Current.Server.MapPath("~/xml/City.xml"));
                    HttpContext.Current.Cache.Insert("CityXMl", xml);
                    return xml;
                }
                else
                {
                    return (XmlDocument)obj;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
