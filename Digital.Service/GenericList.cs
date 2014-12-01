using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Digital.Contact.Models;

namespace Digital.Service
{
    public class BufferFormat
    {
        public object RootObject { get; set; }
        public object MainObject { get; set; }
    }

    public class GenericList
    {
        public static CacheModel CacheModelObj { get; set; }
        public static BufferManager<BufferFormat> MessageBuffer { get; set; }
        public GenericList()
        {
            if (CacheModelObj == null)
            {
                CacheModelObj = new CacheModel();
            }
            if (MessageBuffer == null)
            {
                MessageBuffer = new BufferManager<BufferFormat>();
            }
        }

        /// <summary>
        /// 插入buffer
        /// </summary>
        /// <param name="RootObj">根对象</param>
        /// <param name="MainObj">需要修改的对象</param>
        public static void InsertBuffer(object RootObj, object MainObj)
        {
            if (MainObj != null)
            {
                BufferFormat Buffer = new BufferFormat();
                Buffer.RootObject = RootObj;
                Buffer.MainObject = MainObj;
                MessageBuffer.Put(Buffer);
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
                var PModel = new Contact.Models.ProvinceModel { ID = int.Parse(ItemModel.Attributes["ID"].Value), Name = ItemModel.Attributes["Name"].Value };
                foreach (XmlNode ChildNode in ItemModel.ChildNodes)
                {
                    CityListModel.Add(new Contact.Models.CityModel { ID = int.Parse(ChildNode.Attributes["ID"].Value), Name = ChildNode.InnerText, ProvinceName = PModel.Name });
                }
                PModel.CityList = CityListModel;

                CacheModelObj.ProvinceModellist.Add(PModel);
            }
        }

        public void InitModel<T>(string xmlName, string ModelName)
        {
            Type type = typeof(CacheModel);

            PropertyInfo pi = type.GetProperty(ModelName + "list");

            XmlNodeList XmlModelList = GetXmlConfig.GetSingleXmlItem(xmlName);
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
                        t.SetValue(obj, int.Parse(XmlModels.Attributes[t.Name].Value), null);
                    }
                    else
                    {
                        t.SetValue(obj, XmlModels.Attributes[t.Name].Value, null);
                    }
                }
                //addMethod.Invoke((object)ListTest, new object[] { obj });
                List.Add((T)obj);
            }
            pi.SetValue(CacheModelObj, List);
        }

        #region For Company Base Informations
        public void GetPrimaryBusinessModel()
        {
            if (CacheModelObj.PrimaryBusinessCategoryModelist==null)
            {
                CacheModelObj.PrimaryBusinessCategoryModelist = new List<PrimaryBusinessCategoryMode>();
            }

            var ServicePath = AppDomain.CurrentDomain.BaseDirectory;
            var doc = new XmlDocument();
            var path = ServicePath + "\\Config\\PrimaryBusiness.xml";
            doc.Load(path);
            XmlNodeList ItemModelList = doc.SelectNodes("Root/PrimaryBusinessCategory");

            foreach (XmlNode ParentNode in ItemModelList) // Node : PrimaryBusinessCategory
            {
                var BusinessCategoryMode = new PrimaryBusinessCategoryMode { 
                     Id=int.Parse(ParentNode.Attributes["Id"].Value),
                     Name=ParentNode.Attributes["Name"].Value
                };

                List<PrimaryBusinessMode> BusinessModeList = new List<PrimaryBusinessMode>();
                foreach (XmlNode ChildNode in ParentNode.ChildNodes)// Node : PrimaryBusiness
                {
                    BusinessModeList.Add(new PrimaryBusinessMode
                    {
                        Id=int.Parse(ChildNode.Attributes["Id"].Value),
                        Name=ChildNode.Attributes["Name"].Value
                    });
                }
                BusinessCategoryMode.PrimaryBusinessList = BusinessModeList;

                // Cache : PrimaryBusinessCategoryModelist
                CacheModelObj.PrimaryBusinessCategoryModelist.Add(BusinessCategoryMode);
            }

        }
        #endregion



    }
}
