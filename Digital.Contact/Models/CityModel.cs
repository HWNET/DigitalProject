using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.Models
{
    public class ProvinceModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<CityModel> CityList { get; set; }
    }

    public class CityModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string  ProvinceName { get; set; }
    }
}
