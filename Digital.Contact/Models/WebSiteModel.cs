using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.Models
{
   

    public class WebSiteModel
    {

        public int CompanyId { get; set; }

        public string WebTitle { get; set; }

        public string TemplatePath { get; set; }

        public string Description { get; set; }

        public string KeyWords { get; set; }
    }

    //[KnownType(typeof(object))]
    //[KnownType(typeof(object[]))]
    //[KnownType(typeof(PageModel))]
    public class PageModel
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Model { get; set; }
        //public object[] ModelList { get; set; }
        //public object ObjModel { get; set; }

        public List<CasesModel> CaseModelList { get; set; }

        public CasesModel CaseModel { get; set; }

        public bool Loop { get; set; }

        public int PageSize { get; set; }

        public string Formate { get; set; }

        public string FileName { get; set; }

        public List<PageModelParemetr> Paremeter { get; set; }
    }

    public class PageModelParemetr
    {
        public string ParemeterName { get; set; }
        public string ParemeterValue { get; set; }
    }
}
