using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Digital.Service
{
    public class XmlModel
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Ref { get; set; }
    }

    public class GetXmlConfig
    {
        public static string GetXmlValue(string Name)
        {
            var ServicePath = AppDomain.CurrentDomain.BaseDirectory;
            var doc = new XmlDocument();
            var path = ServicePath + "\\Config\\Service.xml";
            doc.Load(path);
            XmlNode ItemModel = doc.SelectSingleNode("Root/" + Name);
            return ItemModel.InnerText;
        }

        /// <summary>
        /// 更新需要更新的XML
        /// </summary>
        /// <param name="IsAll">是否是全部XML,否则是1的单个XML更新</param>
        /// <returns></returns>
        public static List<XmlModel> GetNeedReloadXml(bool IsAll)
        {
            var ServicePath = AppDomain.CurrentDomain.BaseDirectory;
            var doc = new XmlDocument();
            var path = ServicePath + "\\Config\\Service.xml";
            doc.Load(path);
            XmlNodeList ItemModelList = doc.SelectNodes("Root/XmlFile/Item");
            List<XmlModel> NeedReloadXmlList = new List<XmlModel>();
            foreach (XmlNode ItemMode in ItemModelList)
            {
                XmlModel XModel = new XmlModel();
                if (IsAll)
                {
                    XModel.Name = ItemMode.Attributes["Name"].Value;
                    XModel.Model = ItemMode.Attributes["Model"].Value;
                    XModel.Ref = ItemMode.Attributes["ref"].Value;
                    NeedReloadXmlList.Add(XModel);
                }
                else
                {
                    if (ItemMode.Value == "1")
                    {
                        XModel.Name = ItemMode.Attributes["Name"].Value;
                        XModel.Model = ItemMode.Attributes["Model"].Value;
                        XModel.Ref = ItemMode.Attributes["ref"].Value;
                        NeedReloadXmlList.Add(XModel);
                    }
                }
            }
            return NeedReloadXmlList;
        }

        public static XmlNodeList GetSingleXmlItem(string xmlName)
        {
            var ServicePath = AppDomain.CurrentDomain.BaseDirectory;
            var doc = new XmlDocument();
            var path = ServicePath + "\\Config\\" + xmlName + ".xml";
            doc.Load(path);
            return doc.SelectNodes("Root/Item");
        }

        /// <summary>
        /// 更新xml状态
        /// </summary>
        /// <param name="XmlName"></param>
        /// <param name="Value"></param>
        public static void UpdateStatus(string XmlName, string Value)
        {
            var ServicePath = AppDomain.CurrentDomain.BaseDirectory;
            var doc = new XmlDocument();
            var path = ServicePath + "\\Config\\Service.xml";
            doc.Load(path);
            if (XmlName != "ReloadAll")
            {
                XmlNodeList ItemModelList = doc.SelectNodes("Root/XmlFile/Item");
                foreach (XmlNode ItemMode in ItemModelList)
                {
                    if (ItemMode.Attributes["Name"].Value == XmlName)
                    {
                        ItemMode.InnerText = Value;
                    }
                }

            }
            else
            {
                XmlNode ItemNodeModel = doc.SelectSingleNode("Root/ReloadAll");
                ItemNodeModel.InnerText = Value;
            }
            doc.Save(path);
        }

    }
}
