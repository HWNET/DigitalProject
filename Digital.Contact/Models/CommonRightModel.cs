using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.Models
{
    public class CommonRightModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<MenuModel> MenuModels { get; set; }
    }
}
