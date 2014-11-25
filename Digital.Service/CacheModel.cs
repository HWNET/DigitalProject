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
    }
}
