using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Service
{
    public  class CacheModel
    {
        /// <summary>
        /// ModelName+"list"
        /// </summary>
        public  List<Digital.Contact.Models.MenuModel> MenuModellist { get; set; }
        public List<Digital.Contact.Models.UsersModel> UserModellist { get; set; }
        public List<Digital.Contact.Models.SkillsModel> SkillsModellist { get; set; }
        public List<Digital.Contact.Models.ProvinceModel> ProvinceModellist { get; set; }

        public List<Digital.Contact.Models.CompanyModel> CompanyModelList { get; set; }
        public List<Digital.Contact.Models.CasesCategoryModel> CasesCategoryModelList { get; set; }
        public List<Digital.Contact.Models.CasesModel> CasesModelList { get; set; }
        public List<Digital.Contact.Models.NewsCategoryModel> NewsCategoryModelList { get; set; }
        public List<Digital.Contact.Models.NewsModel> NewsModelList { get; set; }
        public List<Digital.Contact.Models.PatentModel> PatentModelList { get; set; }
        public List<Digital.Contact.Models.SinglePageModel> SinglePageModelList { get; set; }

    }
}
