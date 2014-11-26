using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Digital.Service
{
    public class GenericList
    {
        public static CacheModel CacheModelObj {get ;set;}
        public GenericList()
        {
            if (CacheModelObj == null)
            {
                CacheModelObj = new CacheModel();
            }
        }

        public void GetCityModel()
        {
            if (CacheModelObj.ProvinceModellist == null)
            {
                CacheModelObj.ProvinceModellist = new List<Contact.Models.ProvinceModel>();
            }
            var ServicePath = AppDomain.CurrentDomain.BaseDirectory;
            var doc = new XmlDocument();
            var path = ServicePath + "\\Config\\City.xml";
            doc.Load(path);
            XmlNodeList ItemModelList = doc.SelectNodes("Root/Province");
            foreach (XmlNode ItemModel in ItemModelList)
            {
                List<Contact.Models.CityModel> CityListModel = new List<Contact.Models.CityModel>();
                var PModel = new Contact.Models.ProvinceModel { ID = int.Parse(ItemModel.Attributes["ID"].Value), Name = ItemModel.Attributes["Name"].Value};
                foreach (XmlNode ChildNode in ItemModel.ChildNodes)
                {
                    CityListModel.Add(new Contact.Models.CityModel { ID = int.Parse(ChildNode.Attributes["ID"].Value), Name = ChildNode.InnerText, ProvinceName = PModel.Name });
                }
                PModel.CityList = CityListModel;
               
                CacheModelObj.ProvinceModellist.Add(PModel);
            }
        }

        public  void InitModel<T>(string xmlName,string ModelName) 
        {
            Type type = typeof(CacheModel);
       
            PropertyInfo pi = type.GetProperty(ModelName+"list");
            
            XmlNodeList XmlModelList= GetXmlConfig.GetSingleXmlItem(xmlName);
            var Ass = Assembly.Load("Digital.Contact");
            var ObjModel = Ass.CreateInstance("Digital.Contact.Models." + ModelName);
            System.Type ModelType = ObjModel.GetType();
 
            PropertyInfo[] fields = ObjModel.GetType().GetProperties();
            List<T> List = new List<T>();
            //var listType = typeof(List<>).MakeGenericType(pi.PropertyType);
            //var addMethod = listType.GetMethod("Add");
            //var ListTest = Activator.CreateInstance(listType);
            foreach (XmlNode XmlModels in XmlModelList)
            {
                var obj = Activator.CreateInstance(ModelType, null);
                foreach (PropertyInfo t in fields)
                {
                    if (t.PropertyType.Name.Contains("Int"))
                    {
                        t.SetValue(obj,int.Parse( XmlModels.Attributes[t.Name].Value), null);
                    }
                    else
                    {
                        t.SetValue(obj,  XmlModels.Attributes[t.Name].Value, null);
                    }
                }
                //addMethod.Invoke((object)ListTest, new object[] { obj });
                List.Add((T)obj);
            }
            pi.SetValue(CacheModelObj, List);
        }


        

        
    }
}
